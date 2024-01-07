//using Hotel.Domain.Model;
//using Hotel.Persistence.Repositories;

//namespace ConsoleAppDL
//{
//    internal class Program
//    {
//        static void Main(string[] args)
//        {
//            Console.WriteLine("Hello, World!");
//            string conn = "Data Source=MSI\\SQLEXPRESS;Initial Catalog=Hotel;Integrated Security=True";
//            RegistrationRepository repo = new RegistrationRepository(conn);
//            List<Registration> registrations = repo.GetAllRegistrations(); // Remove the argument here
//            foreach (Registration registration in registrations)
//            {
//                Console.WriteLine(registration.RegistrationId);
//            }
//        }
//    }
//}