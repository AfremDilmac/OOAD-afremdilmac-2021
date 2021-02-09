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

namespace WpfChat
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

        private void btnVerzenden_Click(object sender, RoutedEventArgs e)
        {
            
            string a = boxBericht.Text;
            string newLine = Environment.NewLine;
            boxBericht.Text = a;
            a = a.Replace("Fuck", "***").Replace("Dombo", "***").Replace("Gemeenerik", "***");
           
            string text = Convert.ToString($"{boxNaam.Text} says: \n {a}" + newLine);
            blockChat.Text += text + newLine;

            Convert.ToString(boxNaam.Text = "");
            Convert.ToString(boxBericht.Text = "");

        }
    }
}
