using Hotel.Domain.Exceptions;
using System;
using System.Collections.Generic;

namespace Hotel.Domain.Model
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }  // Add this property

        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; } // Added Status property

        public int NrOfMembers { get; private set; }

        public List<Member> _members = new List<Member>();



        public Customer()
        {
        }

        public Customer(int id, string name, Address address, string phoneNumber, string email)
        {
            Id = id;
            Name = name;
            Address = address;
            PhoneNumber = phoneNumber;
            Email = email;
        }
        public Customer(int id, string name, Address address, string phoneNumber, string email, int nrOfMembers)
        {
            Id = id;
            Name = name;
            Address = address;
            PhoneNumber = phoneNumber;
            Email = email;
            NrOfMembers = nrOfMembers;
        }

        public Customer(string name, string phoneNumber, string email, Address address)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Address = address;
            Email = email;
        }

        public List<Member> Members { get; set; } // Change IReadOnlyList<Member> to List<Member>

        public void AddMember(Member member)
        {
            if (!_members.Contains(member))
                _members.Add(member);
            else
                throw new CustomerException("Add the member");
        }

        public void RemoveMember(Member member)
        {
            if (_members.Contains(member))
                _members.Remove(member);
            else
                throw new CustomerException("Remove the member");
        }

        public IReadOnlyList<Member> GetMembers()
        {
            return _members.AsReadOnly();
        }
    }
}