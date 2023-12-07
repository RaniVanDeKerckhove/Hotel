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
    public class RegistraionRepository : IRegistrationRepository
    {
        public void AddRegistration(Registration registration)
        {
            throw new NotImplementedException();
        }

        public List<Registration> GetAllRegistrations()
        {
            throw new NotImplementedException();
        }

        public Registration GetRegistrationById(int registrationId)
        {
            throw new NotImplementedException();
        }

        public void RemoveRegistrationById(int registrationId)
        {
            throw new NotImplementedException();
        }
    }
}
