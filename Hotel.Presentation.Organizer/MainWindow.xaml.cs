using System.Windows;
using Hotel.Presentation.Organizer;
using HotelProject.UI.OrganizerWPF;

namespace HotelProject.UI.Organizer
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ViewActivities_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow(new ActivityWindow());
        }

        private void AddNewActivity_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow(new ActivityManagement());
        }

        private void ShowWindow(Window window)
        {
            window.Show();
        }
    }
}