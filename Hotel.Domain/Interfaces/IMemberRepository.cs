using Hotel.Domain.Model;
using System;
using System.Collections.Generic;

namespace Hotel.Domain.Repositories
{
    public interface IMemberRepository
    {
        void AddMember(Member member);
        Member GetMemberByCustomerId(int CustomerId);
        List<Member> GetAllMembers();
        void RemoveMemberById(int Id);
    }
}
