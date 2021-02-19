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
using System.Windows.Threading;

namespace WpfApp1
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer = new DispatcherTimer();
        private int count = 0;
        public MainWindow()
        {
           
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            
            count++;
            lblCount.Content = count;
            
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            InitializeComponent();
            lblCount.Content = count;
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += timer_Tick; // voeg methode toe aan timer
            timer.Start(); // start de timer
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            count = 0;
            lblCount.Content = count;
        }
    }
}
