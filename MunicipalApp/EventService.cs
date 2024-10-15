using System;
using System.Collections.Generic;
using System.Linq;

namespace MunicipalApp
{

    public class EventService
    {
        SortedDictionary<DateTime, List<Event>> eventsByDate = new SortedDictionary<DateTime, List<Event>>();
        HashSet<string> categories = new HashSet<string>();
        
        //Part of this code was adopted from StackOverflow
        //https://stackoverflow.com/questions/17009741/in-c-what-is-the-best-way-to-convert-a-listt-to-a-sorteddictionarystring-t
        //Accesed 10 October 2024

        public void InitializeEvents()
        {
            // Add events to the collection
            AddEvent(new Event("Community Cleanup", "Sanitation", new DateTime(2024, 10, 18), "Join us for a community cleanup event."));
            AddEvent(new Event("King Monada Annual Music Festival", "Arts", new DateTime(2024, 12, 1), "Re ya Monadene"));
            AddEvent(new Event("Electricity Price Update", "Utilities", new DateTime(2024, 11, 29), "Latest update on the electric fees"));
            AddEvent(new Event("Water Supply Update", "Utilities", new DateTime(2024, 10, 20), "Latest updates on the water supply."));
        }

        public void AddEvent(Event newEvent)
        {
            // Add the event to the dictionary by date
            if (!eventsByDate.ContainsKey(newEvent.Date))
            {
                eventsByDate[newEvent.Date] = new List<Event>();
            }
            eventsByDate[newEvent.Date].Add(newEvent);

            // Add unique category to the category HashSet
            categories.Add(newEvent.Category);
        }


        // Method to retrieve events based on search terms
        public List<Event> SearchEvents(string searchTerm, string category, DateTime? date)
        {
            return eventsByDate.Values.SelectMany(events => events)
                .Where(evt => (string.IsNullOrEmpty(searchTerm) || evt.Name.ToLower().Contains(searchTerm)) &&
                              (string.IsNullOrEmpty(category) || evt.Category == category) &&
                              (!date.HasValue || evt.Date.Date == date.Value.Date))
                .ToList();
        }

        // Method to recommend events based on search history
        public List<Event> GetRecommendedEvents(string searchTerm)
        {
            return eventsByDate.Values.SelectMany(events => events)
                .Where(evt => evt.Name.ToLower().Contains(searchTerm))
                .ToList();
        }

        //Part of this code was adopted from StackOverflow
        //https://stackoverflow.com/questions/29403810/a-hashset-contains-returning-an-object
        //Accessed 10 October 2024

        // Retrieve all categories
        public HashSet<string> GetCategories()
        {
            return categories;
        }

        // Get earliest event date
        public DateTime? GetEarliestDate()
        {
            if (eventsByDate != null && eventsByDate.Count > 0)
            {
                return eventsByDate.Keys.Min();
            }
            else
            {
                // Return null or a default value when no events exist
                return null;
            }
        }


        // Get latest event date
        public DateTime? GetLatestDate()
        {
            if (eventsByDate != null && eventsByDate.Count > 0)
            {
                return eventsByDate.Keys.Max();
            }
            else
            {
                // Return null or handle as needed
                return null;
            }
        }


    }
}
