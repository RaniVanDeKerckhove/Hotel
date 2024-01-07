using System.Collections.Generic;
using System.Windows;
using Hotel.Domain.Model;

namespace Hotel.UI.Register
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow(List<Member> selectedMembers, decimal totalCost)
        {
            InitializeComponent();

            // Display selected members in the ListBox
            SelectedMembersListBox.ItemsSource = selectedMembers;

            // Display total cost
            TotalCostTextBlock.Text = $"Total Cost: {totalCost:C}";
        }
    }
}