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

namespace WpfBlackJack
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Random number = new Random();
            int playerKaart = number.Next(0, 2);
 
            if (playerKaart == 1 && playerKaart == 11)
            {
                imgEen.Source = new BitmapImage(new Uri("EenC.png", UriKind.Relative));
                
            }
            else if (playerKaart == 2)
            {
                imgEen.Source = new BitmapImage(new Uri("2D.png", UriKind.Relative));
            }
            else if (playerKaart == 3)
            {
                imgEen.Source = new BitmapImage(new Uri("3H.png", UriKind.Relative));
            }
            else if (playerKaart == 4)
            {
                imgEen.Source = new BitmapImage(new Uri("4D.png", UriKind.Relative));
            }
            else if (playerKaart == 5)
            {
                imgEen.Source = new BitmapImage(new Uri("5C.png", UriKind.Relative));
            }
            else if (playerKaart == 6)
            {
                imgEen.Source = new BitmapImage(new Uri("6H.png", UriKind.Relative));
            }
            else if (playerKaart == 7)
            {
                imgEen.Source = new BitmapImage(new Uri("7D.png", UriKind.Relative));
            }
            else if (playerKaart == 8)
            {
                imgEen.Source = new BitmapImage(new Uri("8C.png", UriKind.Relative));
            }
            else if (playerKaart == 9)
            {
                imgEen.Source = new BitmapImage(new Uri("9H.png", UriKind.Relative));
            }
            else if (playerKaart == 10)
            {
                imgEen.Source = new BitmapImage(new Uri("10D.png", UriKind.Relative));
            }
            else if (playerKaart == 11)
            {
                imgEen.Source = new BitmapImage(new Uri("11C.png", UriKind.Relative));
            }
            else if (playerKaart == 12)
            {
                imgEen.Source = new BitmapImage(new Uri("12H.png", UriKind.Relative));
            }
            else if (playerKaart == 13)
            {
                imgEen.Source = new BitmapImage(new Uri("13D.png", UriKind.Relative));
            }

           

        }
    }
}
