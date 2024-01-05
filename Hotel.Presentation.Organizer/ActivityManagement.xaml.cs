using Hotel.Domain.Managers;
using Hotel.Util;
using System;
using System.Windows;
using System.Windows.Controls;
using Hotel.Domain.Model;

namespace Hotel.Presentation.Organizer
{
    /// <summary>
    /// Interaction logic for ActivityManagement.xaml
    /// </summary>
    public partial class ActivityManagement : Window
    {
        private ActivityManager activityManager;

        public ActivityManagement()
        {
            InitializeComponent();
            activityManager = new ActivityManager(RepositoryFactory.ActivityRepository);
        }

        private void AddActivity_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Check if all fields are filled
                if (string.IsNullOrWhiteSpace(NameTextBox.Text) ||
                    string.IsNullOrWhiteSpace(DescriptionTextBox.Text) ||
                    DatePicker.SelectedDate == null ||
                    HoursComboBox.SelectedItem == null ||
                    MinutesComboBox.SelectedItem == null ||
                    string.IsNullOrWhiteSpace(DurationTextBox.Text) ||
                    string.IsNullOrWhiteSpace(AvailablePlacesTextBox.Text) ||
                    string.IsNullOrWhiteSpace(PriceAdultTextBox.Text) ||
                    string.IsNullOrWhiteSpace(PriceChildTextBox.Text) ||
                    string.IsNullOrWhiteSpace(DiscountTextBox.Text) ||
                    string.IsNullOrWhiteSpace(LocationTextBox.Text))
                {
                    MessageBox.Show("Please fill all fields!");
                    return;
                }

                // Get values from the input fields
                string name = NameTextBox.Text;
                string description = DescriptionTextBox.Text;
                DateTime date = DatePicker.SelectedDate ?? DateTime.Now;
                int selectedHours = int.Parse(((ComboBoxItem)HoursComboBox.SelectedItem).Content.ToString());
                int selectedMinutes = int.Parse(((ComboBoxItem)MinutesComboBox.SelectedItem).Content.ToString());
                date = date.AddHours(selectedHours).AddMinutes(selectedMinutes);
                int duration = Convert.ToInt32(DurationTextBox.Text);
                int availablePlaces = Convert.ToInt32(AvailablePlacesTextBox.Text);
                decimal priceAdult = Convert.ToDecimal(PriceAdultTextBox.Text);
                decimal priceChild = Convert.ToDecimal(PriceChildTextBox.Text);
                decimal discount = Convert.ToDecimal(DiscountTextBox.Text);
                string location = LocationTextBox.Text;

                // Create the new Activity object
                // Adjust the constructor parameters based on your Activity class
                Activity newActivity = new Activity(0, name, description, date, duration, availablePlaces, priceAdult, priceChild, discount, location);

                // Add your logic to save the new activity to the database or perform other actions
                activityManager.AddActivity(newActivity);

                MessageBox.Show("Activity added successfully!");

                // Optionally close the window after adding the activity
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
