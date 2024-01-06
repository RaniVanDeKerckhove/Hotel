﻿using Hotel.Domain.Exceptions;
using System;
using System.Collections.Generic;

namespace Hotel.Domain.Model
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ContactInfo Contact { get; set; }
        public bool Status { get; set; } // Added Status property

        public List<Member> _members = new List<Member>();

        public Customer()
        {
        }

        public Customer(int id, string name, ContactInfo contact)
        {
            Id = id;
            Name = name;
            Contact = contact;
        }

        public Customer(string name, ContactInfo contact)
        {
            Name = name;
            Contact = contact;
        }

        public IReadOnlyList<Member> Members => _members.AsReadOnly();

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