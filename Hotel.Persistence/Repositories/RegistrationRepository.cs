using Hotel.Domain.Model;
using Hotel.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;
using Hotel.Persistence.Exceptions;

public class RegistrationRepository : IRegistrationRepository
{
    private readonly string connectionString;

    public RegistrationRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public void AddRegistration(Registration registration)
    {
        try
        {
            string sql =
            "INSERT INTO Registration (CustomerId, ActivityId, TotalPrice, NumberOfAdults, NumberOfChildren) VALUES (@CustomerId, @ActivityId, @TotalPrice, @NumberOfAdults, @NumberOfChildren)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@customerId", registration.Customer.Id);
                cmd.Parameters.AddWithValue("@ActivityId", registration.Activity.Id);
                cmd.Parameters.AddWithValue("@NumberOfAdults", registration.NumberOfAdults);
                cmd.Parameters.AddWithValue("@NumberOfChildren", registration.NumberOfChildren);
                cmd.Parameters.AddWithValue("@TotalCost", registration.Price);

                cmd.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            throw new RegistrationRepositoryException("addregistration", ex);
        }
    }

   


    public void RemoveRegistrationById(int registrationId)
    {

        try
        {
            string sql = "DELETE FROM Registration WHERE RegistrationId = @registrationId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@registrationId", registrationId);

                cmd.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            throw new RegistrationRepositoryException("removeregistration", ex);
        }
    }

    // Implement other methods as needed
}