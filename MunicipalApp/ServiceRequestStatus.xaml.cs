using System.Windows;
using System.Collections.Generic;

namespace MunicipalApp
{
    public partial class ServiceRequestStatus : Window
    {
        private ServiceRequestManager requestManager;

        public ServiceRequestStatus(ServiceRequestManager manager)
        {
            InitializeComponent();
            requestManager = manager;
            LoadRequests();
        }

        //Part oof this cod was adapted from stackoverlow
        //https://stackoverflow.com/questions/57964195/partial-class-not-recognizing-using-customnamespace-or-any-methods-affiliated
        //Accesed 09 November 2024

        private void LoadRequests()
        {
            dataGridRequests.ItemsSource = requestManager.GetUpcomingRequests();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            int requestId;
            if (int.TryParse(txtSearch.Text, out requestId))
            {
                var request = requestManager.FindRequest(requestId);
                if (request != null)
                {
                    MessageBox.Show($"Found Request: {request.Category}, Status: {request.Status}");
                }
                else
                {
                    MessageBox.Show("Request not found.");
                }
            }
            else
            {
                MessageBox.Show("Invalid Request ID.");
            }
        }
    }
}
