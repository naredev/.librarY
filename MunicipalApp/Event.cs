using System;

namespace MunicipalApp
{
    public class Event
    {
        public int ID { get; set; } // Unique identifier for the event
        public string Name { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        // Constructor with basic validation
        public Event(string name, string category, DateTime date, string description, int id = 0)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Event name cannot be empty.");

            if (string.IsNullOrWhiteSpace(category))
                throw new ArgumentException("Event category cannot be empty.");

            if (date < DateTime.MinValue || date > DateTime.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(date), "Invalid date.");

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Event description cannot be empty.");

            ID = id;
            Name = name;
            Category = category;
            Date = date;
            Description = description;
        }

        // Derived property to check if the event is upcoming
        public bool IsUpcoming => Date > DateTime.Now;

        // Override ToString for better debugging and logging
        public override string ToString()
        {
            return $"{ID}: {Name} ({Category}) - {Date:yyyy-MM-dd} | {Description}";
        }
    }
}
