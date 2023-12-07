﻿using Hotel.Domain.Repositories;
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