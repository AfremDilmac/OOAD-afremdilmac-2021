using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
using System.Windows.Shapes;
using ItemsLibrary;


namespace WpfBalieMedewerkers
{
    /// <summary>
    /// Logique d'interaction pour ItemEdit.xaml
    /// </summary>
    public partial class ItemEdit : Window
    {
        Items test;
        BibItems emp;
        string connString = ConfigurationManager.AppSettings["connString"];

        public ItemEdit(Items test, int id)
        {
            //Textboxen invullen met gegevens dat geselecteerd zijn in de listbox
            InitializeComponent();
            this.test = test;
            emp = BibItems.FindById(id);
            txtTitel.Text = Convert.ToString(emp.Titel);
            txtBeschrijving.Text = Convert.ToString(emp.Beschrijving);
            txtUitgeverij.Text = Convert.ToString(emp.Uitgeverij);
            txtLeeftijdTot.Text = Convert.ToString(emp.Leeftijd_tot);
            txtLeeftijdVan.Text = Convert.ToString(emp.Leeftijd_van);
            txtTaal.Text = Convert.ToString(emp.Taal);
          
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //Opslaan button
            using (SqlConnection conn = new SqlConnection(connString))
            {
                emp.Beschrijving = txtBeschrijving.Text;
                emp.Titel = txtTitel.Text;
                emp.Leeftijd_van = Convert.ToInt32(txtLeeftijdVan.Text);
                emp.Leeftijd_tot = Convert.ToInt32(txtLeeftijdTot.Text);
                emp.Taal = txtTaal.Text;
                emp.Uitgeverij = txtUitgeverij.Text;
                emp.InsertInDb();
            }

            // herlaad hoofdvenster, en sluit dit venster
            test.ReloadEmployees(emp.Id);
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
