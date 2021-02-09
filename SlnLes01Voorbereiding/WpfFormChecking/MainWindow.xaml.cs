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

namespace WpfFormChecking
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
            int aantalFouten = 0;
            if (txtNaam.Text == "")
            {
                foutNaam.Content = "naam moet ingevuld zijn";
                aantalFouten++;
            }

            if (txtEmail.Text == "")
            {
                foutEmail.Content = "Email moet ingevuld zijn";
                aantalFouten++;
            }
            if (dteGeboorte.SelectedDate == null)
            {
                foutGeboorte.Content = "datum moet ingevuld zijn";
                aantalFouten++;
            }
            if (cbxProfiel.SelectedItem == null)
            {
                foutProfiel.Content = "maak een keuze";
                aantalFouten++;
            }
            if (txtPassword.Text == "")
            {
                foutPasswoord.Content = "kies een passwoord";
                aantalFouten++;
            }
            if (rbnMan.IsChecked == false && rbnVrouw.IsChecked == false)
            {
                foutGeslacht.Content = "kies een geslacht";
                aantalFouten++;
            }
            if (cbProgrammeren.IsChecked == false && cbProgrammeren.IsChecked == false)
            {
                if (cbNetwerken.IsChecked == false)
                {
                    foutInteresses.Content = "kies minstens één interesse";
                    aantalFouten++;

                }
            }
           

            lblAantalFouten.Content = Convert.ToString($"dit formulier bevat {aantalFouten} fouten");
        }

        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            cbxProfiel.Items.Add("Test1");
            cbxProfiel.Items.Add("Test2");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (txtNaam.Text != "")
            {
                txtNaam.Text = "";
     
            }

            if (txtEmail.Text != "")
            {
                txtEmail.Text = "";
                foutEmail.Content = "";
              
            }
            if (dteGeboorte.SelectedDate != null)
            {
                dteGeboorte.SelectedDate = null;
                foutGeboorte.Content = "";
             
            }
            if (cbxProfiel.SelectedItem != null)
            {
                cbxProfiel.SelectedItem = null;
                foutProfiel.Content = "";
            
            }
            if (txtPassword.Text != "")
            {
                txtPassword.Text = "";
                foutPasswoord.Content = "";
              
            }
            if (rbnMan.IsChecked != false && rbnVrouw.IsChecked != false)
            {
                rbnMan.IsChecked = false;
                rbnVrouw.IsChecked = false;
                foutGeslacht.Content = "";
               
            }
            if (cbProgrammeren.IsChecked != false)
            {
                cbProgrammeren.IsChecked = false;
                foutProfiel.Content = "";
            }
            if (cbNetwerken.IsChecked != false)
            {
                cbNetwerken.IsChecked = false;

            }
            if (cbBusiness.IsChecked != false)
            {
                cbBusiness.IsChecked = false;
            }
        }
    }
}
