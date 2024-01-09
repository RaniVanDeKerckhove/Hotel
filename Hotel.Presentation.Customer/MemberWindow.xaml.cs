using System;
using System.Windows;
using System.Windows.Controls;
using Hotel.Presentation.Customer.Model;

namespace Hotel.Presentation.Customer
{
    public partial class MemberWindow : Window
    {

        public MemberUI NewMember { get; private set; }

        public MemberWindow()
        {
            InitializeComponent();
        }

    
        private void AddMemberButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Check
                if (string.IsNullOrWhiteSpace(NameTextBox.Text) || DatePicker.SelectedDate == null)
                {
                    MessageBox.Show("Please fill all fields!");
                    return;
                }

                // neem de waarden van de textboxes
                string name = NameTextBox.Text;
                DateOnly birthDay = new DateOnly(
                    DatePicker.SelectedDate.Value.Year,
                    DatePicker.SelectedDate.Value.Month,
                    DatePicker.SelectedDate.Value.Day
                );

              
                MemberUI newMember = new MemberUI(name, birthDay);

                
                NewMember = newMember;

              
                MessageBox.Show("Member added successfully!");

              
                DialogResult = true;
            }
            catch (Exception ex)
            {
               
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
