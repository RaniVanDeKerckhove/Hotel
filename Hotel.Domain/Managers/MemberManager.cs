using System;
using System.Collections.Generic;

namespace Hotel.Domain.Model
{
    public class MemberManager
    {
        private List<Member> _members = new List<Member>();

        // Add a new member to the manager
        public void AddMember(Member member)
        {
            _members.Add(member);
        }

        // Retrieve a member by name
        public Member GetMemberByName(string memberName)
        {
            return _members.Find(member => member.Name == memberName);
        }

        // Retrieve all members
        public List<Member> GetAllMembers()
        {
            return _members;
        }

        // Remove a member by name
        public void RemoveMemberByName(string memberName)
        {
            Member memberToRemove = GetMemberByName(memberName);
            if (memberToRemove != null)
            {
                _members.Remove(memberToRemove);
            }
        }

    }
}
