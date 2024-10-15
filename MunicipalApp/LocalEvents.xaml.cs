using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MunicipalApp
{
    public partial class LocalEvents : Window
    {
        private EventService eventService;
        private Queue<string> searchHistory = new Queue<string>();

        public LocalEvents(EventService service)
        {
            InitializeComponent();
            eventService = service;
            eventService.InitializeEvents(); // Initialize events here
            PopulateCategoriesAndDates();
        }


        // Populate categories and dates into the combo box and date picker
        public void PopulateCategoriesAndDates()
        {
            comboCategory.ItemsSource = eventService.GetCategories();
            datePickerEventDate.DisplayDateStart = eventService.GetEarliestDate();
            datePickerEventDate.DisplayDateEnd = eventService.GetLatestDate();
        }

        //Part of this code was adopted from StackOverflow
        //https://stackoverflow.com/questions/7740426/c-sharp-how-to-use-the-eventhandler
        //Accessed 12 October 2024

        // Search button click handler
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = txtSearch.Text.ToLower();
            string category = comboCategory.SelectedItem?.ToString();
            DateTime? selectedDate = datePickerEventDate.SelectedDate;

            var results = eventService.SearchEvents(searchTerm, category, selectedDate);
            lstEvents.ItemsSource = results.Select(evt => $"{evt.Name} - {evt.Date.ToShortDateString()}");

            // Add search term to history
            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchHistory.Enqueue(searchTerm);
                if (searchHistory.Count > 5)
                {
                    searchHistory.Dequeue(); // Limit history size
                }
            }

            RecommendEvents();
        }

        //Part of this code was adopted from Wisej
        //https://wisej.com/support/question/messagebox-with-event-handler
        //Accessed 12 October 2024

        // Event recommendation based on search history
        public void RecommendEvents()
        {
            if (searchHistory.Count > 0)
            {
                string lastSearch = searchHistory.Last();
                var recommendedEvents = eventService.GetRecommendedEvents(lastSearch);

                if (recommendedEvents.Any())
                {
                    MessageBox.Show("Recommended Events based on your search history:\n" +
                                    string.Join("\n", recommendedEvents.Select(evt => evt.Name)));
                }
            }
        }

        // Back button click handler
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

    }
}
