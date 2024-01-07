using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Hotel.Domain.Exceptions;
using Hotel.Domain.Managers;
using Hotel.Domain.Model;
using Hotel.Presentation.Customer.Model;
using Hotel.Util;

namespace Hotel.Presentation.Customer
{
    public partial class MainWindow : Window
    {
        private CustomerManager customerManager;
        private ObservableCollection<CustomerUI> customersUIs = new ObservableCollection<CustomerUI>();

        public MainWindow()
        {
            InitializeComponent();
            customerManager = new CustomerManager(RepositoryFactory.CustomerRepository);
            GetDatabaseInfo();
            Refresh();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            // Clear the existing collection
            customersUIs.Clear();

            // Get the search text
            string searchText = SearchTextBox.Text;

            // Filter customers based on the search text
            var filteredCustomers = customerManager.GetCustomers(searchText)
                .Select(c => new CustomerUI(
                    c.Id,
                    c.Name,
                    c.Email,
                    c.Address.City,          // Update to use City property from Address
                    c.Address.Street,        // Update to use Street property from Address
                    c.Address.PostalCode,    // Update to use PostalCode property from Address
                    c.Address.HouseNumber,   // Update to use HouseNumber property from Address
                    c.PhoneNumber,
                    c.Members.Count
                ));

            // Add filtered customers to the ObservableCollection
            foreach (var customerUI in filteredCustomers)
            {
                customersUIs.Add(customerUI);
            }
        }




        private void MenuItemAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            CustomerWindow w = new CustomerWindow(false, null);
            if (w.ShowDialog() == true)
                customersUIs.Add(w.CustomerUI);
            Refresh();
        }

        private void MenuItemDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Customer not selected", "Delete");
            }
            else
            {
                int customerId = ((CustomerUI)CustomerDataGrid.SelectedItem).Id ?? 0;

                try
                {
                    // Call the RemoveCustomerById method
                    customerManager.RemoveCustomerById(customerId);
                    customersUIs.Remove((CustomerUI)CustomerDataGrid.SelectedItem);
                    Refresh();
                }
                catch (CustomerManagerException ex)
                {
                    // Handle the exception or display an error message
                    MessageBox.Show($"Error deleting customer: {ex.Message}", "Error");
                }
            }
        }

        private void MenuItemUpdateCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerDataGrid.SelectedItem == null)
                MessageBox.Show("Customer not selected", "Update");
            else
            {
                CustomerWindow w = new CustomerWindow(true, (CustomerUI)CustomerDataGrid.SelectedItem);
                w.ShowDialog();
            }
            Refresh();
        }

        public void GetDatabaseInfo()
        {
            try
            {
                foreach (Domain.Model.Customer c in customerManager.GetCustomers(null))
                {
                    List<MemberUI> membersUI = new List<MemberUI>();

                    // Check if Members is not null before accessing its count
                    if (c.Members != null)
                    {
                        foreach (Member m in c.Members)
                        {
                            membersUI.Add(new MemberUI(m.Name, m.Birthday));
                        }
                    }

                    if (c.Address != null)
                    {
                        string city = c.Address.City ?? string.Empty;
                        string street = c.Address.Street ?? string.Empty;
                        string postalCode = c.Address.PostalCode ?? string.Empty;
                        string houseNumber = c.Address.HouseNumber ?? string.Empty;

                        // Use the count of membersUI instead of directly accessing c.Members.Count
                        customersUIs.Add(new CustomerUI(c.Id, c.Name, c.Email, city, street, postalCode, houseNumber, c.PhoneNumber, membersUI.Count));
                    }
                    else
                    {
                        Console.WriteLine($"Address is null for customer with ID: {c.Id}");
                        // If you want to skip customers with null address, you can continue to the next iteration
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or display the exception details
                Console.WriteLine($"Exception in GetDatabaseInfo: {ex.Message}");
            }
        }


        public void Refresh()
        {
            CustomerDataGrid.ItemsSource = customersUIs;
            CustomerDataGrid.Loaded += (sender, e) =>
            {
                CustomerDataGrid.Columns[5].Visibility = Visibility.Hidden;
                CustomerDataGrid.Columns[7].Visibility = Visibility.Hidden;
                CustomerDataGrid.Columns[8].Visibility = Visibility.Hidden;
                CustomerDataGrid.Columns[9].Visibility = Visibility.Hidden;
                CustomerDataGrid.Columns[10].Visibility = Visibility.Hidden;
            };
        }


    }
}