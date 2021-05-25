using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// Logique d'interaction pour ItemInsert.xaml
    /// </summary>
    public partial class ItemInsert : Window
    {
        BibItems emp;
       
        Items test;
        string connString = ConfigurationManager.AppSettings["connString"];
        public ItemInsert(Items mainWin, int id)
        {
            InitializeComponent();
            this.test = mainWin;
            emp = BibItems.FindById(id);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            emp.Beschrijving = txtBeschrijving.Text;
            emp.Titel = txtTitel.Text;
            emp.Leeftijd_van = Convert.ToInt32(txtLeeftijdVan.Text);
            emp.Leeftijd_tot = Convert.ToInt32(txtLeeftijdTot.Text);
            emp.Taal = txtTaal.Text;
            emp.Uitgeverij = txtUitgeverij.Text;
            emp.InsertInDb();

            // herlaad hoofdvenster, en sluit dit venster
            test.ReloadEmployees(emp.Id);
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            // sluit dit venster
            this.Close();
        }
    }
}
