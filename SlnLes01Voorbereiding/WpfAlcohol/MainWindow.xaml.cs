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

namespace WpfAlcohol
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

        private void sldBier_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lblBier.Content = Convert.ToString(Math.Round(sldBier.Value)) + " glazen";
            rtlBalk.Width = 25 + (Math.Round(sldBier.Value) * 25) + (Math.Round(sldWhisky.Value) * 25) + (Math.Round(sldWijn.Value) * 25);
            rtlBalk.Fill = new SolidColorBrush(Color.FromRgb(40, 255, 0));



        }

        private void sldWijn_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lblWijn.Content = Convert.ToString(Math.Round(sldWijn.Value)) + " glazen";
            rtlBalk.Width = 25 + (Math.Round(sldBier.Value) * 25) + (Math.Round(sldWhisky.Value) * 25) + (Math.Round(sldWijn.Value) * 25);
            rtlBalk.Fill = new SolidColorBrush(Color.FromRgb(40, 255, 0));
        }

        private void sldWhisky_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lblWhisky.Content = Convert.ToString(Math.Round(sldWhisky.Value)) + " glazen";
            rtlBalk.Width = 25 + (Math.Round(sldBier.Value) * 25) + (Math.Round(sldWhisky.Value) * 25) + (Math.Round(sldWijn.Value) * 25);
            rtlBalk.Fill = new SolidColorBrush(Color.FromRgb(40, 255, 0));
        }
    }
}
