using Hotel.Domain.Interfaces;
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
                List<Customer> customers = new List<Customer>();
                string sql = "SELECT c.Id, c.Name AS customername, c.Email, c.PhoneNumber, " +
                             "c.Address_City, c.Address_PostalCode, c.Address_Street, c.Address_HouseNumber, " +
                             "COUNT(m.Id) AS nrOfMembers " +
                             "FROM Customer c " +
                             "LEFT JOIN Member m ON c.Id = m.CustomerId " +
                             "WHERE c.Status = 1 ";

                if (!string.IsNullOrWhiteSpace(filter))
                {
                    sql += "AND c.Id IN (SELECT Id FROM Customer WHERE Id LIKE @filter OR Name LIKE @filter OR Email LIKE @filter)";
                }

                sql += " GROUP BY c.Id, c.Name, c.Email, c.PhoneNumber, " +
                       "c.Address_City, c.Address_PostalCode, c.Address_Street, c.Address_HouseNumber";

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
                            Customer customer = new Customer(
                                id: Convert.ToInt32(reader["Id"]),
                                name: reader["customername"].ToString(),
                                address: new Address(
                                    city: reader["Address_City"].ToString(),
                                    postalCode: reader["Address_PostalCode"].ToString(),
                                    street: reader["Address_Street"].ToString(),
                                    houseNumber: reader["Address_HouseNumber"].ToString()
                                ),
                                phoneNumber: reader["PhoneNumber"].ToString(),
                                email: reader["Email"].ToString(),
                                nrOfMembers: Convert.ToInt32(reader["nrOfMembers"])
                            );

                            customers.Add(customer);
                        }
                    }
                }

                return customers;
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
                string sql = "INSERT INTO Customer (Name, Email, PhoneNumber, Address_City, Address_PostalCode, Address_Street, Address_HouseNumber, Status) " +
                             "VALUES (@Name, @Email, @PhoneNumber, @City, @PostalCode, @Street, @HouseNumber, 1); SELECT SCOPE_IDENTITY();";

                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = sql;

                    cmd.Parameters.AddWithValue("@Name", customer.Name);
                    cmd.Parameters.AddWithValue("@Email", customer.Email);
                    cmd.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                    cmd.Parameters.AddWithValue("@City", customer.Address.City);
                    cmd.Parameters.AddWithValue("@PostalCode", customer.Address.PostalCode);
                    cmd.Parameters.AddWithValue("@Street", customer.Address.Street);
                    cmd.Parameters.AddWithValue("@HouseNumber", customer.Address.HouseNumber);

                    int customerId = Convert.ToInt32(cmd.ExecuteScalar());

             
                    customer.Id = customerId;
                }
            }
            catch (Exception ex)
            {
                throw new CustomerRepositoryException("addcustomer", ex);
            }
        }

        // Method to retrieve a customer by their ID from the database
        public Customer GetCustomerById(int customerId)
        {
            try
            {
                Customer customer = null;
                string sql = "SELECT [Id], [Name], [PhoneNumber], [Email], " +
                             "[Address_City], [Address_Street], [Address_PostalCode], [Address_HouseNumber] " +
                             "FROM [HotelDB].[dbo].[Customer] " +
                             "WHERE [Id] = @Id";

                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@Id", customerId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            customer = new Customer(
                                    id: Convert.ToInt32(reader["Id"]),
                                    name: reader["Name"].ToString(),
                                    address: new Address(
                                        city: reader["Address_City"].ToString(),
                                        postalCode: reader["Address_PostalCode"].ToString(),
                                        street: reader["Address_Street"].ToString(),
                                        houseNumber: reader["Address_HouseNumber"].ToString()
                                    ),
                                    phoneNumber: reader["PhoneNumber"].ToString(),
                                    email: reader["Email"].ToString());

                        }
                    }
                }

                return customer;
            }
            catch (Exception ex)
            {
                throw new CustomerRepositoryException("getcustomerbyid", ex);
            }
        }

        public void RemoveCustomerById(int customerId)
        {
            try
            {
                string sql = "UPDATE Customer SET Status = 0 WHERE Id = @Id";

                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@Id", customerId);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new CustomerRepositoryException("updatecustomerstatusbyid", ex);
            }
        }

        public void UpdateCustomer(Customer customer)
        {
            try
            {
                string sql =
                    "UPDATE Customer SET Name = @Name, Email = @Email, PhoneNumber = @Phone, " +
                    "Address_City = @City, Address_PostalCode = @PostalCode, Address_Street = @Street, Address_HouseNumber = @HouseNumber " +
                    "WHERE Id = @Id";

                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = sql;

                    cmd.Parameters.AddWithValue("@Id", customer.Id);
                    cmd.Parameters.AddWithValue("@Name", customer.Name);
                    cmd.Parameters.AddWithValue("@Email", customer.Email);
                    cmd.Parameters.AddWithValue("@Phone", customer.PhoneNumber);
                    cmd.Parameters.AddWithValue("@City", customer.Address.City);
                    cmd.Parameters.AddWithValue("@PostalCode", customer.Address.PostalCode);
                    cmd.Parameters.AddWithValue("@Street", customer.Address.Street);
                    cmd.Parameters.AddWithValue("@HouseNumber", customer.Address.HouseNumber);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new CustomerRepositoryException("updatecustomer", ex);
            }
        }

        public List<Customer> GetAllCustomers()
        {
            try
            {
                List<Customer> customers = new List<Customer>();
                string sql = "SELECT [Id], [Name], [PhoneNumber], [Email], [Address_City], [Address_PostalCode], [Address_Street], [Address_HouseNumber], [Status] FROM [HotelDB].[dbo].[Customer]";

                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = sql;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Customer customer = new Customer(
                                id: Convert.ToInt32(reader["Id"]),
                                name: reader["Name"].ToString(),
                                address: new Address(
                                    city: reader["Address_City"].ToString(),
                                    postalCode: reader["Address_PostalCode"].ToString(),
                                    street: reader["Address_Street"].ToString(),
                                    houseNumber: reader["Address_HouseNumber"].ToString()
                                ),
                                phoneNumber: reader["PhoneNumber"].ToString(),
                                email: reader["Email"].ToString()
                            );

                            customers.Add(customer);
                        }
                    }
                }

                return customers;  
            }
            catch (Exception ex)
            {
                throw new CustomerRepositoryException("getallcustomers", ex);
            }
        }

        public void AddMemberToCustomer(int customerId, Member member)
        {
            try
            {
                if (member.Birthday >= DateOnly.FromDateTime(DateTime.Now))
                {
                    throw new CustomerRepositoryException("Member's birthdate should be in the past");
                }

                string sql = "INSERT INTO Member (CustomerId, Name, Birthday) " +
                             "VALUES (@CustomerId, @Name, @Birthday)";

                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = sql;

                    cmd.Parameters.AddWithValue("@CustomerId", customerId);
                    cmd.Parameters.AddWithValue("@Name", member.Name);
                    cmd.Parameters.AddWithValue("@Birthday", member.Birthday.ToString("yyyy-MM-dd"));

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new CustomerRepositoryException("addmembertocustomer", ex);
            }
        }


    }
}
