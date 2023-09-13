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
    /// Interaction logic for Attendance.xaml
    /// </summary>
    public partial class Attendance : Page
    {
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\madgw\source\repos\Assignment Project\Assignment Project\Database1.mdf;Integrated Security=True;Connect Timeout=30";

        public Attendance()
        {
            InitializeComponent();
            List<Employees> userDetails = LoadUserDataFromDatabase();
            AttendanceDataGrid.ItemsSource = userDetails;
        }
        protected List<Employees> LoadUserDataFromDatabase()
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
                                    Employee_Name = reader["Employee_Name"].ToString(),
                                    Employee_Address = reader["Employee_Address"].ToString(),
                                    Employee_StartDate = reader["Employee_StartDate"].ToString(),
                                    Employee_Duration = (int)reader["Employee_Duration"],
                                    Employee_Department = reader["Employee_Department"].ToString(),
                                    Employee_Designation = reader["Employee_Designation"].ToString(),
                                    Employee_Leaves = reader["Employee_Leaves"].ToString(),
                                    Salary_Details = (int)reader["Salary_Details"]
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
            public string Employee_StartDate { get; set; }
            public int Employee_Duration { get; set; }
            public string Employee_Department { get; set; }
            public string Employee_Designation { get; set; }
            public string Employee_Leaves { get; set; }
            public int Salary_Details { get; set; }
        }
    }
}
