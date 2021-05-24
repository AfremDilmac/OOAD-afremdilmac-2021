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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfBalieMedewerkers
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }


        private void btnClickLid(object sender, RoutedEventArgs e)
        {
            Main.Content = new Medewerker();
            imgHome.Visibility = Visibility.Hidden;
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new MainWindow();
            imgHome.Visibility = Visibility.Visible;
        }

        private void btnClickItems(object sender, RoutedEventArgs e)
        {
            Main.Content = new Items();
            imgHome.Visibility = Visibility.Hidden;
        }
    }
}
