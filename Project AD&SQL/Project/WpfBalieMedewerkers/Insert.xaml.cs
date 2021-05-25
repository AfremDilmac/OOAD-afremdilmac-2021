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
using ClassLibrary;

namespace WpfBalieMedewerkers
{
    /// <summary>
    /// Logique d'interaction pour Insert.xaml
    /// </summary>
    public partial class Insert : Window
    {
        // referentie naar hoofdvenster
        Medewerker mainWin;
        Library emp;

        // connectiestring nodig om te connecteren met de databank
        string connString = ConfigurationManager.AppSettings["connString"];
        public Insert(Medewerker mainWin, int id)
        {
            InitializeComponent();
            this.mainWin = mainWin;
            emp = Library.FindById(id);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            // sluit dit venster
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                emp.Lidnummer = txtLidnummer.Text;
                emp.Gsm = txtGsm.Text;
                emp.Gemeente = txtGemeente.Text;
                emp.Achternaam = txtLast.Text;
                emp.Nummer = txtNummer.Text;
                emp.Postcode = txtPostcode.Text;
                emp.Straat = txtStraat.Text;


                DateTime geboortedatum = (DateTime)emp.Geboortedatum;
                DateTime vervaldatum_lidkaart = (DateTime)emp.vervaldatum_lidkaart;
                emp.InsertInDb();
            }


            // herlaad hoofdvenster, en sluit dit venster
            mainWin.ReloadEmployees(emp.Lidnummer);
            this.Close();
        }
    }
}
