using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32; // For OpenFileDialog
using System.IO;
using System.ComponentModel;

namespace MunicipalApp
{
    public partial class Reportissues : Window
    {
        private List<issues> reportedIssues = new List<issues>();  // List to store reported issues

        public Reportissues()
        {
            InitializeComponent();
        }

        private void btnAttachFile_Click(object sender, RoutedEventArgs e)
        {
            // Open file dialog to attach an image or document
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                lblAttachmentStatus.Text = $"File attached: {System.IO.Path.GetFileName(filePath)}";
            }
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            // Get details from the form
            string location = txtLocation.Text;
            string category = (comboCategory.SelectedItem as ComboBoxItem)?.Content.ToString();
            string description = txtDescription.Text;
            string filePath = lblAttachmentStatus.Text != "No file attached" ? lblAttachmentStatus.Text : null;

            // Add the new issue to the list
            issues newIssue = new issues(location, category, description, filePath);
            reportedIssues.Add(newIssue);

            // Show success message
            MessageBox.Show("Issue reported successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            // Clear form fields for next input
            txtLocation.Clear();
            txtDescription.Clear();
            comboCategory.SelectedIndex = -1;
            lblAttachmentStatus.Text = "No file attached";
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            // Close the current window to return to the main menu
            this.Close();
        }
    }
}

