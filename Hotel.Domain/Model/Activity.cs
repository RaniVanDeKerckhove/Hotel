using Hotel.Domain.Exceptions;

using System;

namespace Hotel.Domain.Model
{
    public class Activity
    {
        public Activity(int id, string name, string description, DateTime date, int duration, int availablePlaces, decimal priceAdult, decimal priceChild, decimal discount, string location)
        {
            Id = id;
            Name = name;
            Description = description;
            Date = date;
            Duration = duration;
            AvailablePlaces = availablePlaces;
            PriceAdult = priceAdult;
            PriceChild = priceChild;
            Discount = discount;
            Location = location;
        }

        public int Id
        {
            get => _id;
            set
            {
                if (value <= 0)
                    throw new ActivityException("Invalid ID");
                _id = value;
            }
        }
        private int _id;

        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ActivityException("Name is empty");
                _name = value;
            }
        }
        private string _name;

        public string Description
        {
            get => _description;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ActivityException("Description is empty");
                _description = value;
            }
        }
        private string _description;

        public DateTime Date
        {
            get => _date;
            set
            {
                if (value == null)
                    throw new ActivityException("Date is null");
                _date = value;
            }
        }
        private DateTime _date;

        public int Duration
        {
            get => _duration;
            set
            {
                if (value <= 0)
                    throw new ActivityException("Invalid duration");
                _duration = value;
            }
        }
        private int _duration;

        public int AvailablePlaces
        {
            get => _availablePlaces;
            set
            {
                if (value <= 0)
                    throw new ActivityException("Invalid available places");
                _availablePlaces = value;
            }
        }
        private int _availablePlaces;

        public decimal PriceAdult
        {
            get => _priceAdult;
            set
            {
                if (value <= 0)
                    throw new ActivityException("Invalid adult price");
                _priceAdult = value;
            }
        }
        private decimal _priceAdult;

        public decimal PriceChild
        {
            get => _priceChild;
            set
            {
                if (value <= 0)
                    throw new ActivityException("Invalid child price");
                _priceChild = value;
            }
        }
        private decimal _priceChild;

        public string Location
        {
            get => _location;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ActivityException("Location is empty");
                _location = value;
            }
        }
        private string _location;

        public decimal? Discount
        {
            get => _discount;
            set
            {
                if (value < 0 || value > 100)
                    throw new ActivityException("Invalid discount");
                _discount = value;
            }
        }

        public DateTime StartDate { get; set; }
        public decimal CostAdult { get; set; }
        public decimal CostChild { get; set; }

        private decimal? _discount;
    }
}
