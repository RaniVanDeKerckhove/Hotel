using Hotel.Domain.Repositories;
using System;
using System.Collections.Generic;

namespace Hotel.Domain.Model
{
    public class OrganizerManager
    {
        private List<Organizer> _organizers = new List<Organizer>();
        private IOrganizerRepository _organizerRepository;


        public OrganizerManager(IOrganizerRepository organizerRepository)
        {
            _organizerRepository = organizerRepository;
        }

        public void AddOrganizer(Organizer organizer)
        {
            _organizers.Add(organizer);
        }

        public Organizer GetOrganizerById(int organizerId)
        {
            return _organizers.Find(organizer => organizer.OrganizerId == organizerId);
        }

        public List<Organizer> GetAllOrganizers()
        {
            return _organizers;
        }

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
