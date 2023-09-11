using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Interaction logic for management.xaml
    /// </summary>
    public partial class management : Page
    {
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\madgw\source\repos\Assignment Project\Assignment Project\Database1.mdf;Integrated Security=True;Connect Timeout=30";

        public management()
        {
            InitializeComponent();
            List<Employees> userDetails = LoadUserDataFromDatabase();
            Employee_manager_Datagrid.ItemsSource = userDetails;
        }
        private List<Employees> LoadUserDataFromDatabase()
        {
            List<Employees> userDetails = new List<Employees>();

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string SQL = "SELECT * FROM Employees";
                    using (SqlCommand cmd = new SqlCommand(SQL, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Employees employees = new Employees()
                                {
                                    Id = (int)reader["Id"],
                                    Employee_Name = reader["name"].ToString(),
                                    Employee_Address = reader["name"].ToString(),
                                    Employee_StartDate = reader["name"].ToString(),
                                    Employee_Duration = int.Parse(reader["Age"].ToString())
                                };
                                userDetails.Add(employees);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return userDetails;
        }
        public class Employees
        {
            public int Id { get; set; }
            public string Employee_Name { get; set; }
            public string Employee_Address { get; set; }
            public string Employee_StartDate { get; set;}
            public int Employee_Duration { get; set;}
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Employee_manager_Datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
