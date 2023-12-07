using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Domain.Interfaces;
using Hotel.Domain.Model;
using Hotel.Domain.Repositories;

namespace Hotel.Persistence.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        public MemberRepository()
        {

        }
        public void AddMember(Member member)
        {
            throw new NotImplementedException();
        }

        public List<Member> GetAllMembers()
        {
            throw new NotImplementedException();
        }

        public Member GetMemberByName(string memberName)
        {
            throw new NotImplementedException();
        }

        public void RemoveMemberByName(string memberName)
        {
            throw new NotImplementedException();
        }
    }
}
