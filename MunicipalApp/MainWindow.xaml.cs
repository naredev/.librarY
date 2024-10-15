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
        }

        private void btnReportIssues_Click(object sender, RoutedEventArgs e)
        {
            // Open the Report Issues window
            Reportissues reportIssuesWindow = new Reportissues();
            reportIssuesWindow.Show();
        }

        private void btnLocalEvents_Click(object sender, RoutedEventArgs e)
        {
            // Pass the eventService to the LocalEvents window
            LocalEvents localEventsWindow = new LocalEvents(eventService);
            localEventsWindow.Show();
            this.Close();
        }
    }
}
