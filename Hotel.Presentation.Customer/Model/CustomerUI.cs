using System.Collections.Generic;
using System.ComponentModel;
using Hotel.Domain.Model;

namespace Hotel.Presentation.Customer.Model
{
    public class CustomerUI : INotifyPropertyChanged
    {
        public CustomerUI(string name, string email, string address, string phone, int nrOfMembers)
        {
            Name = name;
            Email = email;
            SplitAddress(address);
            Phone = phone;
            NrOfMembers = nrOfMembers;
            _members = new List<MemberUI>();
        }
        private const char splitChar = '-';

        public void SplitAddress(string addressLine)
        {
            string[] parts = addressLine.Split(splitChar);
            Municipality = parts[0];
            Street = parts[2];
            ZipCode = parts[1];
            HouseNumber = parts[3];
        }

        public CustomerUI()
        {
        }


        public CustomerUI(int? id, string name, string email, string address, string phone, int nrOfMembers)
            : this(name, email, address, phone, nrOfMembers)
        {
            Id = id;
        }

        public List<MemberUI> _members { get; set; }

        public int? Id { get; set; }

        private string _name;
        public string Name { get { return _name; } set { _name = value; OnPropertyChanged(); } }

        private string _email;
        public string Email { get { return _email; } set { _email = value; OnPropertyChanged(); } }

        public string Address { get; set; }

        private string _phone;
        public string Phone { get { return _phone; } set { _phone = value; OnPropertyChanged(); } }

        private int _nrOfMembers;

        public int NrOfMembers
        {
            get { return _nrOfMembers; }
            set { _nrOfMembers = value; OnPropertyChanged(); }
        }

        public string Municipality { get; private set; }
        public string ZipCode { get; private set; }
        public string HouseNumber { get; private set; }
        public string Street { get; private set; }

        
        private void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}