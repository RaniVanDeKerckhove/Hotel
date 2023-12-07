using System;
using System.Collections.Generic;

namespace Hotel.Domain.Model
{
    public class OrganizerManager
    {
        private List<Organizer> _organizers = new List<Organizer>();

        // Add a new organizer to the manager
        public void AddOrganizer(Organizer organizer)
        {
            _organizers.Add(organizer);
        }

        // Retrieve an organizer by ID
        public Organizer GetOrganizerById(int organizerId)
        {
            return _organizers.Find(organizer => organizer.OrganizerId == organizerId);
        }

        // Retrieve all organizers
        public List<Organizer> GetAllOrganizers()
        {
            return _organizers;
        }

        // Remove an organizer by ID
        public void RemoveOrganizerById(int organizerId)
        {
            Organizer organizerToRemove = GetOrganizerById(organizerId);
            if (organizerToRemove != null)
            {
                _organizers.Remove(organizerToRemove);
            }
        }

    }
}
