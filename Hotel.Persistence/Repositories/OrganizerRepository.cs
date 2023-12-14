using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Hotel.Domain.Model; // Update with your actual namespace
using Hotel.Domain.Repositories;

namespace Hotel.Persistence.Repositories
{
    public class OrganizerRepository : IOrganizerRepository
    {
        private readonly string connectionString;

        public OrganizerRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void AddOrganizer(Organizer organizer)
        {
            throw new NotImplementedException();
        }

        public List<Organizer> GetAllOrganizers()
        {
            List<Organizer> organizers = new List<Organizer>();

            // Query to get organizers with contact info and address
            string query = "SELECT o.[OrganizerId], o.[Name], ci.[Email], ci.[Phone], " +
                           "a.[City], a.[PostalCode], a.[Street], a.[HouseNumber] " +
                           "FROM [Hotel].[dbo].[Organizer] o " +
                           "INNER JOIN [Hotel].[dbo].[ContactInfo] ci ON o.[ContactInfo_Email] = ci.[Email] " +
                           "INNER JOIN [Hotel].[dbo].[Address] a ON ci.[Address_City] = a.[City]";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Organizer organizer = new Organizer(
                            organizerId: Convert.ToInt32(reader["OrganizerId"]),
                            name: reader["Name"].ToString(),
                            contactInfo: new ContactInfo(
                                email: reader["Email"].ToString(),
                                phone: reader["Phone"].ToString(),
                                address: new Address(
                                    city: reader["City"].ToString(),
                                    postalCode: reader["PostalCode"].ToString(),
                                    street: reader["Street"].ToString(),
                                    houseNumber: reader["HouseNumber"].ToString()
                                )
                            )
                        );

                        organizers.Add(organizer);
                    }
                }
            }
            catch (SqlException ex)
            {
                // Handle exception
                Console.WriteLine($"Error: {ex.Message}");
            }

            return organizers;
        }

        public Organizer GetOrganizerById(int organizerId)
        {
            throw new NotImplementedException();
        }

        public void RemoveOrganizerById(int organizerId)
        {
            throw new NotImplementedException();
        }
    }
}
