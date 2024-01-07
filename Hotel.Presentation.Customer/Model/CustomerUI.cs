using System.Collections.Generic;
using System.ComponentModel;

namespace Hotel.Presentation.Customer.Model
{
    public class CustomerUI : INotifyPropertyChanged
    {
        public CustomerUI(string name, string email, string municipality, string street, string postalCode, string houseNumber, string phone, int nrOfMembers)
        {
            Name = name;
            Email = email;
            Municipality = municipality;
            Street = street;
            PostalCode = postalCode;
            HouseNumber = houseNumber;
            Phone = phone;
            NrOfMembers = nrOfMembers;
            _members = new List<MemberUI>();
        }

        public CustomerUI(int? id, string name, string email, string municipality, string street, string postalCode, string houseNumber, string phone, int nrOfMembers)
            : this(name, email, municipality, street, postalCode, houseNumber, phone, nrOfMembers)
        {
            Id = id;
        }

        public List<MemberUI> _members { get; set; }

        public int? Id { get; set; }

        private string _name;
        public string Name { get { return _name; } set { _name = value; OnPropertyChanged(); } }

        private string _email;
        public string Email { get { return _email; } set { _email = value; OnPropertyChanged(); } }

        public string Municipality { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string HouseNumber { get; set; }

        private string _phone;
        public string Phone { get { return _phone; } set { _phone = value; OnPropertyChanged(); } }

        public int NrOfMembers { get; set; }

        private void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}