using Hotel.Domain.Model;
using Hotel.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

public class RegistrationRepository : IRegistrationRepository
{
    private readonly string connectionString;

    public RegistrationRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public void AddRegistration(Registration registration)
    {
        throw new NotImplementedException();
    }

    public List<Registration> GetAllRegistrations()
    {
        List<Registration> registrations = new List<Registration>();

        // Query to get registrations with related customer and activity data
        string query = "SELECT r.[RegistrationId], r.[CustomerId], r.[ActivityId], " +
               "r.[TotalPrice], r.[ChildCost], r.[AdultCost], " +
               "r.[NumberOfAdults], r.[NumberOfChildren], " +
               "c.[Name] AS CustomerName, a.[Name] AS ActivityName, " +
               "c.[ContactInfo_Email], " +
               "ci.[Email], ci.[Phone], " +
               "a.[Description], a.[Date], a.[Duration], " +
               "a.[AvailablePlaces], a.[PriceAdult], a.[PriceChild], " +
               "a.[Discount], a.[Location], " +
               "ad.[City], ad.[PostalCode], ad.[Street], ad.[HouseNumber] " +
               "FROM [Hotel].[dbo].[Registration] r " +
               "INNER JOIN [Hotel].[dbo].[Customer] c ON r.[CustomerId] = c.[Id] " +
               "INNER JOIN [Hotel].[dbo].[Activity] a ON r.[ActivityId] = a.[Id] " +
               "LEFT JOIN [Hotel].[dbo].[ContactInfo] ci ON c.[ContactInfo_Email] = ci.[Email] " +
               "LEFT JOIN [Hotel].[dbo].[Address] ad ON ci.[Address_City] = ad.[City]";


        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Customer customer = new Customer(
                        id: Convert.ToInt32(reader["CustomerId"]),
                        name: reader["CustomerName"].ToString(),
                        contact: new ContactInfo(
                            email: reader["ContactInfo_Email"].ToString(),
                            phone: reader["Phone"].ToString(),
                            address: new Address(
                                city: reader["City"].ToString(),
                                street: reader["Street"].ToString(),
                                postalCode: reader["PostalCode"].ToString(),
                                houseNumber: reader["HouseNumber"].ToString()
                            )
                        )
                    );

                    Activity activity = new Activity(
    id: Convert.ToInt32(reader["ActivityId"]),
    name: reader["ActivityName"].ToString(),
    description: reader["Description"] == DBNull.Value ? string.Empty : reader["Description"].ToString(),
    date: reader["Date"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["Date"]),
    duration: reader["Duration"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Duration"]),
    availablePlaces: reader["AvailablePlaces"] == DBNull.Value ? 0 : Convert.ToInt32(reader["AvailablePlaces"]),
    priceAdult: reader["PriceAdult"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["PriceAdult"]),
    priceChild: reader["PriceChild"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["PriceChild"]),
    discount: reader["Discount"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["Discount"]),
    location: reader["Location"] == DBNull.Value ? string.Empty : reader["Location"].ToString()
);


                    Registration registration = new Registration(customer, activity)
                    {
                        RegistrationId = Convert.ToInt32(reader["RegistrationId"]),
                        TotalPrice = Convert.ToDecimal(reader["TotalPrice"]),
                        ChildCost = Convert.ToDecimal(reader["ChildCost"]),
                        AdultCost = Convert.ToDecimal(reader["AdultCost"]),
                        NumberOfAdults = Convert.ToInt32(reader["NumberOfAdults"]),
                        NumberOfChildren = Convert.ToInt32(reader["NumberOfChildren"]),
                    };

                    registrations.Add(registration);
                }
            }
        }
        catch (SqlException ex)
        {
            // Handle exception
            Console.WriteLine($"Error: {ex.Message}");
        }

        return registrations;
    }

    public Registration GetRegistrationById(int registrationId)
    {
        throw new NotImplementedException();
    }

    public void RemoveRegistrationById(int registrationId)
    {
        throw new NotImplementedException();
    }

    // Implement other methods as needed
}
