using System.Windows;
using Hotel.Domain.Managers;
using Hotel.Util;

namespace HotelProject.UI.OrganizerWPF
{
    public partial class ActivityWindow : Window
    {
        private ActivityManager activityManager;

        public ActivityWindow()
        {
            InitializeComponent();
            activityManager = new ActivityManager(RepositoryFactory.ActivityRepository);
            RefreshActivities();
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
    }
}