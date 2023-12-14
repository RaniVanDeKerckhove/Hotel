
using Hotel.Domain.Model;

using Hotel.Persistence.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Hotel.Domain.Repositories;

namespace Hotel.Persistence.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private string connectionString;

        public ActivityRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void AddActivity(Activity activity)
        {
            throw new NotImplementedException();
        }

        public List<Activity> GetActivities(string filter)
        {
            throw new NotImplementedException();
        }

        public Activity GetActivityByActivityId(int activityId)
        {
            throw new NotImplementedException();
        }

        public Activity GetActivityById(int activityId)
        {
            throw new NotImplementedException();
        }

        public List<Activity> GetAllActivities()
        {
            throw new NotImplementedException();
        }

        public void RemoveActivityById(int activityId)
        {
            throw new NotImplementedException();
        }
    }
}

        
