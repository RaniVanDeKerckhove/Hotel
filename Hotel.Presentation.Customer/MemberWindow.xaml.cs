using System;
using System.Windows;
using System.Windows.Controls;
using Hotel.Presentation.Customer.Model;

namespace Hotel.Presentation.Customer
{
    // MemberWindow class for adding a new member
    public partial class MemberWindow : Window
    {
        // Property to get the newly added member
        public MemberUI NewMember { get; private set; }

        // Constructor for the MemberWindow
        public MemberWindow()
        {
            InitializeComponent();
        }

        // Event handler for the "Add Member" button click
        private void AddMemberButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Check if all fields are filled
                if (string.IsNullOrWhiteSpace(NameTextBox.Text) || DatePicker.SelectedDate == null)
                {
                    MessageBox.Show("Please fill all fields!");
                    return;
                }

                // Get values from the input fields
                string name = NameTextBox.Text;
                DateOnly birthDay = new DateOnly(
                    DatePicker.SelectedDate.Value.Year,
                    DatePicker.SelectedDate.Value.Month,
                    DatePicker.SelectedDate.Value.Day
                );

                // Create the new MemberUI object
                MemberUI newMember = new MemberUI(name, birthDay);

                // Set the NewMember property to pass the result back to the calling code
                NewMember = newMember;

                // Show a success message
                MessageBox.Show("Member added successfully!");

                // Close the window after adding the member
                DialogResult = true;
            }
            catch (Exception ex)
            {
                // Show an error message if an exception occurs
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
