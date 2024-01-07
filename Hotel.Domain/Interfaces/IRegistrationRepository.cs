using Hotel.Domain.Model;
using System;
using System.Collections.Generic;

namespace Hotel.Domain.Repositories
{
    public interface IRegistrationRepository
    {
        public void AddRegistration(Registration registration);

    }
}
