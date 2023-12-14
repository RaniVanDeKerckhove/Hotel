﻿using Hotel.Domain.Interfaces;
using Hotel.Domain.Model;
using Hotel.Persistence.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Hotel.Persistence.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string connectionString;

        public CustomerRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Customer> GetCustomers(string filter)
        {
            try
            {
                Dictionary<int, Customer> customers = new Dictionary<int, Customer>();
                string sql = "SELECT t1.Id, t1.Name AS customername, t1.ContactInfo_Email AS email, " +
                             "t2.Email, t2.Phone, t3.City AS address_city, t3.PostalCode AS address_postalcode, " +
                             "t3.Street AS address_street, t3.HouseNumber AS address_housenumber " +
                             "FROM Customer t1 " +
                             "LEFT JOIN ContactInfo t2 ON t1.ContactInfo_Email = t2.Email " +
                             "LEFT JOIN Address t3 ON t2.Address_City = t3.City AND t2.Address_PostalCode = t3.PostalCode " +
                             "WHERE t1.Id IS NOT NULL";

                if (!string.IsNullOrWhiteSpace(filter))
                {
                    sql += " AND (t1.Id LIKE @filter OR t1.Name LIKE @filter OR t1.ContactInfo_Email LIKE @filter)";
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = sql;

                    if (!string.IsNullOrWhiteSpace(filter))
                    {
                        cmd.Parameters.AddWithValue("@filter", $"%{filter}%");
                    }

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["Id"]);
                            if (!customers.ContainsKey(id))
                            {
                                Customer customer = new Customer(id, (string)reader["customername"],
                                    new ContactInfo((string)reader["email"], (string)reader["Phone"],
                                        new Address((string)reader["address_city"], (string)reader["address_postalcode"],
                                            (string)reader["address_street"], (string)reader["address_housenumber"])));
                                customers.Add(id, customer);
                            }
                        }
                    }
                }

                return customers.Values.ToList();
            }
            catch (Exception ex)
            {
                throw new CustomerRepositoryException("getcustomer", ex);
            }
        }

        public void AddCustomer(Customer customer)
        {
            try
            {
                string sql = "INSERT INTO Customer (Name, ContactInfo_Email, ContactInfo_Phone, Address_City, Address_PostalCode, Address_Street, Address_HouseNumber) " +
                             "VALUES (@Name, @Email, @Phone, @City, @PostalCode, @Street, @HouseNumber)";

                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = sql;

                    // Add parameters
                    cmd.Parameters.AddWithValue("@Name", customer.Name);
                    cmd.Parameters.AddWithValue("@Email", customer.Contact.Email);
                    cmd.Parameters.AddWithValue("@Phone", customer.Contact.Phone);
                    cmd.Parameters.AddWithValue("@City", customer.Contact.Address.City);
                    cmd.Parameters.AddWithValue("@PostalCode", customer.Contact.Address.PostalCode);
                    cmd.Parameters.AddWithValue("@Street", customer.Contact.Address.Street);
                    cmd.Parameters.AddWithValue("@HouseNumber", customer.Contact.Address.HouseNumber);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new CustomerRepositoryException("addcustomer", ex);
            }
        }


        public Customer GetCustomerById(int customerId)
        {
            throw new NotImplementedException();
        }

        public void RemoveCustomerById(int customerId)
        {
            throw new NotImplementedException();
        }
    }

}
