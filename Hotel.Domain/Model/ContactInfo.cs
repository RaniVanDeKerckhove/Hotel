using Hotel.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Hotel.Domain.Model
{
    public class ContactInfo
    {
        private string _email;
        private string _phone;
        private Address _address;

        public ContactInfo(string email, string phone, Address address)
        {
            _email = email;
            _phone = phone;
            _address = address;
        }

        public string Email { get { return _email; } set { if (string.IsNullOrWhiteSpace(value)) throw new CustomerException("ci"); _email = value; } }
       
        public string Phone {
            get { return _phone; }
            set { if (string.IsNullOrWhiteSpace(value)) throw new CustomerException("cin"); _phone = value; } 
        }
        
        public Address Address {
            get { return _address; }
            set { if (value==null) throw new CustomerException("cin"); _address = value; }
        }
    }
}
