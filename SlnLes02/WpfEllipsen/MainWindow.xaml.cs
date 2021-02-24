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

namespace WpfEllipsen
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    


    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            const int AANTAL_ELLIPSEN = 50;
            const int MAX_RADIUS = 300;
            const int MIN_RADIUS = 50;
            sldEen.Maximum = AANTAL_ELLIPSEN;
            sldTwee.Maximum = AANTAL_ELLIPSEN;
            sldDrie.Maximum = AANTAL_ELLIPSEN;
        }

       

        private void btnTekenen_Click(object sender, RoutedEventArgs e)
        {

            // maak de ellips
            Ellipse newEllipse = new Ellipse();
            newEllipse.Width = 150;
            newEllipse.Height = 60;
            newEllipse.Fill = new SolidColorBrush(Color.FromRgb(122, 78, 200));
            double xPos = 50;
            double yPos = 85;
            newEllipse.SetValue(Canvas.LeftProperty, xPos);
            newEllipse.SetValue(Canvas.TopProperty, yPos);
            //voeg ellips toe aan het canvas
            
        }

        private void sldHorizontal_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Ellipse newEllipse = new Ellipse();
            lblSlider1.Content = Math.Round(sldEen.Value);
            lblSlider2.Content = Math.Round(sldTwee.Value);
            lblSlider3.Content = Math.Round(sldDrie.Value);
            
           canvas1.Children.Add(newEllipse);
        }
    }
}
