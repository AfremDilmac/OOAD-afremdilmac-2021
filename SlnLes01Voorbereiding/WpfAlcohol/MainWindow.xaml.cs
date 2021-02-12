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
    
        private void sld_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double aantalGlazen = (sldBier.Value + sldWhisky.Value + sldWijn.Value);

            //Formule om kleur te bepalen met via rgb met aantalglazen (slidervalue)
            byte r = Convert.ToByte(17 * aantalGlazen);
            byte g = Convert.ToByte(255 -(17 * aantalGlazen));
            byte b = 0;

            //Text van aantal glazen hernomen van de slider value
            lblBier.Content = Convert.ToString(Math.Round(sldBier.Value)) + " glazen";
            lblWijn.Content = Convert.ToString(Math.Round(sldWijn.Value)) + " glazen";
            lblWhisky.Content = Convert.ToString(Math.Round(sldWhisky.Value)) + " glazen";
            rtlBalk.Width = 25 + (Math.Round(sldBier.Value) * 25) + (Math.Round(sldWhisky.Value) * 25) + (Math.Round(sldWijn.Value) * 25);

            //Kleur wordt gegeven aan balk
            rtlBalk.Fill = new SolidColorBrush(Color.FromRgb(r, g, b));


        }

    }
}
