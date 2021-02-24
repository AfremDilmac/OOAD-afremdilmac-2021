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

namespace WpfTimer
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer = new DispatcherTimer();
        private int count;
        public MainWindow()
        {
            count = 0;
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += timer_Tick;

        }
        private void timer_Tick(object sender, EventArgs e)
        {
            
            count++;
            lblCount.Content = TimeSpan.FromSeconds(count).ToString(@"mm\:ss");
            rctSeconds.Height = count;
            rctMinuts.Height = count / 60;

        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {

            timer.Start(); // start de timer
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            //stopt de timer
            timer.Stop();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Reset button count terug aan 0 en timer herstart.
            timer.Stop();
            count = 0;
            lblCount.Content = count;
        }
    }
}
