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
            //Registreerbtn checkt of alles ingevuld is.
            //Als er iets niet ingevuld was en dat men een foutmelding krijgt moet hij het invullen en dan terug op regristreren clicken en dan gaat die foutmelding weg.
            int aantalFouten = 0;
            if (txtNaam.Text == "")
            {
                foutNaam.Content = "naam moet ingevuld zijn";
                aantalFouten++;
            }
            else if (txtNaam.Text != "")
            {
                foutNaam.Content = "";
            }

            if (txtEmail.Text == "")
            {
                foutEmail.Content = "Email moet ingevuld zijn";
                aantalFouten++;
            }
            else if (txtEmail.Text != "")
            {
                foutEmail.Content = "";
            }

            if (dteGeboorte.SelectedDate == null)
            {
                foutGeboorte.Content = "datum moet ingevuld zijn";
                aantalFouten++;
            }
            else if (dteGeboorte.SelectedDate != null)
            {
                foutGeboorte.Content = "";
            }

            if (cbxProfiel.SelectedItem == null)
            {
                foutProfiel.Content = "maak een keuze";
                aantalFouten++;
            }
            else if (cbxProfiel.SelectedItem != null)
            {
                foutProfiel.Content = "";
            }
            if (txtPassword.Text == "")
            {
                foutPasswoord.Content = "kies een passwoord";
                aantalFouten++;
            }
            else if (txtPassword.Text != "")
            {
                foutPasswoord.Content = "";
            }
            if (rbnMan.IsChecked == false && rbnVrouw.IsChecked == false)
            {
                foutGeslacht.Content = "kies een geslacht";
                aantalFouten++;
            }
            else if (rbnMan.IsChecked == true || rbnVrouw.IsChecked == true)
            {
                foutGeslacht.Content = "";
            }

            if (cbProgrammeren.IsChecked == false && cbProgrammeren.IsChecked == false)
            {
                if (cbNetwerken.IsChecked == false)
                {
                    foutInteresses.Content = "kies minstens één interesse";
                    aantalFouten++;

                }
            }
            if (cbProgrammeren.IsChecked == true || cbBusiness.IsChecked == true)
            {
                foutInteresses.Content = "";
            }
            else if (cbNetwerken.IsChecked == true)
            {
                foutInteresses.Content = "";
            }

            if (aantalFouten == 0)
            {
                lblAantalFouten.Content = "bedankt voor de registratie";
            }
            else
            {
                lblAantalFouten.Content = Convert.ToString($"dit formulier bevat {aantalFouten} fouten");
            }

            
        }

        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            //Combobox items
            cbxProfiel.Items.Add("Profiel1");
            cbxProfiel.Items.Add("Profiel2");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Om alles te wissen
            dteGeboorte.SelectedDate = null;
            cbxProfiel.SelectedItem = null;
            txtPassword.Text = null;
            rbnMan.IsChecked = false;
            rbnVrouw.IsChecked = false;
            cbBusiness.IsChecked = false;
            cbNetwerken.IsChecked = false;
            cbProgrammeren.IsChecked = false;
            txtNaam.Text = "";
            txtEmail.Text = "";
            foutInteresses.Content = "";
            lblAantalFouten.Content = "";
            foutEmail.Content = "";
            foutGeboorte.Content = "";
            foutGeslacht.Content = "";
            foutProfiel.Content = "";
            foutPasswoord.Content = "";
            foutInteresses.Content = "";
            foutNaam.Content = "";
        }
    }
}
