using System;
using System.Collections.Generic;
using Hotel.Domain.Model;

namespace Hotel.Domain.Repositories
{
    public interface IActivityRepository
    {
        void AddActivity(Activity activity);
        Activity GetActivityById(int activityId);
        List<Activity> GetAllActivities();
        void RemoveActivityById(int activityId);
        Activity GetActivityByActivityId(int activityId);
        List<Activity> GetActivities(string filter);
    }
}
