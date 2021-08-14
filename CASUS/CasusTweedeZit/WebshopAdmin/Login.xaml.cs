using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Shapes;

namespace WebshopAdmin
{
    /// <summary>
    /// Logique d'interaction pour Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }
        string connString = ConfigurationManager.AppSettings["connString"];

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand("SELECT * FROM Admin", conn);
                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    string gebruikersnaam = Convert.ToString(reader["gebruikersnaam"]);
                    string passwoord = Convert.ToString(reader["passwoord"]);

                    if (tbxUsername.Text == gebruikersnaam && tbxPassword.Password == passwoord) 
                    {
                        MainWindow main = new MainWindow();
                        main.Show();
                    }
                }
               
            }
        }
    }
}
