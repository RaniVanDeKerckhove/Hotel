using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Domain.Exceptions;
using Hotel.Domain.Model;

namespace Hotel.Domain.Model
{
    public class Registration
    {
        public Registration(Customer customer, Activity activity)
        {
            InitializeRegistration(customer, activity);
            CalculateTotalPrice();
        }

        public int RegistrationId
        {
            get { return _registrationId; }
            set { if (value <= 0) throw new RegistrationException("Invalid registration ID"); _registrationId = value; }
        }
        private int _registrationId;

        public Customer RegisteredCustomer
        {
            get { return _registeredCustomer; }
            set { if (value == null) throw new RegistrationException("Customer cannot be null"); _registeredCustomer = value; }
        }
        private Customer _registeredCustomer;

        public Activity RegisteredActivity
        {
            get { return _registeredActivity; }
            set { if (value == null) throw new RegistrationException("Activity cannot be null"); _registeredActivity = value; }
        }
        private Activity _registeredActivity;

        public decimal TotalPrice
        {
            get { return _totalPrice; }
            set { if (value <= 0) throw new RegistrationException("Invalid total price"); _totalPrice = value; }
        }
        private decimal _totalPrice;

        public decimal ChildCost
        {
            get { return _childCost; }
            set { if (value < 0) throw new RegistrationException("Invalid child cost"); _childCost = value; }
        }
        private decimal _childCost;

        public decimal AdultCost
        {
            get { return _adultCost; }
            set { if (value < 0) throw new RegistrationException("Invalid adult cost"); _adultCost = value; }
        }
        private decimal _adultCost;

        public int NumberOfAdults
        {
            get { return _numberOfAdults; }
            set { if (value <= 0) throw new RegistrationException("Invalid number of adults"); _numberOfAdults = value; }
        }
        private int _numberOfAdults;

        public int NumberOfChildren
        {
            get { return _numberOfChildren; }
            set { if (value < 0) throw new RegistrationException("Invalid number of children"); _numberOfChildren = value; }
        }
        private int _numberOfChildren;

        private void InitializeRegistration(Customer customer, Activity activity)
        {
            _registeredCustomer = customer;
            _registeredActivity = activity;
            NumberOfAdults = 1; // The customer is always considered an adult
            DetermineAdultOrChild(customer);
        }

        private void CalculateTotalPrice()
        {
            decimal adultPrice, childPrice;

            // Calculate the price considering any discounts
            if (_registeredActivity.Discount != null && _registeredActivity.Discount != 0)
            {
                adultPrice = (decimal)(_registeredActivity.PriceAdult - (_registeredActivity.PriceAdult * (_registeredActivity.Discount / 100))) * _numberOfAdults;
                childPrice = (_registeredCustomer.Members.Count != 0) ? (decimal)(_registeredActivity.PriceChild - (_registeredActivity.PriceChild * (_registeredActivity.Discount / 100))) * _numberOfChildren : 0;
            }
            else
            {
                adultPrice = _registeredActivity.PriceAdult * _numberOfAdults;
                childPrice = (_registeredCustomer.Members.Count != 0) ? _registeredActivity.PriceChild * _numberOfChildren : 0;
            }

            _adultCost = decimal.Parse(adultPrice.ToString("0.00"));
            _childCost = decimal.Parse(childPrice.ToString("0.00"));
            _totalPrice = decimal.Parse((_adultCost + _childCost).ToString("0.00"));
        }


        private void DetermineAdultOrChild(Customer customer)
        {
            // Determine whether members of the customer are adults or children to count the number of adults and children
            foreach (Member member in customer.Members)
            {
                DateTime birthDate = new DateTime(member.Birthday.Year, member.Birthday.Month, member.Birthday.Day);
                _numberOfAdults += (birthDate.AddYears(18) < DateTime.Now) ? 1 : 0;
                _numberOfChildren += (birthDate.AddYears(18) >= DateTime.Now) ? 1 : 0;
            }
        }


    }
}
