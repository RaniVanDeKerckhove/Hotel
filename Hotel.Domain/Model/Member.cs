using Hotel.Domain.Exceptions;

namespace Hotel.Domain.Model
{
    public class Member
    {
        public Member(int customerId, string name, DateOnly birthday)
        {
            CustomerId = customerId;
            Name = name;
            Birthday = birthday;
        }

        private int _customerId;
        public int CustomerId
        {
            get { return _customerId; }
            set
            {
                if (value <= 0) throw new CustomerException("CustomerId must be greater than zero");
                _customerId = value;
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new CustomerException("Name is empty");
                _name = value;
            }
        }

        private DateOnly _birthday;
        public DateOnly Birthday
        {
            get
            {
                return _birthday;
            }
            set
            {
                if (DateOnly.FromDateTime(DateTime.Now) <= value) throw new CustomerException("Birthdate is not correct");
                _birthday = value;
            }
        }

    
        public override bool Equals(object? obj)
        {
            return obj is Member member &&
                   _customerId == member._customerId &&
                   _name == member._name &&
                   _birthday.Equals(member._birthday);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_customerId, _name, _birthday);
        }
    }
}