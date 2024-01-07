using System;

namespace Hotel.Presentation.Customer.Model
{
    public class MemberUI
    {
        public MemberUI(string name, DateOnly birthDay)
        {
            Name = name;
            BirthDay = birthDay;
        }

        private string _name;
        public string Name { get { return _name; } set { _name = value; } }

        private DateOnly _birthDay;
        public DateOnly BirthDay { get { return _birthDay; } set { _birthDay = value; } }

    }
}