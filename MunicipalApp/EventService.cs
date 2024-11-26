using System;
using System.Collections.Generic;
using System.Linq;

namespace MunicipalApp
{
    public class EventService
    {
        private SortedDictionary<DateTime, List<Event>> eventsByDate = new SortedDictionary<DateTime, List<Event>>();
        private HashSet<string> categories = new HashSet<string>();
        private Stack<Event> recentEvents = new Stack<Event>();
        private Queue<Event> upcomingEvents = new Queue<Event>();
        private SortedSet<DateTime> uniqueEventDates = new SortedSet<DateTime>();
        private Dictionary<string, List<Event>> eventsByCategory = new Dictionary<string, List<Event>>();

        public void InitializeEvents()
        {
            // Add initial events
            AddEvent(new Event("Community Cleanup", "Sanitation", new DateTime(2024, 10, 18), "Join us for a community cleanup event."));
            AddEvent(new Event("King Monada Annual Music Festival", "Arts", new DateTime(2024, 12, 1), "Re ya Monadene"));
            AddEvent(new Event("Electricity Price Update", "Utilities", new DateTime(2024, 11, 29), "Latest update on the electric fees."));
            AddEvent(new Event("Water Supply Update", "Utilities", new DateTime(2024, 10, 20), "Latest updates on the water supply."));
        }

        public void AddEvent(Event newEvent)
        {
            // Add to sorted dictionary
            if (!eventsByDate.ContainsKey(newEvent.Date))
            {
                eventsByDate[newEvent.Date] = new List<Event>();
            }
            eventsByDate[newEvent.Date].Add(newEvent);

            // Add to category dictionary
            if (!eventsByCategory.ContainsKey(newEvent.Category))
            {
                eventsByCategory[newEvent.Category] = new List<Event>();
            }
            eventsByCategory[newEvent.Category].Add(newEvent);

            // Add to hash set of categories
            categories.Add(newEvent.Category);

            // Add to recent events stack
            recentEvents.Push(newEvent);

            // Add to upcoming events queue if the date is in the future
            if (newEvent.Date > DateTime.Now)
            {
                upcomingEvents.Enqueue(newEvent);
            }

            // Add to sorted set of unique dates
            uniqueEventDates.Add(newEvent.Date);
        }

        // Search for events with filters
        public List<Event> SearchEvents(string searchTerm, string category, DateTime? date)
        {
            if (!eventsByDate.Any())
                return new List<Event>();

            return eventsByDate.Values.SelectMany(e => e)
                .Where(e =>
                    (string.IsNullOrEmpty(searchTerm) || (e.Name?.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)) &&
                    (string.IsNullOrEmpty(category) || e.Category.Equals(category, StringComparison.OrdinalIgnoreCase)) &&
                    (!date.HasValue || e.Date.Date.Equals(date.Value.Date)))
                .ToList();
        }

        // Recommendation feature based on user preferences
        public List<Event> RecommendEvents(string searchTerm, string preferredCategory, DateTime? preferredDate)
        {
            var recommended = new List<Event>();

            // If a category is preferred, suggest events in that category
            if (!string.IsNullOrEmpty(preferredCategory) && eventsByCategory.ContainsKey(preferredCategory))
            {
                recommended.AddRange(eventsByCategory[preferredCategory]);
            }

            // Suggest events based on search term
            recommended.AddRange(SearchEvents(searchTerm, null, null));

            // Suggest events close to the preferred date
            if (preferredDate.HasValue)
            {
                var closeDates = uniqueEventDates.GetViewBetween(preferredDate.Value.AddDays(-7), preferredDate.Value.AddDays(7));
                foreach (var date in closeDates)
                {
                    recommended.AddRange(eventsByDate[date]);
                }
            }

            // Remove duplicates and return
            return recommended.Distinct().ToList();
        }

        public List<Event> GetRecentEvents()
        {
            return recentEvents.ToList();
        }

        public List<Event> GetUpcomingEvents()
        {
            return upcomingEvents.ToList();
        }

        public HashSet<string> GetCategories()
        {
            return categories;
        }

        public DateTime? GetEarliestDate()
        {
            return uniqueEventDates.FirstOrDefault();
        }

        public DateTime? GetLatestDate()
        {
            return uniqueEventDates.LastOrDefault();
        }
    }
}
