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
                _customer = customer;
                _activity = activity;
                NumberOfAdults = 1; //the customer is always an adult
                DetermineAdultOrChild(customer);
                CalculatePrice();

            }

            public int Id { get { return _id; } set { if (value <= 0) throw new RegistrationException("invalid id"); _id = value; } }
            private int _id;
            public Customer Customer { get { return _customer; } set { if (value == null) throw new RegistrationException("customer null"); _customer = value; } }
            private Customer _customer;
            public Activity Activity { get { return _activity; } set { if (value == null) throw new RegistrationException("activity null"); _activity = value; } }
            private Activity _activity;
            public decimal Price { get { return _price; } set { if (value <= 0) throw new RegistrationException("invalid price"); _price = value; } }
            private decimal _price;
            public decimal costChild { get { return _costChild; } set { if (value < 0) throw new RegistrationException("invalid costchild"); _costChild = value; } }
            private decimal _costChild;
            public decimal costAdult { get { return _costAdult; } set { if (value < 0) throw new RegistrationException("invalid costadult"); _costAdult = value; } }
            private decimal _costAdult;

            public int NumberOfAdults { get { return _numberOfAdults; } set { if (value <= 0) throw new RegistrationException("invalid numberofadults"); _numberOfAdults = value; } }
            private int _numberOfAdults;
            public int NumberOfChildren { get { return _numberOfChildren; } set { if (value < 0) throw new RegistrationException("invalid numberofchildren"); _numberOfChildren = value; } }
            private int _numberOfChildren;

            private void CalculatePrice()
            {
                //calculate the price if there is a discount
                if (_activity.Discount != null || _activity.Discount != 0)
                {
                    costAdult = (decimal)(_activity.PriceAdult - (_activity.PriceAdult * (_activity.Discount / 100))) * _numberOfAdults;
                    if (Customer.Members.Count != 0) costChild = (decimal)(_activity.PriceChild - (_activity.PriceChild * (_activity.Discount / 100))) * _numberOfChildren;
                    else costChild = 0;
                }
                else
                {
                    costAdult = _activity.PriceAdult * _numberOfAdults;
                    if (Customer.Members.Count != 0) costChild = _activity.PriceChild * _numberOfChildren;
                    else costChild = 0;
                }

                costAdult = decimal.Parse(costAdult.ToString("0.00"));
                costChild = decimal.Parse(costChild.ToString("0.00"));


                Price = decimal.Parse((costAdult + costChild).ToString("0.00"));


            }

        private void DetermineAdultOrChild(Customer customer)
        {
            //calculate based on birthdate if members of the customer is adult or child so we can see how many adults and children there are
            foreach (Member member in customer.Members)
            {
                DateTime dateTime = new DateTime(member.Birthday.Year, member.Birthday.Month, member.Birthday.Day);
                if (dateTime.AddYears(18) < DateTime.Now)
                {
                    _numberOfAdults++;
                }
                else
                {
                    _numberOfChildren++;
                }
            }
        }

    }

}

