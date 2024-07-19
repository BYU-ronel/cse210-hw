using System;

namespace EventPlanning
{
    public class Address
    {
        private string _street;
        private string _city;
        private string _state;
        private string _zip;

        public Address(string street, string city, string state, string zip)
        {
            _street = street;
            _city = city;
            _state = state;
            _zip = zip;
        }

        public override string ToString()
        {
            return $"{_street}, {_city}, {_state} {_zip}";
        }
    }

    public abstract class Event
    {
         private string _title;
         private string _description;
        protected DateTime _date; // Change from private to protected
        private TimeSpan _time;
        private Address _address;

        public Event(string title, string description, DateTime date, TimeSpan time, Address address)
        {
            _title = title;
            _description = description;
            _date = date;
            _time = time;
            _address = address;
        }

        public string StandardDetails()
        {
             return $"{_title}\n{_description}\n{_date.ToShortDateString()} {_time.ToString(@"h\:mm tt")}\n{_address}";
        }

        protected DateTime Date { get { return _date; } }
        protected string Title { get { return _title; } } // Add this property
        protected string Description { get { return _description; } } // Add this property
        protected TimeSpan Time { get { return _time; } } // Add this property
        protected Address Address { get { return _address; } } // Add this property

        public abstract string FullDetails();
        public abstract string ShortDescription();
    }

    public class Lecture : Event
    {
        private string _speaker;
        private int _capacity;

        public Lecture(string title, string description, DateTime date, TimeSpan time, Address address, string speaker, int capacity)
            : base(title, description, date, time, address)
        {
            _speaker = speaker;
            _capacity = capacity;
        }

        public override string FullDetails()
        {
            return $"{base.StandardDetails()}\nLecture by {_speaker}\nCapacity: {_capacity}";
        }

        public override string ShortDescription()
        {
            return $"Lecture: {Title} on {Date.ToShortDateString()}";
        }
    }

    public class Reception : Event
    {
        private string _rsvpEmail;

        public Reception(string title, string description, DateTime date, TimeSpan time, Address address, string rsvpEmail)
            : base(title, description, date, time, address)
        {
            _rsvpEmail = rsvpEmail;
        }

        public override string FullDetails()
        {
            return $"{base.StandardDetails()}\nRSVP to {_rsvpEmail}";
        }

        public override string ShortDescription()
        {
            return $"Reception: {Title} on {Date.ToShortDateString()}";
        }
    }

    public class OutdoorGathering : Event
    {
        private string _weather;

        public OutdoorGathering(string title, string description, DateTime date, TimeSpan time, Address address, string weather)
            : base(title, description, date, time, address)
        {
            _weather = weather;
        }

        public override string FullDetails()
        {
            return $"{base.StandardDetails()}\nWeather: {_weather}";
        }

        public override string ShortDescription()
        {
            return $"Outdoor Gathering: {Title} on {Date.ToShortDateString()}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Address lectureAddress = new Address("123 Main St", "Anytown", "CA", "12345");
            Lecture lecture = new Lecture("C# Programming", "Learn C# programming", new DateTime(2023, 3, 15), new TimeSpan(6, 0, 0), lectureAddress, "John Doe", 50);

            Address receptionAddress = new Address("456 Elm St", "Othertown", "NY", "67890");
            Reception reception = new Reception("Wedding Reception", "Join us for a wedding reception", new DateTime(2023, 6, 1), new TimeSpan(7, 0, 0), receptionAddress, "rsvp@example.com");

            Address outdoorAddress = new Address("789 Park Ave", "Parksburg", "FL", "34567");
            OutdoorGathering outdoor = new OutdoorGathering("Music Festival", "Enjoy live music and food", new DateTime(2023, 9, 22), new TimeSpan(12, 0, 0), outdoorAddress, "Sunny with a high of 75");

            Console.WriteLine("Lecture:");
            Console.WriteLine(lecture.StandardDetails());
            Console.WriteLine(lecture.FullDetails());
            Console.WriteLine(lecture.ShortDescription());
            Console.WriteLine();

            Console.WriteLine("Reception:");
            Console.WriteLine(reception.StandardDetails());
            Console.WriteLine(reception.FullDetails());
            Console.WriteLine(reception.ShortDescription());
            Console.WriteLine();

            Console.WriteLine("Outdoor Gathering:");
            Console.WriteLine(outdoor.StandardDetails());
            Console.WriteLine(outdoor.FullDetails());
            Console.WriteLine(outdoor.ShortDescription());
            Console.WriteLine();
        }
    }
}