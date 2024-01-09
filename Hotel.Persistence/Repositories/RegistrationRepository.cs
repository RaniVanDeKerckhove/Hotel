using Hotel.Domain.Model;
using Hotel.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;
using Hotel.Persistence.Exceptions;

// Repository class for handling data operations related to registrations
public class RegistrationRepository : IRegistrationRepository
{
    private readonly string connectionString;

    // Constructor to initialize the database connection string
    public RegistrationRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    // Method to add a new registration to the database
    public void AddRegistration(Registration registration)
    {
        try
        {
            // SQL query to insert a new registration
            string sql =
            "INSERT INTO Registration (CustomerId, ActivityId, TotalPrice, NumberOfAdults, NumberOfChildren) VALUES (@CustomerId, @ActivityId, @TotalCost, @NumberOfAdults, @NumberOfChildren)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();

                // Set the SQL command and add parameters
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@customerId", registration.Customer.Id);
                cmd.Parameters.AddWithValue("@ActivityId", registration.Activity.Id);
                cmd.Parameters.AddWithValue("@NumberOfAdults", registration.NumberOfAdults);
                cmd.Parameters.AddWithValue("@NumberOfChildren", registration.NumberOfChildren);
                cmd.Parameters.AddWithValue("@TotalCost", registration.Price);

                // Execute the SQL command
                cmd.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions and throw a custom exception
            throw new RegistrationRepositoryException("addregistration", ex);
        }
    }

    // Method to remove a registration by its ID from the database
    public void RemoveRegistrationById(int registrationId)
    {
        try
        {
            // SQL query to delete a registration by ID
            string sql = "DELETE FROM Registration WHERE RegistrationId = @registrationId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();

                // Set the SQL command and add parameters
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@registrationId", registrationId);

                // Execute the SQL command
                cmd.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions and throw a custom exception
            throw new RegistrationRepositoryException("removeregistration", ex);
        }
    }
}
