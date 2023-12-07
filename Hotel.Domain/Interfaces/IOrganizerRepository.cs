using Hotel.Domain.Model;
using System;
using System.Collections.Generic;

namespace Hotel.Domain.Repositories
{
    public interface IOrganizerRepository
    {
        void AddOrganizer(Organizer organizer);
        Organizer GetOrganizerById(int organizerId);
        List<Organizer> GetAllOrganizers();
        void RemoveOrganizerById(int organizerId);
    }
}
