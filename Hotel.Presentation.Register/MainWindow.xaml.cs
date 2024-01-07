using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Hotel.Domain.Managers;
using Hotel.Domain.Model;
using Hotel.Util;


namespace Hotel.Presentation.Register
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
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
            InitializeManagers();
            PopulateComboBoxes();
        }

        private void InitializeManagers()
        {
            registrationManager = new RegistrationManager(RepositoryFactory.RegistrationRepository);
            customerManager = new CustomerManager(RepositoryFactory.CustomerRepository);
            activityManager = new ActivityManager(RepositoryFactory.ActivityRepository);
            memberManager = new MemberManager(RepositoryFactory.MemberRepository);
        }

        private void PopulateComboBoxes()
        {
            CustomerComboBox.ItemsSource = customerManager.GetCustomers(null);
            ActivitiesComboBox.ItemsSource = activityManager.GetActivities(null);
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerComboBox.SelectedItem == null || ActivitiesComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please fill all fields");
                return;
            }

            if (customer.Members.Count + 1 > activity.AvailablePlaces)
            {
                MessageBox.Show("Not enough capacity for this activity.");
                return;

            }

            registrationManager.AddRegistration(registration);
            MessageBox.Show("Registration completed successfully");
            Close();
        }

        private void UpdateRegistrationDetails()
        {
            registration = new Registration(customer, activity);

            var adultsInfo = $" {registration.NumberOfAdults} adult(s)";
            var childrenInfo = $" {registration.NumberOfChildren} children";
            SubtotalAdultsTextBlock.Text = registration.costAdult + adultsInfo;
            SubtotalChildrenTextBlock.Text = registration.costChild + childrenInfo;

            if (activity.Discount == null || activity.Discount == 0)
            {
                DiscountTextBlock.Text = " ";
            }
            else
            {
                DiscountTextBlock.Text = $"Discount: {activity.Discount}%";
            }

            TotalCostTextBlock.Text = registration.Price.ToString();
        }

        private void MemberCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            var member = checkBox.Tag as Member;
            if (member != null)
            {
                customer.Members.Add(member);
            }

            UpdateRegistrationDetails();
        }

        private void MemberCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            var member = checkBox.Tag as Member;
            if (member != null)
            {
                customer.Members.Remove(member);
            }

            UpdateRegistrationDetails();
        }

        private void CustomerComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            members = new List<Member>();
            customer = CustomerComboBox.SelectedItem as Customer;
            if (customer != null)
            {
                members = memberManager.GetMembersByCustomerId(customer.Id);
                MembersCheckboxes.ItemsSource = members;
            }
        }

        private void ActivitiesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MembersCheckboxes.IsEnabled = true;
            activity = ActivitiesComboBox.SelectedItem as Activity;
            DateTextBlock.Text = activity.Date.ToString();
            LocationTextBlock.Text = activity.Location;
            AvailableSeatsTextBlock.Text = activity.AvailablePlaces.ToString();
            customer.Members = new List<Member>();
            registration = new Registration(customer, activity);

            if (activity.Discount == null || activity.Discount == 0)
            {
                SubtotalAdultsTextBlock.Text = registration.costAdult +
                                               $"       {registration.NumberOfAdults} adult(s)";
                SubtotalChildrenTextBlock.Text = registration.costChild +
                                                 $"       {registration.NumberOfChildren} children";
                DiscountTextBlock.Text = " ";
                TotalCostTextBlock.Text = registration.Price.ToString();
            }
            else
            {
                SubtotalAdultsTextBlock.Text = registration.costAdult +
                                               $" ({activity.PriceAdult * registration.NumberOfAdults})       {registration.NumberOfAdults} adult(s)";
                SubtotalChildrenTextBlock.Text = registration.costChild +
                                                 $" ({activity.PriceChild * registration.NumberOfChildren})      {registration.NumberOfChildren} children";
                DiscountTextBlock.Text = $"Discount: {activity.Discount}%";
                TotalCostTextBlock.Text = registration.Price.ToString();
            }
        }
    }
}