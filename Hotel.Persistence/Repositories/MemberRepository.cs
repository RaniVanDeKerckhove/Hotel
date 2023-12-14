using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Hotel.Domain.Interfaces;
using Hotel.Domain.Model;
using Hotel.Domain.Repositories;
using Hotel.Persistence.Exceptions;

namespace Hotel.Persistence.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly string connectionString;

        public MemberRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void AddMember(Member member)
        {
            try
            {
                string sql = "INSERT INTO Member (CustomerId, Name, Birthday) VALUES (@customerId, @name, @birthday)";

                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@customerId", member.CustomerId); 
                    cmd.Parameters.AddWithValue("@name", member.Name);
                    cmd.Parameters.AddWithValue("@birthday", member.Birthday.ToDateTime(TimeOnly.MinValue));

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new MemberRepositoryException("addmember", ex);
            }
        }

        public List<Member> GetAllMembers()
        {
            try
            {
                List<Member> members = new List<Member>();
                string sql = "SELECT CustomerId, Name, Birthday FROM Member";

                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = sql;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int customerId = Convert.ToInt32(reader["CustomerId"]);
                            string name = (string)reader["Name"];
                            DateOnly birthday = DateOnly.FromDateTime((DateTime)reader["Birthday"]);

                            Member member = new Member(customerId, name, birthday); // Replace with the actual constructor parameters from your Member class
                            members.Add(member);
                        }
                    }
                }

                return members;
            }
            catch (Exception ex)
            {
                throw new MemberRepositoryException("getallmembers", ex);
            }
        }

        public Member GetMemberByName(string memberName)
        {
            try
            {
                Member member = null;
                string sql = "SELECT CustomerId, Name, Birthday FROM Member WHERE Name = @memberName";

                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@memberName", memberName);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int customerId = Convert.ToInt32(reader["CustomerId"]);
                            string name = (string)reader["Name"];
                            DateOnly birthday = DateOnly.FromDateTime((DateTime)reader["Birthday"]);

                            member = new Member(customerId, name, birthday); // Replace with the actual constructor parameters from your Member class
                        }
                    }
                }

                return member;
            }
            catch (Exception ex)
            {
                throw new MemberRepositoryException("getmemberbyname", ex);
            }
        }

        public void RemoveMemberByName(string memberName)
        {
            try
            {
                string sql = "DELETE FROM Member WHERE Name = @memberName";

                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@memberName", memberName);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new MemberRepositoryException("removememberbyname", ex);
            }
        }
    }
}
