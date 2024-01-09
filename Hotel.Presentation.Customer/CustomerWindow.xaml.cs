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
       
        public CustomerUI CustomerUI { get; set; }

        // Managers 
        private CustomerManager customerManager;
        private MemberManager memberManager;

        private bool isUpdate;

        public CustomerWindow(bool isUpdate, CustomerUI customerUI)
        {
            InitializeComponent();

            // Initializeer managers
            customerManager = new CustomerManager(RepositoryFactory.CustomerRepository);
            memberManager = new MemberManager(RepositoryFactory.MemberRepository);

            //  initializeer CustomerUI
            this.isUpdate = isUpdate;
            this.CustomerUI = customerUI;

            // idien update
            // geeft de textboxes de juiste waarden
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

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //add en update
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CustomerUI == null)
                {
                    // nieuwe customer
                    Address address = new Address(CityTextBox.Text, StreetTextBox.Text, ZipTextBox.Text, HouseNumberTextBox.Text);
                    CustomerUI = new CustomerUI(
                        name: NameTextBox.Text,
                        email: EmailTextBox.Text,
                        address: $"{address.City}, {address.Street}, {address.PostalCode}, {address.HouseNumber}",
                        phone: PhoneTextBox.Text,
                        nrOfMembers: 0
                    );

                    // toevoegen aan de database
                    Domain.Model.Customer newCustomer = new Domain.Model.Customer
                    {
                        Name = CustomerUI.Name,
                        Email = CustomerUI.Email,
                        PhoneNumber = CustomerUI.Phone,
                        Address = address
                    };

                    customerManager.AddCustomer(newCustomer);

                    // ophalen van de customer uit de database
                    newCustomer = customerManager.GetCustomerById(newCustomer.Id);

                    // customUi update met info van de database
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
                    // update bestaande customer
                    CustomerUI.Email = EmailTextBox.Text;
                    CustomerUI.Phone = PhoneTextBox.Text; 
                    CustomerUI.Name = NameTextBox.Text;

                    // update customer in database
                    Domain.Model.Customer existingCustomer = customerManager.GetCustomerById(CustomerUI.Id ?? 0);
                    existingCustomer.Email = CustomerUI.Email;
                    existingCustomer.PhoneNumber = CustomerUI.Phone;
                    existingCustomer.Name = CustomerUI.Name;

                    customerManager.UpdateCustomer(existingCustomer);
                }

                // succesvol
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
