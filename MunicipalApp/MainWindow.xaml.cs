using System.Collections.Generic;
using System;
using System.Windows;

namespace MunicipalApp
{
    public partial class MainWindow : Window
    {
        private EventService eventService;

        public MainWindow()
        {
            InitializeComponent();

            eventService = new EventService();

            var serviceRequestManager = InitializeDummyData();

            // Set up the button click handler in the constructor
            btnServiceRequestStatus.Click += (s, e) =>
            {
                ServiceRequestStatus statusWindow = new ServiceRequestStatus(serviceRequestManager);
                statusWindow.Show();
            };
        }

        // Initialize dummy data for service requests
        private ServiceRequestManager InitializeDummyData()
        {
            var serviceRequestManager = new ServiceRequestManager();

            serviceRequestManager.AddRequest(new ServiceRequest
            {
                RequestID = 1,
                Category = "Electrical",
                Status = "In Progress",
                Description = "Fix streetlight on Main St.",
                SubmissionDate = DateTime.Now.AddDays(-3),
                Dependencies = new List<int> { }
            });

            serviceRequestManager.AddRequest(new ServiceRequest
            {
                RequestID = 2,
                Category = "Electrical",
                Status = "Complete",
                Description = "Replace transformer on New Road.",
                SubmissionDate = DateTime.Now.AddDays(-5),
                Dependencies = new List<int> { }
            });

            serviceRequestManager.AddRequest(new ServiceRequest
            {
                RequestID = 3,
                Category = "Sanitation",
                Status = "In Progress",
                Description = "Burst water pipe in Noordwyk.",
                SubmissionDate = DateTime.Now.AddDays(-1),
                Dependencies = new List<int> { }
            });

            serviceRequestManager.AddRequest(new ServiceRequest
            {
                RequestID = 4,
                Category = "Roads",
                Status = "Pending",
                Description = "Repair potholes on Rivonia Road.",
                SubmissionDate = DateTime.Now.AddDays(-7),
                Dependencies = new List<int> { }
            });

            serviceRequestManager.AddRequest(new ServiceRequest
            {
                RequestID = 5,
                Category = "Sanitation",
                Status = "In Progress",
                Description = "Blocked sewer in Tembisa.",
                SubmissionDate = DateTime.Now.AddDays(-2),
                Dependencies = new List<int> { }
            });

            serviceRequestManager.AddRequest(new ServiceRequest
            {
                RequestID = 6,
                Category = "Water",
                Status = "Complete",
                Description = "Install new water meter in Mamelodi.",
                SubmissionDate = DateTime.Now.AddDays(-10),
                Dependencies = new List<int> { }
            });

            serviceRequestManager.AddRequest(new ServiceRequest
            {
                RequestID = 7,
                Category = "Electrical",
                Status = "Pending",
                Description = "Power outage in Soweto Zone 6.",
                SubmissionDate = DateTime.Now.AddDays(-1),
                Dependencies = new List<int> { }
            });

            serviceRequestManager.AddRequest(new ServiceRequest
            {
                RequestID = 8,
                Category = "Roads",
                Status = "In Progress",
                Description = "Repair traffic lights at Sandton Drive intersection.",
                SubmissionDate = DateTime.Now.AddDays(-4),
                Dependencies = new List<int> { }
            });

            serviceRequestManager.AddRequest(new ServiceRequest
            {
                RequestID = 9,
                Category = "Sanitation",
                Status = "Complete",
                Description = "Leaking stormwater drain in Braamfontein.",
                SubmissionDate = DateTime.Now.AddDays(-6),
                Dependencies = new List<int> { }
            });

            serviceRequestManager.AddRequest(new ServiceRequest
            {
                RequestID = 10,
                Category = "Water",
                Status = "In Progress",
                Description = "Low water pressure in Alexandra.",
                SubmissionDate = DateTime.Now.AddDays(-3),
                Dependencies = new List<int> { }
            });

            serviceRequestManager.AddRequest(new ServiceRequest
            {
                RequestID = 11,
                Category = "Parks",
                Status = "Pending",
                Description = "Overgrown grass at Zoo Lake park.",
                SubmissionDate = DateTime.Now.AddDays(-2),
                Dependencies = new List<int> { }
            });

            serviceRequestManager.AddRequest(new ServiceRequest
            {
                RequestID = 12,
                Category = "Roads",
                Status = "Complete",
                Description = "Road resurfacing in Midrand.",
                SubmissionDate = DateTime.Now.AddDays(-8),
                Dependencies = new List<int> { }
            });

            serviceRequestManager.AddRequest(new ServiceRequest
            {
                RequestID = 13,
                Category = "Electrical",
                Status = "Pending",
                Description = "Install new streetlights in Orange Farm.",
                SubmissionDate = DateTime.Now.AddDays(-5),
                Dependencies = new List<int> { }
            });

            serviceRequestManager.AddRequest(new ServiceRequest
            {
                RequestID = 14,
                Category = "Water",
                Status = "In Progress",
                Description = "Burst pipe repair in Centurion.",
                SubmissionDate = DateTime.Now.AddDays(-3),
                Dependencies = new List<int> { }
            });

            serviceRequestManager.AddRequest(new ServiceRequest
            {
                RequestID = 15,
                Category = "Sanitation",
                Status = "Pending",
                Description = "Illegal dumping cleanup in Diepsloot.",
                SubmissionDate = DateTime.Now.AddDays(-6),
                Dependencies = new List<int> { }
            });



            return serviceRequestManager;
        }

        // Handler for the Report Issues button click
        private void btnReportIssues_Click(object sender, RoutedEventArgs e)
        {
            Reportissues reportIssuesWindow = new Reportissues();
            reportIssuesWindow.Show();
        }

        // Handler for the Local Events button click
        private void btnLocalEvents_Click(object sender, RoutedEventArgs e)
        {
            LocalEvents localEventsWindow = new LocalEvents(eventService);
            localEventsWindow.Show();
            this.Close();
        }

        //Handler for the Service Request Status button click
        private void btnServiceRequestStatus_Click(object sender, RoutedEventArgs e)
        {
            ServiceRequestStatus statusWindow = new ServiceRequestStatus(new ServiceRequestManager());
            statusWindow.Show();
        }
    }
}
//Part of this code as adapted from dev.to
//https://dev.to/dianaiminza/creating-mock-data-in-net-with-bogus-5bah
//Accessed 16 November 2024
