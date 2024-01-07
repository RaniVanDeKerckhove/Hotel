using Hotel.Domain.Model;
using System;
using System.Collections.Generic;

namespace Hotel.Domain.Repositories
{
    public interface IMemberRepository
    {
        void AddMember(Member member);
        public List<Member> GetMembersByCustomerId(int customerId);

        List<Member> GetAllMembers();
        void RemoveMemberById(int Id);
    }
}
