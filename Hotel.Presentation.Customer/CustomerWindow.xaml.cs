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
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        public CustomerUI CustomerUI { get; set; }
        private CustomerManager customerManager;
        public CustomerWindow(CustomerUI customerUI)
        {
            InitializeComponent();
            customerManager = new CustomerManager(RepositoryFactory.CustomerRepository);
            this.CustomerUI = customerUI;
            if (CustomerUI != null)
            {
                IdTextBox.Text = CustomerUI.Id.ToString();
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
                    ContactInfo ContactInfo = new ContactInfo(EmailTextBox.Text, PhoneTextBox.Text, address);
                    Hotel.Domain.Model.Customer customer = new Hotel.Domain.Model.Customer(NameTextBox.Text, ContactInfo);

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
