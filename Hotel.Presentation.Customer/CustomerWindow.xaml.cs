using Hotel.Domain.Interfaces;
using Hotel.Domain.Managers;
using Hotel.Domain.Model;
using Hotel.Persistence.Repositories;
using Hotel.Presentation.Customer.Model;
using Hotel.Util;
using System;
using System.Windows;

namespace Hotel.Presentation.Customer
{
    public partial class CustomerWindow : Window
    {
        // Property to hold the CustomerUI data
        public CustomerUI CustomerUI { get; set; }

        // Managers for handling customer and member operations
        private CustomerManager customerManager;
        private MemberManager memberManager;

        // Flag to determine if it's an update or a new customer
        private bool isUpdate;

        // Constructor for the CustomerWindow
        public CustomerWindow(bool isUpdate, CustomerUI customerUI)
        {
            InitializeComponent();

            // Initialize managers
            customerManager = new CustomerManager(RepositoryFactory.CustomerRepository);
            memberManager = new MemberManager(RepositoryFactory.MemberRepository);

            // Set the flag and initialize CustomerUI
            this.isUpdate = isUpdate;
            this.CustomerUI = customerUI;

            // If updating, populate the UI with existing customer data
            if (CustomerUI != null)
            {
                IdTextBox.Text = CustomerUI.Id.ToString();
                NameTextBox.Text = CustomerUI.Name;
                EmailTextBox.Text = CustomerUI.Email;
                PhoneTextBox.Text = CustomerUI.Phone;
                CityTextBox.Text = CustomerUI.Municipality;
                ZipTextBox.Text = CustomerUI.ZipCode;
                StreetTextBox.Text = CustomerUI.Street;
                HouseNumberTextBox.Text = CustomerUI.HouseNumber;
            }
        }

        // Event handler for the Cancel button click
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        // Event handler for the Add or Update button click
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CustomerUI == null)
                {
                    // Creating a new customer
                    Address address = new Address(CityTextBox.Text, StreetTextBox.Text, ZipTextBox.Text, HouseNumberTextBox.Text);
                    CustomerUI = new CustomerUI(
                        name: NameTextBox.Text,
                        email: EmailTextBox.Text,
                        address: $"{address.City}, {address.Street}, {address.PostalCode}, {address.HouseNumber}",
                        phone: PhoneTextBox.Text,
                        nrOfMembers: 0
                    );

                    // Adding the new customer to the database
                    Domain.Model.Customer newCustomer = new Domain.Model.Customer
                    {
                        Name = CustomerUI.Name,
                        Email = CustomerUI.Email,
                        PhoneNumber = CustomerUI.Phone,
                        Address = address
                    };

                    customerManager.AddCustomer(newCustomer);

                    // Retrieving the updated customer (including the generated ID)
                    newCustomer = customerManager.GetCustomerById(newCustomer.Id);

                    // Updating the CustomerUI with the database information
                    CustomerUI = new CustomerUI(
                        newCustomer.Id,
                        newCustomer.Name,
                        newCustomer.Email,
                        $"{newCustomer.Address.City}, {newCustomer.Address.Street}, {newCustomer.Address.PostalCode}, {newCustomer.Address.HouseNumber}",
                        newCustomer.PhoneNumber,
                        0
                    );
                }
                else
                {
                    // Updating an existing customer
                    CustomerUI.Email = EmailTextBox.Text;
                    CustomerUI.Phone = PhoneTextBox.Text; // Update with the correct phone number
                    CustomerUI.Name = NameTextBox.Text;

                    // Update the customer in the database
                    Domain.Model.Customer existingCustomer = customerManager.GetCustomerById(CustomerUI.Id ?? 0);
                    existingCustomer.Email = CustomerUI.Email;
                    existingCustomer.PhoneNumber = CustomerUI.Phone;
                    existingCustomer.Name = CustomerUI.Name;

                    customerManager.UpdateCustomer(existingCustomer);
                }

                // Setting the DialogResult to true to indicate success and closing the window
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                // Displaying an error message if an exception occurs
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
