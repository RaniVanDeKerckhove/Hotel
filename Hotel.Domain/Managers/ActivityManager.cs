using Hotel.Domain.Repositories;
using Hotel.Domain.Exceptions;
using Hotel.Domain.Model;
using Hotel.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Hotel.Domain.Managers
{
    public class ActivityManager : IActivityRepository
    {
        private readonly IActivityRepository _activityRepository;

        public ActivityManager(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public void AddActivity(Activity activity)
        {
            _activityRepository.AddActivity(activity);
        }

        public List<Activity> GetActivities(string filter)
        {
            return _activityRepository.GetActivities(filter);
        }

     
        public Activity GetActivityById(int activityId)
        {
            return _activityRepository.GetActivityById(activityId);
        }

        public List<Activity> GetAllActivities()
        {
            return _activityRepository.GetAllActivities();
        }

        public void RemoveActivityById(int activityId)
        {
            _activityRepository.RemoveActivityById(activityId);
        }
    }
}
