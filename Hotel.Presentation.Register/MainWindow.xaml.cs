using Hotel.Domain.Managers;
using Hotel.Domain.Model;
using Hotel.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace HotelProject.UI.Register
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
            customer._members = new List<Member>();
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
                // Update members based on selected customer
                members = memberManager.GetMembersByCustomerId(customer.Id);
                MembersListBox.ItemsSource = members;
            }
        }

        private void ActivitiesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Handle selection change in ActivitiesComboBox
            MembersListBox.IsEnabled = true;
            MembersListBox.SelectedItems.Clear();
            activity = ActivitiesComboBox.SelectedItem as Activity;

            // Update UI elements based on selected activity
            DateTextBox.Text = activity.Date.ToString();
            LocationTextBox.Text = activity.Location;
            AvailableSeatsTextBox.Text = activity.AvailablePlaces.ToString();

            // Initialize registration with customer and activity
            customer._members = new List<Member>();
            registration = new Registration(customer, activity);

            // Update UI elements based on registration details
            UpdateRegistrationUI();
        }

        private void MembersListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Handle selection change in MembersListBox
            customer._members = new List<Member>();

            foreach (Member member in MembersListBox.SelectedItems)
            {
                customer._members.Add(member);
            }

            // Update UI elements based on registration details
            UpdateRegistrationUI();
        }

        private void UpdateRegistrationUI()
        {
            // Update UI elements based on registration details
            if (activity.Discount == null || activity.Discount == 0)
            {
                SubtotalAdultsTextBlock.Text = registration.AdultCost.ToString() + $"       {registration.NumberOfAdults} adults";
                SubtotalChildrenTextBlock.Text = registration.ChildCost.ToString() + $"       {registration.NumberOfChildren} children";
                DiscountTextBlock.Text = " ";
                TotalCostTextBlock.Text = registration.TotalPrice.ToString();
            }
            else
            {
                SubtotalAdultsTextBlock.Text = $"{registration.AdultCost.ToString()} ({activity.PriceAdult * registration.NumberOfAdults} per adult)       {registration.NumberOfAdults} adults";
                SubtotalChildrenTextBlock.Text = $"{registration.ChildCost.ToString()} ({activity.PriceChild * registration.NumberOfChildren})       {registration.NumberOfChildren} children";
                DiscountTextBlock.Text = $"Discount: {activity.Discount}%";
                TotalCostTextBlock.Text = registration.TotalPrice.ToString();
            }
        }
    }
}
