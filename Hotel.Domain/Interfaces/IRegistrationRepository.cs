using Hotel.Domain.Model;
using System;
using System.Collections.Generic;

namespace Hotel.Domain.Repositories
{
    public interface IRegistrationRepository
    {
        void AddRegistration(Registration registration);
        Registration GetRegistrationById(int registrationId);
        List<Registration> GetAllRegistrations();
        void RemoveRegistrationById(int registrationId);
    }
}
