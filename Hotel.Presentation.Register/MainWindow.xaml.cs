using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Hotel.Domain.Managers;
using Hotel.Domain.Model;
using Hotel.Persistence.Repositories;
using Hotel.Presentation.Register;
using Hotel.Util;

namespace Hotel.UI.Register
{
    public partial class MainWindow : Window
    {
        private ActivityManager activityManager;
        private MemberRepository memberRepository; // Add this line
        private ObservableCollection<Member> selectedMembers;
        private CustomerManager customerManager;  // Add this line



        public MainWindow()
        {
            InitializeComponent();
            activityManager = new ActivityManager(RepositoryFactory.ActivityRepository);
            memberRepository = (MemberRepository)RepositoryFactory.MemberRepository;
            customerManager = new CustomerManager(RepositoryFactory.CustomerRepository);  // Add this line


            RefreshActivities();
        }


        private void CustomerComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // get all customers from the customerManager using GetAllCustomers
            List<Customer> customers = customerManager.GetAllCustomers();
            // set the combobox itemsource to the list of customers
            CustomerComboBox.ItemsSource = customers;
            // set the selected value to the first customer
            CustomerComboBox.SelectedIndex = 0;
        }
        // show member by selected customer
        private void ShowMembersButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Fetch members for the selected customer using customerManager
                int selectedCustomerId = (int)CustomerComboBox.SelectedValue;
                Customer selectedCustomer = customerManager.GetCustomerById(selectedCustomerId);

                if (selectedCustomer != null)
                {
                    // Update the selectedMembers collection
                    selectedMembers.Clear();
                    foreach (var member in selectedCustomer.Members)
                    {
                        selectedMembers.Add(member);
                    }
                }
                else
                {
                    MessageBox.Show("Error fetching customer information.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }



        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            // Show activities that match the search
            ActivitiesDataGrid.ItemsSource = activityManager.GetActivities(SearchTextBox.Text);
        }

        private void RefreshActivities()
        {
            // Show all activities
            ActivitiesDataGrid.ItemsSource = activityManager.GetActivities(null);
        }

        private void ShowAllButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshActivities();
        }
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Fetch selected members and calculate total cost
                List<Member> selectedMembersList = new List<Member>();
                foreach (var item in MembersListBox.SelectedItems)
                {
                    if (item is Member selectedMember)
                    {
                        selectedMembersList.Add(selectedMember);
                    }
                }

                // Fetch the selected activity
                Activity selectedActivity = (Activity)ActivitiesDataGrid.SelectedItem;

                if (selectedActivity != null)
                {
                    // Calculate the total cost
                    decimal totalCost = selectedMembersList.Count * selectedActivity.PriceAdult;

                    // Show the RegisterWindow with selected members and total cost
                    RegisterWindow registerWindow = new RegisterWindow(selectedMembersList, totalCost);
                    registerWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Please select an activity.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }








    }
}
