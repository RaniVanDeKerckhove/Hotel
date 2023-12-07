using Hotel.Domain.Model;
using System;
using System.Collections.Generic;

namespace Hotel.Domain.Repositories
{
    public interface IMemberRepository
    {
        void AddMember(Member member);
        Member GetMemberByName(string memberName);
        List<Member> GetAllMembers();
        void RemoveMemberByName(string memberName);
        // Additional methods for member repository can be added as needed
    }
}
