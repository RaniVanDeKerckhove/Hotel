using System;
using System.Collections.Generic;
using Hotel.Domain.Model;

namespace Hotel.Domain.Repositories
{
    public interface IActivityRepository
    {
        void AddActivity(Activity activity);
        List<Activity> GetActivities(string filter);
        Activity GetActivityById(int activityId);
        List<Activity> GetAllActivities();
        void RemoveActivityById(int activityId);
       
    }
}
