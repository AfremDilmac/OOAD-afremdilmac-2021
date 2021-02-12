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
            //Nieuwe lijn
            string newLine = Environment.NewLine;
            //Hier kan ik woorden toevoegen om scheldwoorden te vervangen door ***
            boxBericht.Text = a;
            a = a.Replace("Fuck", "***").Replace("Dombo", "***").Replace("Gemeenerik", "***");

           //Text word hiermee gestuurd naar chat.
            string text = Convert.ToString($"{boxNaam.Text} says: \n {a}" + newLine);
            blockChat.Text += text + newLine;
            //Na clicken op button verzenden wordt Naam & Bericht leeg.
            Convert.ToString(boxNaam.Text = "");
            Convert.ToString(boxBericht.Text = "");

        }
    }
}
