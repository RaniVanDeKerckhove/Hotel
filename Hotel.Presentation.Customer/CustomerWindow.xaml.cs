using Hotel.Domain.Interfaces;
using Hotel.Domain.Managers;
using Hotel.Domain.Model;
using Hotel.Persistence.Repositories;
using Hotel.Presentation.Customer.Model;
using Hotel.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Hotel.Presentation.Customer
{
    public partial class CustomerWindow : Window
    {
        // Add this line at the beginning of your class
        public CustomerUI CustomerUI { get; set; }

        private CustomerManager customerManager;
        private MemberManager memberManager;
        private CustomerUI customerUI;
        private bool isUpdate;

        public CustomerWindow(bool isUpdate, CustomerUI customerUI)
        {
            InitializeComponent();
            customerManager = new CustomerManager(RepositoryFactory.CustomerRepository);
            memberManager = new MemberManager(RepositoryFactory.MemberRepository);
            this.CustomerUI = customerUI;
            if (CustomerUI != null)
            {
                IdTextBox.Text = CustomerUI.Id.ToString();
                NameTextBox.Text = CustomerUI.Name;
                EmailTextBox.Text = CustomerUI.Email;
                PhoneTextBox.Text = CustomerUI.Phone;

                // Extract address details and set in respective textboxes
                CityTextBox.Text = CustomerUI.Municipality;
                StreetTextBox.Text = CustomerUI.Street;
                ZipTextBox.Text = CustomerUI.PostalCode;
                HouseNumberTextBox.Text = CustomerUI.HouseNumber;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CustomerUI == null)
                {
                    // New customer
                    Address address = new Address(CityTextBox.Text, StreetTextBox.Text, ZipTextBox.Text, HouseNumberTextBox.Text);
                    CustomerUI = new CustomerUI(
                        null,
                        NameTextBox.Text,
                        EmailTextBox.Text,
                        CityTextBox.Text,
                        StreetTextBox.Text,
                        ZipTextBox.Text,
                        HouseNumberTextBox.Text,
                        PhoneTextBox.Text,
                        0
                    );

                    // Add the new customer to the database
                    Domain.Model.Customer newCustomer = new Domain.Model.Customer
                    {
                        Name = CustomerUI.Name,
                        Email = CustomerUI.Email,
                        PhoneNumber = CustomerUI.Phone,
                        Address = address
                    };

                    customerManager.AddCustomer(newCustomer);

                    // Retrieve the updated customer (including the generated ID)
                    newCustomer = customerManager.GetCustomerById(newCustomer.Id);

                    // Update the CustomerUI with the database information
                    CustomerUI = new CustomerUI(
                        newCustomer.Id,
                        newCustomer.Name,
                        newCustomer.Email,
                        newCustomer.Address.City,
                        newCustomer.Address.Street,
                        newCustomer.Address.PostalCode,
                        newCustomer.Address.HouseNumber,
                        newCustomer.PhoneNumber,
                        0
                    );
                }
                else
                {
                    // Update existing customer
                    CustomerUI.Email = EmailTextBox.Text;
                    CustomerUI.Phone = PhoneTextBox.Text;
                    CustomerUI.Name = NameTextBox.Text;

                    // Update the customer in the database
                    Domain.Model.Customer existingCustomer = customerManager.GetCustomerById(CustomerUI.Id ?? 0);
                    existingCustomer.Email = CustomerUI.Email;
                    existingCustomer.PhoneNumber = CustomerUI.Phone;
                    existingCustomer.Name = CustomerUI.Name;

                    customerManager.UpdateCustomer(existingCustomer);
                }

                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}
