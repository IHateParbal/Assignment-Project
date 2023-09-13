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
            public string Employee_StartDate { get; set;}
            public int Employee_Duration { get; set;}
            public string Employee_Department { get; set; }
            public string Employee_Designation {  get; set; }
            public string Employee_Leaves {  get; set; }
            public int Salary_Details {  get; set; }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string SQL = "Insert Into Employees (Id, Employee_Name, Employee_Address, Employee_StartDate, Employee_Duration, Employee_Department, Employee_Designation, Employee_Leaves, Salary_Details) " +
                                 "values (@EmpID, @EmpName, @EmpAddress, @EmpStartDate, @EmpDuration, @EmpDepartment, @EmpDesignation, @EmpLeaves, @EmpSalary)";
                    using (SqlCommand cmd = new SqlCommand(SQL, con))
                    {
                        cmd.Parameters.AddWithValue("@EmpID", EmpID.Text);
                        cmd.Parameters.AddWithValue("@EmpName", EmpName.Text);
                        cmd.Parameters.AddWithValue("@EmpAddress", EmpAddress.Text);
                        cmd.Parameters.AddWithValue("@EmpStartDate", EmpStartDate.SelectedDate);
                        cmd.Parameters.AddWithValue("@EmpDuration", int.Parse(EmpDuration.Text));
                        cmd.Parameters.AddWithValue("@EmpDepartment", EmployeeDepartment.Text);
                        cmd.Parameters.AddWithValue("@EmpDesignation", EmpDisgnation.Text);
                        cmd.Parameters.AddWithValue("@EmpLeaves", EmpLeaves.Text);
                        cmd.Parameters.AddWithValue("@EmpSalary", int.Parse(EmpSalary.Text));
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            List<Employees> userDetails = LoadUserDataFromDatabase();
            Employee_manager_Datagrid.ItemsSource = userDetails;
    }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string SQL = "UPDATE Employees " +
                                 "SET Employee_Name = @EmpName, " +
                                 "Employee_Address = @EmpAddress, " +
                                 "Employee_StartDate = @EmpStartDate, " +
                                 "Employee_Duration = @EmpDuration, " +
                                 "Employee_Department = @EmpDepartment, " +
                                 "Employee_Designation = @EmpDesignation, " +
                                 "Employee_Leaves = @EmpLeaves, " +
                                 "Salary_Details = @EmpSalary " +
                                 "WHERE Id = @EmpID";
                    using (SqlCommand cmd = new SqlCommand(SQL, con))
                    {
                        cmd.Parameters.AddWithValue("@EmpID", EmpID.Text);
                        cmd.Parameters.AddWithValue("@EmpName", EmpName.Text);
                        cmd.Parameters.AddWithValue("@EmpAddress", EmpAddress.Text);
                        cmd.Parameters.AddWithValue("@EmpStartDate", EmpStartDate.SelectedDate);
                        cmd.Parameters.AddWithValue("@EmpDuration", int.Parse(EmpDuration.Text));
                        cmd.Parameters.AddWithValue("@EmpDepartment", EmployeeDepartment.Text);
                        cmd.Parameters.AddWithValue("@EmpDesignation", EmpDisgnation.Text);
                        cmd.Parameters.AddWithValue("@EmpLeaves", EmpLeaves.Text);
                        cmd.Parameters.AddWithValue("@EmpSalary", int.Parse(EmpSalary.Text));
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            List<Employees> userDetails = LoadUserDataFromDatabase();
            Employee_manager_Datagrid.ItemsSource = userDetails;
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string SQL = "DELETE FROM Employees WHERE Id = @EmpID";
                    using (SqlCommand cmd = new SqlCommand(SQL, con))
                    {
                        cmd.Parameters.AddWithValue("@EmpID", EmpID.Text);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            List<Employees> userDetails = LoadUserDataFromDatabase();
            Employee_manager_Datagrid.ItemsSource = userDetails;
        }


        private void Employee_manager_Datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Employees selectedUser = (Employees)Employee_manager_Datagrid.SelectedItem;
            if (selectedUser != null)
            {
                EmpID.Text = selectedUser.Id.ToString();
                EmpName.Text = selectedUser.Employee_Name;
                EmpAddress.Text = selectedUser.Employee_Address;
                EmpStartDate.SelectedDate = DateTime.Parse(selectedUser.Employee_StartDate);
                EmpDuration.Text = selectedUser.Employee_Duration.ToString();
                EmployeeDepartment.Text = selectedUser.Employee_Department;
                EmpDisgnation.Text = selectedUser.Employee_Designation;
                EmpLeaves.Text = selectedUser.Employee_Leaves;
                EmpSalary.Text = selectedUser.Salary_Details.ToString();
            }
        }

    }
}
