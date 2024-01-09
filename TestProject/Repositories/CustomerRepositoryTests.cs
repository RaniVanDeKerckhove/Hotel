using System;
using Hotel.Domain.Model;
using Hotel.Persistence.Repositories;
using Xunit;

namespace TestProject.Repositories
{
    public class CustomerRepositoryTests
    {
        // Please replace "your_connection_string" with the actual connection string for your database
        private readonly string connectionString =
            "Data Source=MSI\\SQLEXPRESS;Initial Catalog=HotelDB;Integrated Security=True;TrustServerCertificate=True";

        [Fact]
        public void AddCustomer_ValidCustomer_ShouldAddCustomerToDatabase()
        {
            // Arrange
            var repository = new CustomerRepository(connectionString);
            var customer = new Customer
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "1234567890",
                Address = new Address
                {
                    City = "City",
                    PostalCode = "12345",
                    Street = "Street",
                    HouseNumber = "123"
                }
            };

            // Act
            repository.AddCustomer(customer);

            // Assert
            var retrievedCustomer = repository.GetCustomerById(customer.Id);
            Assert.NotNull(retrievedCustomer);
            Assert.Equal(customer.Name, retrievedCustomer.Name);
            Assert.Equal(customer.Email, retrievedCustomer.Email);
            Assert.Equal(customer.PhoneNumber, retrievedCustomer.PhoneNumber);
            Assert.Equal(customer.Address.City, retrievedCustomer.Address.City);
            Assert.Equal(customer.Address.PostalCode, retrievedCustomer.Address.PostalCode);
            Assert.Equal(customer.Address.Street, retrievedCustomer.Address.Street);
            Assert.Equal(customer.Address.HouseNumber, retrievedCustomer.Address.HouseNumber);

            // Cleanup
            repository.RemoveCustomerById(customer.Id);
        }
        //remove test


        [Fact]
        public void AddMemberToCustomer_ValidMember_ShouldAddMemberToCustomer()
        {
            // Arrange
            var repository = new CustomerRepository(connectionString);
            var member = new Member("John Doe", new DateOnly(1990, 1, 1));
            var customerId = 55; // Replace this with the actual customer ID you want to use

            // Act
            repository.AddMemberToCustomer(customerId, member);

            // Assert
            var retrievedCustomer = repository.GetCustomerById(customerId);
            Assert.NotNull(retrievedCustomer);
            Assert.Equal(customerId, retrievedCustomer.Id); // Ensure the correct customer is retrieved
            Assert.Equal(member.Name, retrievedCustomer.Members.First().Name); // Assuming Members is a collection
            // Add more assertions if needed

            // Cleanup (if necessary)
            // repository.RemoveMemberFromCustomer(customerId, memberId);
        }



    }
}