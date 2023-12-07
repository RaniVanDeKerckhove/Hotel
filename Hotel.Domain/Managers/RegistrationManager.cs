using System;
using System.Collections.Generic;

namespace Hotel.Domain.Model
{
    public class RegistrationManager
    {
        private List<Registration> _registrations = new List<Registration>();

        // Add a new registration to the manager
        public void AddRegistration(Registration registration)
        {
            _registrations.Add(registration);
        }

        // Retrieve a registration by ID
        public Registration GetRegistrationById(int registrationId)
        {
            return _registrations.Find(registration => registration.RegistrationId == registrationId);
        }

        // Retrieve all registrations
        public List<Registration> GetAllRegistrations()
        {
            return _registrations;
        }

        // Remove a registration by ID
        public void RemoveRegistrationById(int registrationId)
        {
            Registration registrationToRemove = GetRegistrationById(registrationId);
            if (registrationToRemove != null)
            {
                _registrations.Remove(registrationToRemove);
            }
        }

    }
}
