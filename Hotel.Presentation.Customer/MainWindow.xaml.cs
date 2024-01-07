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
                    c.Address.ToString(),
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
            foreach (Domain.Model.Customer c in customerManager.GetCustomers(null))
            {
                List<MemberUI> membersUI = new List<MemberUI>();
                if (c.Members != null)
                {
                    foreach (Member m in c.Members)
                    {
                        membersUI.Add(new MemberUI(m.Name, m.Birthday));
                    }
                }
                customersUIs.Add(new CustomerUI(c.Id, c.Name, c.Email, c.Address.ToString(), c.PhoneNumber, c.NrOfMembers));
            }
        }
        private void AddMemberButton_Click(object sender, RoutedEventArgs e)
        {
            // Ensure a customer is selected
            if (CustomerDataGrid.SelectedItem != null)
            {
                // Get the selected customer
                CustomerUI selectedCustomer = (CustomerUI)CustomerDataGrid.SelectedItem;

                // Open the MemberWindow to add a new member
                MemberWindow memberWindow = new MemberWindow();
                if (memberWindow.ShowDialog() == true)
                {
                    // Extract DateTime value from the DatePicker
                    DateTime selectedDate = memberWindow.DatePicker.SelectedDate.GetValueOrDefault();

                   
                    // Create a new Member object
                    Member newMember = new Member(
                                               memberWindow.NameTextBox.Text,
                                                                      DateOnly.FromDateTime(selectedDate)
                                                                  );

                    try
                    {
                        // Add the new member to the selected customer
                        customerManager.AddMemberToCustomer(selectedCustomer.Id ?? 0, newMember);

                        // Refresh the UI to reflect the changes
                        GetDatabaseInfo();
                        Refresh();
                    }
                    catch (CustomerManagerException ex)
                    {
                        // Handle the exception or display an error message
                        MessageBox.Show($"Error adding member: {ex.Message}", "Error");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a customer before adding a member.", "Add Member");
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