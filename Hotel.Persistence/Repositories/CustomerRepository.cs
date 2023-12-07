using Hotel.Domain.Interfaces;
using Hotel.Domain.Model;
using Hotel.Persistence.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Persistence.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private string connectionString;

        public CustomerRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IReadOnlyList<Customer> GetCustomers(string filter)
        {
            try
            {
                Dictionary<int,Customer> customers = new Dictionary<int, Customer>();
                string sql = "select t1.id,t1.name customername,t1.email,t1.phone,t1.address,t2.name membername,t2.birthday\r\nfrom customer t1 left join (select * from member where status=1) t2 on t1.id=t2.customerId\r\nwhere t1.status=1";
                if (!string.IsNullOrWhiteSpace(filter)) 
                {
                    sql += " and (t1.id like @filter or t1.name like @filter or t1.email like @filter)";
                }
                using(SqlConnection conn = new SqlConnection(connectionString)) 
                using(SqlCommand cmd = conn.CreateCommand()) 
                { 
                    conn.Open();
                    cmd.CommandText = sql;
                    if (!string.IsNullOrWhiteSpace(filter)) cmd.Parameters.AddWithValue("@filter",$"%{filter}%");
                        using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["ID"]);
                            if (!customers.ContainsKey(id))
                            {
                                Customer customer = new Customer(id, (string)reader["customername"], new ContactInfo((string)reader["email"], (string)reader["phone"], new Address((string)reader["address"])));
                                customers.Add(id, customer);
                            }
                            if (!reader.IsDBNull(reader.GetOrdinal("membername")))
                            {
                                Member member = new Member((string)reader["membername"], DateOnly.FromDateTime((DateTime)reader["birthday"]));
                                customers[id].AddMember(member);
                            }
                        }
                    }
                }
                return customers.Values.ToList();
            }
            catch(Exception ex)
            {
                throw new CustomerRepositoryException("getcustomer", ex);
            }
        }
        public void AddCustomer(Customer customer)
        {
            try
            {
                string sql = "INSERT INTO Customer(name,email,phone,address,status) output INSERTED.ID VALUES(@name,@email,@phone,@address,@status)";
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    SqlTransaction sqlTransaction = conn.BeginTransaction();
                    try
                    {
                        cmd.Transaction = sqlTransaction;
                        cmd.CommandText = sql;
                        cmd.Parameters.AddWithValue("@name", customer.Name);
                        cmd.Parameters.AddWithValue("@email", customer.Contact.Email);
                        cmd.Parameters.AddWithValue("@phone", customer.Contact.Phone);
                        cmd.Parameters.AddWithValue("@address", customer.Contact.Address.ToAddressLine());
                        cmd.Parameters.AddWithValue("@status", 1);
                        int id=(int)cmd.ExecuteScalar();
                        customer.Id = id;   
                        foreach (Member member in customer.GetMembers())
                        {
                            sql = "INSERT INTO Member(customerId,name,birthday,status) VALUES (@customerid,@name,@birthday,@status)";
                            cmd.CommandText = sql;
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@customerid", customer.Id);
                            cmd.Parameters.AddWithValue("@name", member.Name);
                            cmd.Parameters.AddWithValue("@birthday", member.Birthday.ToDateTime(TimeOnly.MinValue));
                            cmd.Parameters.AddWithValue("@status", 1);
                            cmd.ExecuteNonQuery();
                        }
                        sqlTransaction.Commit();
                    }
                    catch (Exception ex) { sqlTransaction.Rollback(); throw; }
                }
            }
            catch(Exception ex) { throw new CustomerRepositoryException("addcustomer", ex); }
        }
    }
}
