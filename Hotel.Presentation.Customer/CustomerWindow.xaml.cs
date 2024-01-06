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
            InitializeComponent();
            this.CustomerUI = customerUI;
            if (CustomerUI != null)
            {
                IdTextBox.Text = CustomerUI.Id.HasValue ? CustomerUI.Id.ToString() : string.Empty;
                NameTextBox.Text = CustomerUI.Name;
                EmailTextBox.Text = CustomerUI.Email;
                PhoneTextBox.Text = CustomerUI.Phone;
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
                    ContactInfo contactInfo = new ContactInfo(EmailTextBox.Text, PhoneTextBox.Text, address);
                    Hotel.Domain.Model.Customer customer = new Hotel.Domain.Model.Customer(NameTextBox.Text, contactInfo);

                    // Pass an empty list as the last parameter for the constructor
                    CustomerUI = new CustomerUI(NameTextBox.Text, EmailTextBox.Text, address.ToString(), PhoneTextBox.Text, 0);

                    // Add to DB
                    customerManager.AddCustomer(customer);
                }
                else
                {
                    // Update existing customer
                    // Update DB
                    CustomerUI.Email = EmailTextBox.Text;
                    CustomerUI.Phone = PhoneTextBox.Text;
                    CustomerUI.Name = NameTextBox.Text;
                    // Update additional properties from CustomerUI if any

                    // Get the customer from the database using its ID
                    Hotel.Domain.Model.Customer existingCustomer = customerManager.GetCustomerById(CustomerUI.Id.Value);

                    if (existingCustomer != null)
                    {
                        // Update the existing customer's properties
                        existingCustomer.Name = NameTextBox.Text;
                        existingCustomer.Contact.Email = EmailTextBox.Text;
                        existingCustomer.Contact.Phone = PhoneTextBox.Text;
                        // Update additional properties from CustomerUI if any

                        // Save the changes to the database
                        customerManager.UpdateCustomer(existingCustomer);
                    }
                    else
                    {
                        MessageBox.Show("Customer not found in the database.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }

                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately (log, show an error message, etc.)
                MessageBox.Show($"Error adding/updating customer: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }


}
