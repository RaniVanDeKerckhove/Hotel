using Hotel.Domain.Repositories;
using System;
using System.Collections.Generic;

namespace Hotel.Domain.Model
{
    public class RegistrationManager
    {
        private IRegistrationRepository registrationRepository;
        public RegistrationManager(IRegistrationRepository registrationRepository)
        {
            this.registrationRepository = registrationRepository;
        }

        public void AddRegistration(Registration registration)
        {
            registrationRepository.AddRegistration(registration);
        }



    }
}