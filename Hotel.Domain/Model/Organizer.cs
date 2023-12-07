using Hotel.Domain.Exceptions;
using System;

namespace Hotel.Domain.Model
{
    public class Organizer
    {
        private const char SplitChar = '|';

        public Organizer(int organizerId, string name, ContactInfo contactInfo)
        {
            OrganizerId = organizerId;
            Name = name;
            ContactInfo = contactInfo;
        }

        public int OrganizerId { get; set; }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new OrganizerException("Organizer name cannot be empty");
                }
                _name = value;
            }
        }

        public ContactInfo ContactInfo { get; set; }
    }
}

