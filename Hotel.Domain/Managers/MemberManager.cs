using Hotel.Domain.Repositories;
using System;
using System.Collections.Generic;

namespace Hotel.Domain.Model
{
    public class MemberManager
    {

        private List<Member> _members = new List<Member>();

     


        public MemberManager(IMemberRepository memberRepository)
        {
            _members = memberRepository.GetAllMembers();
        }
        public List<Member> GetMembersByCustomerId(int customerId)
        {
            return _members.Where(member => member.CustomerId == customerId).ToList();
        }


        public void AddMember(Member member)
        {
            _members.Add(member);
        }

        public Member GetMembers(int CustomerId)
        {
            return _members.Find(member => member.CustomerId == CustomerId);
        }
      

        public List<Member> GetAllMembers()
        {
            return _members;
        }

        public void RemoveMemberById(int CustomerId)
        {
            Member memberToRemove = GetMembers(CustomerId);
            if (memberToRemove != null)
            {
                _members.Remove(memberToRemove);
            }
        }

    }
}
