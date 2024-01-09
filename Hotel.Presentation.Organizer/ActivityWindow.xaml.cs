using System.Windows;
using Hotel.Domain.Managers;
using Hotel.Domain.Model;
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
            ActivitiesDataGrid.ItemsSource = activityManager.GetActivities(SearchTextBox.Text);
        }

        private void RefreshActivities()
        {

            List<Activity> allActivities = activityManager.GetAllActivities();

            // activiteit later dna vandaag niet tonen
            DateTime today = DateTime.Today;
            List<Activity> filteredActivities = new List<Activity>();

            foreach (var activity in allActivities)
            {
                if (activity.Date >= today)
                {
                    filteredActivities.Add(activity);
                }
            }


            ActivitiesDataGrid.ItemsSource = filteredActivities;
        }

        private void ShowAllButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshActivities();
        }
    }
}