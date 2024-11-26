using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace MunicipalApp
{
    public partial class LocalEvents : Window
    {
        private readonly EventService _eventService;
        private readonly Queue<string> _searchHistory = new Queue<string>();
        private const int MaxSearchHistorySize = 5;

        public LocalEvents(EventService service)
        {
            InitializeComponent();
            _eventService = service ?? throw new ArgumentNullException(nameof(service));
            _eventService.InitializeEvents();
            PopulateCategoriesAndDates();
        }

        // Populate categories and dates into combo box and date picker
        private void PopulateCategoriesAndDates()
        {
            comboCategory.ItemsSource = _eventService.GetCategories();
            datePickerEventDate.DisplayDateStart = _eventService.GetEarliestDate();
            datePickerEventDate.DisplayDateEnd = _eventService.GetLatestDate();
        }

        // Search button click handler
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = txtSearch.Text?.ToLower().Trim();
            string category = comboCategory.SelectedItem?.ToString();
            DateTime? selectedDate = datePickerEventDate.SelectedDate;

            if (string.IsNullOrWhiteSpace(searchTerm) && string.IsNullOrWhiteSpace(category) && !selectedDate.HasValue)
            {
                MessageBox.Show("Please enter a search term, select a category, or choose a date.", "Invalid Input");
                return;
            }

            var results = _eventService.SearchEvents(searchTerm, category, selectedDate);
            if (results.Any())
            {
                lstEvents.ItemsSource = results.Select(evt => $"{evt.Name} - {evt.Date:yyyy-MM-dd}");
                AddToSearchHistory(searchTerm);
                RecommendEvents();
            }
            else
            {
                MessageBox.Show("No events found matching your search criteria.", "No Results");
            }
        }

        // Add search term to history
        private void AddToSearchHistory(string searchTerm)
        {
            if (!string.IsNullOrEmpty(searchTerm))
            {
                _searchHistory.Enqueue(searchTerm);
                if (_searchHistory.Count > MaxSearchHistorySize)
                {
                    _searchHistory.Dequeue();
                }
            }
        }

        // Recommend events based on search history
        // Recommend events based on search history
        private void RecommendEvents()
        {
            if (_searchHistory.Any())
            {
                string lastSearch = _searchHistory.Last();
                var recommendedEvents = _eventService.RecommendEvents(lastSearch, null, null);

                if (recommendedEvents.Any())
                {
                    MessageBox.Show("Recommended Events based on your search history:\n" +
                                    string.Join("\n", recommendedEvents.Select(evt => evt.Name)), "Recommendations");
                }
                else
                {
                    MessageBox.Show("No recommendations available based on your search history.", "Recommendations");
                }
            }
        }


        // Back button click handler
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
