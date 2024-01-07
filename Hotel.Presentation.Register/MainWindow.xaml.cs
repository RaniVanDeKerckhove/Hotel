using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Hotel.Domain.Managers;
using Hotel.Domain.Model;
using Hotel.Util;

namespace Hotel.UI.Register
{
    public partial class MainWindow : Window
    {
        private RegistrationManager registrationManager;
        private CustomerManager customerManager;
        private ActivityManager activityManager;
        private MemberManager memberManager;
        private Registration registration;
        private Customer customer;
        private Activity activity;
        private List<Member> members;

        public MainWindow()
        {
            InitializeComponent();

            // Initialize managers and set item sources
            registrationManager = new RegistrationManager(RepositoryFactory.RegistrationRepository);
            customerManager = new CustomerManager(RepositoryFactory.CustomerRepository);
            activityManager = new ActivityManager(RepositoryFactory.ActivityRepository);
            memberManager = new MemberManager(RepositoryFactory.MemberRepository);

            CustomerComboBox.ItemsSource = customerManager.GetCustomers(null);
            ActivitiesComboBox.ItemsSource = activityManager.GetActivities(null);

            // Initialize customer object
            customer = new Customer();
            customer.Members = new List<Member>();
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if all fields are filled
            if (CustomerComboBox.SelectedItem == null || ActivitiesComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please fill all fields");
                return;
            }

            // Initialize registration with customer and activity
            customer = CustomerComboBox.SelectedItem as Customer;
            activity = ActivitiesComboBox.SelectedItem as Activity;

            registration = new Registration(customer, activity);

            // Add registration and show success message
            registrationManager.AddRegistration(registration);
            MessageBox.Show("Registration completed successfully");
            Close();
        }

        private void CustomerComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            customer = CustomerComboBox.SelectedItem as Customer;
            if (customer != null)
            {
                members = memberManager.GetMembersByCustomerId(customer.Id);
                MembersListBox.ItemsSource = members;

                // Update other UI elements related to customer data if needed
                // For example, update TextBoxes, Labels, etc.
            }
        }

        private void MembersListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            customer.Members = MembersListBox.SelectedItems.Cast<Member>().ToList();

            registration = new Registration(customer, activity);
            // Update UI elements related to registration and member data
        }

        private void ActivitiesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MembersListBox.IsEnabled = true;
            MembersListBox.SelectedItems.Clear();
            activity = ActivitiesComboBox.SelectedItem as Activity;
            DateTextBox.Text = activity.Date.ToString();
            LocationTextBox.Text = activity.Location;
            AvailableSeatsTextBox.Text = activity.AvailablePlaces.ToString();
            customer.Members = new List<Member>();
            registration = new Registration(customer, activity);

           
        }

        



    }
}
