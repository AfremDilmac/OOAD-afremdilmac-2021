using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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

namespace WpfLeden
{
    /// <summary>
    /// Logique d'interaction pour WpfLogin.xaml
    /// </summary>
    public partial class WpfLogin : Window
    {
        string connString = ConfigurationManager.AppSettings["connString"];
        public WpfLogin()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(connString);

                sqlCon.Open();
                string query = "SELECT COUNT(1) FROM LID WHERE lidnummer=@lidnummer";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@lidnummer", tbxLidnummer.Text);
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());

                if (count == 1)
                {
                    lblError.Content = "Welkom";
                    MainWindow sW = new MainWindow();
                    sW.Show();
                    this.Close();
                }
                else
                {
                    lblError.Content = "Gebruikersnaam en/of wachtwoord is fout.";
                }
        }
    }
}
