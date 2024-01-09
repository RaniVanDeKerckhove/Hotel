using Hotel.Domain.Exceptions;
using Hotel.Domain.Interfaces;
using Hotel.Domain.Model;
using System;

namespace Hotel.Domain.Managers
{
    public class CustomerManager
    {
        private ICustomerRepository _customerRepository;

        public CustomerManager(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public List<Customer> GetCustomers(string filter)
        {
            try
            {
                return _customerRepository.GetCustomers(filter);
            }
            catch (Exception ex)
            {
                throw new CustomerManagerException("Error in GetCustomers method of CustomerManager", ex);
            }
        }
        public List<Customer> GetAllCustomers()
        {
            try
            {
                return _customerRepository.GetAllCustomers();
            }
            catch (Exception ex)
            {
                throw new CustomerManagerException("Error in GetAllCustomers method of CustomerManager", ex);
            }
        }


        public void AddCustomer(Customer customer)
        {
            try
            {
                _customerRepository.AddCustomer(customer);
            }
            catch (Exception ex)
            {
                throw new CustomerManagerException("Error in AddCustomer method of CustomerManager", ex);
            }
        }

        public void UpdateCustomer(Customer customer)
        {
            try
            {
                
                if (customer == null)
                {
                    throw new ArgumentNullException(nameof(customer), "Customer cannot be null.");
                }

                _customerRepository.UpdateCustomer(customer);
            }
            catch (Exception ex)
            {

                throw new CustomerManagerException("Error in UpdateCustomer method of CustomerManager", ex);
            }
        }


        public void RemoveCustomerById(int customerId)
        {
            try
            {
                _customerRepository.RemoveCustomerById(customerId);
            }
            catch (Exception ex)
            {
    
                throw new CustomerManagerException("Error in RemoveCustomerById method of CustomerManager", ex);
            }
        }
        public Customer GetCustomerById(int customerId)
        {
            try
            {
                return _customerRepository.GetCustomerById(customerId);
            }
            catch (Exception ex)
            {
        
                throw new CustomerManagerException("Error in GetCustomerById method of CustomerManager", ex);
            }
        }
        public void AddMemberToCustomer(int customerId, Member member)
        {
            try
            {

                _customerRepository.AddMemberToCustomer(customerId, member);
            }
            catch (Exception ex)
            {
                throw new CustomerManagerException("Error in AddMemberToCustomer method of CustomerManager", ex);
            }
        }

    }
}