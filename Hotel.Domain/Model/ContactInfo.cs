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
       
    
  

        public ContactInfo(string email, string phone, Address address)
        {
            _email = email;
            _phone = phone;
            _address = address;
        }
        private string _email;

        public string Email { get { return _email; } set { if (string.IsNullOrWhiteSpace(value)) throw new CustomerException("Fill in email"); _email = value; } }
        private string _phone;
        public string Phone {
            get { return _phone; }
            set { if (string.IsNullOrWhiteSpace(value)) throw new CustomerException("Fill in phone"); _phone = value; } 
        }
        private Address _address;
        public Address Address {
            get { return _address; }
            set { if (value==null) throw new CustomerException("Fill in address"); _address = value; }
        }
    }
}
