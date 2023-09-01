using Assignment_Project;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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

namespace Assignment_Project
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\madgw\source\repos\Console\personal project\personal project\POS.mdf;Integrated Security=True;Connect Timeout=30";
        public LoginWindow()
        {
            InitializeComponent();
            LoginSignup.Content = "Sign up";
            Header.Content = LoginSignup.Content;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Username.Text = "";
            password.Password = "";
            Button SignupClicked = (Button)sender;
            string Signuptext = SignupClicked.Content.ToString();
            LoginSignup.Content = Signuptext;
            Header.Content = Signuptext;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Username.Text = "";
            password.Password = "";
            Button LoginClicked = (Button)sender;
            string Logintext = LoginClicked.Content.ToString();
            LoginSignup.Content = Logintext;
            Header.Content = Logintext;
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string buttonText = LoginSignup.Content.ToString();
            if (buttonText == "Sign up")
            {
                // Perform sign-up logic
                UserName = Username.Text;
                Password = password.Password;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        string sqlInsertQuery = "INSERT INTO Users (UserName, Password) VALUES (@Username, @Password)";
                        using (SqlCommand insertCommand = new SqlCommand(sqlInsertQuery, connection))
                        {
                            insertCommand.Parameters.AddWithValue("@Username", UserName);
                            insertCommand.Parameters.AddWithValue("@Password", Password);
                            insertCommand.ExecuteNonQuery();
                        }
                        MessageBox.Show("Sign up successful!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
            else if (buttonText == "Login")
            {
                // Perform login logic
                UserName = Username.Text;
                Password = password.Password;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        string sqlSelectQuery = "SELECT COUNT(*) FROM Users WHERE UserName = @Username AND Password = @Password";
                        using (SqlCommand selectCommand = new SqlCommand(sqlSelectQuery, connection))
                        {
                            selectCommand.Parameters.AddWithValue("@Username", UserName);
                            selectCommand.Parameters.AddWithValue("@Password", Password);

                            int matchingUserCount = (int)selectCommand.ExecuteScalar();

                            if (matchingUserCount > 0)
                            {
                                MainWindow window = new MainWindow();
                                window.Show();
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Login failed. Please sign up if you don't have an account.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }
    }
}

   