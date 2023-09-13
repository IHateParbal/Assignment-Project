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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Assignment_Project
{
    /// <summary>
    /// Interaction logic for Report.xaml
    /// </summary>
    public partial class Report : Page
    {
        public Report()
        {
            InitializeComponent();
            
                ReportViewFrame.Navigate(new Uri("EmployeeDirectoryPage.xaml", UriKind.Relative));
       
        }

        private void Employee_DirectoryButton_Click(object sender, RoutedEventArgs e)
        {
            
                ReportViewFrame.Navigate(new Uri("EmployeeDirectoryPage.xaml", UriKind.Relative));
            
        }

        private void attendance_report_Click(object sender, RoutedEventArgs e)
        {
                ReportViewFrame.Navigate(new Uri("Attendance.xaml", UriKind.Relative));
            
        }

        private void Salary_statement_Click(object sender, RoutedEventArgs e)
        {

                ReportViewFrame.Navigate(new Uri("Salary.xaml", UriKind.Relative));
            
        }

        private void termination_report_Click(object sender, RoutedEventArgs e)
        {            
                ReportViewFrame.Navigate(new Uri("Termination.xaml", UriKind.Relative));
            
        }

        private void Training_report_Click(object sender, RoutedEventArgs e)
        {
            
                ReportViewFrame.Navigate(new Uri("Training.xaml", UriKind.Relative));
            
        }
    }
}
