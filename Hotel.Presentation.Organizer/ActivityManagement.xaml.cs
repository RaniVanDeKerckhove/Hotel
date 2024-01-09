using Hotel.Domain.Managers;
using Hotel.Util;
using System;
using System.Windows;
using System.Windows.Controls;
using Hotel.Domain.Model;

namespace Hotel.Presentation.Organizer
{
 
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
                // Check alle velden ingevuld
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

                // neem waarden van texboxes
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

                
                Activity newActivity = new Activity( name, description, date, duration, availablePlaces, priceAdult, priceChild, discount, location);

                //save database
                activityManager.AddActivity(newActivity);

                MessageBox.Show("Activity added successfully!");


        
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
