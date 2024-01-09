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


        [Fact]
        public void UpdateCustomer_ShouldUpdateCustomerInDatabase()
        {
            // Arrange
            int customerIdToUpdate = 65; 

            CustomerRepository customerRepository = new CustomerRepository(connectionString);
            Customer existingCustomer = customerRepository.GetCustomerById(customerIdToUpdate);

            Assert.NotNull(existingCustomer);

            existingCustomer.Name = "UpdatedName";
            existingCustomer.Email = "updated@example.com";
            existingCustomer.PhoneNumber = "987654321";
            existingCustomer.Address.City = "UpdatedCity";
            existingCustomer.Address.PostalCode = "54321";
            existingCustomer.Address.Street = "UpdatedStreet";
            existingCustomer.Address.HouseNumber = "123";

            // Act
            customerRepository.UpdateCustomer(existingCustomer);

            // Assert
            Customer updatedCustomer = customerRepository.GetCustomerById(customerIdToUpdate);
            Assert.NotNull(updatedCustomer);
            Assert.Equal("UpdatedName", updatedCustomer.Name);
            Assert.Equal("updated@example.com", updatedCustomer.Email);
            Assert.Equal("987654321", updatedCustomer.PhoneNumber);
            Assert.Equal("UpdatedCity", updatedCustomer.Address.City);
            Assert.Equal("54321", updatedCustomer.Address.PostalCode);
            Assert.Equal("UpdatedStreet", updatedCustomer.Address.Street);
            Assert.Equal("123", updatedCustomer.Address.HouseNumber);
        }


     

    }
}




