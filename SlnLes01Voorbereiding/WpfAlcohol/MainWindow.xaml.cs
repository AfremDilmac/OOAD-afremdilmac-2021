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

  

        private void sldWijn_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lblWijn.Content = Convert.ToString(Math.Round(sldWijn.Value)) + "glazen";

        }

        private void sldWhisky_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lblWhisky.Content = Convert.ToString(Math.Round(sldWhisky.Value)) + "glazen";

        }

        private void sldBier_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lblBier.Content = Convert.ToString(Math.Round(sldBier.Value)) + "glazen";
        }
    }
}
