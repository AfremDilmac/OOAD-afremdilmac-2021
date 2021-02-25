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
            count = 300;
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += timer_Tick;

            
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            //Hier is de count-- omdat ik de versie heb gekoze waar de timer van 5min aftelt en de achtergrondkleur verandert.
            count--;
            lblCount.Content = TimeSpan.FromSeconds(count).ToString(@"mm\:ss");

            //Hiermee bepaal ik de hoogte van mijn rectangles via de aantal van de count.
            rctSeconds.Height = count;
            rctMinuts.Height = count / 5;


            //Hier gebeurt de background color via de timer het start met rood aan 5min en wanneer ik aan 0 kom wordt het groen.
            byte r = Convert.ToByte(17 + count / 2);
            byte g = Convert.ToByte(255 - (17 + count / 2));
            byte b = 0;
            test.Background = new SolidColorBrush(Color.FromRgb(r, g, b));

        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            //start de timer
            timer.Start();
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
            count = 300;
            lblCount.Content = count;
        }
    }
}
