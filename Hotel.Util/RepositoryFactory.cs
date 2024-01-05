using Hotel.Domain.Interfaces;
using Hotel.Persistence.Repositories;
using System.Configuration;
using Hotel.Domain.Repositories;

namespace Hotel.Util
{
    public static class RepositoryFactory
    {

        public static ICustomerRepository CustomerRepository { get { return new CustomerRepository(ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString); } }
        public static IActivityRepository ActivityRepository { get { return new ActivityRepository(ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString); } }
        public static IMemberRepository MemberRepository { get { return new MemberRepository(ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString); } }
        public static IOrganizerRepository OrganizerRepository { get { return new OrganizerRepository(ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString); } }
        public static IRegistrationRepository RegistrationRepository { get { return new RegistrationRepository(ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString); } }
}
    
  
}