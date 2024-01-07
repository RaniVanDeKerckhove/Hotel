using Hotel.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Interfaces
{
    public interface ICustomerRepository
    {
       

        void AddCustomer(Customer customer);
        List<Customer> GetCustomers(string filter);
        Customer GetCustomerById(int customerId);
        void RemoveCustomerById(int customerId);
        public void UpdateCustomer(Customer customer);
        public List<Customer> GetAllCustomers();
        void AddMemberToCustomer(int customerId, Member member);





    }
}
