using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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
using ItemsLibrary;

namespace WpfLeden
{
    /// <summary>
    /// Logique d'interaction pour Pitems.xaml
    /// </summary>
    public partial class Pitems : Page
    {
        string connString = ConfigurationManager.AppSettings["connString"];
        public Pitems()
        {
            InitializeComponent();
            ReloadEmployees(null);

        }
        public void ReloadEmployees(int? selectedId)
        {
            // wis de lijst
            lstBox.Items.Clear();

            // laad alle werknemers in
            List<BibItems> allEmps = BibItems.GetAll();
            foreach (BibItems emp in allEmps)
            {
                ListBoxItem item = new ListBoxItem();
                item.Content = emp.ToString();
                item.Tag = emp.Id;
                item.IsSelected = selectedId == emp.Id;
                lstBox.Items.Add(item);
            }
        }

        private void lbxResults_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxItem li = (ListBoxItem)lstBox.SelectedItem;
            if (li == null) return;
            int id = (int)li.Tag;
            BibItems mwd = BibItems.GetById(id);
            lblTitel.Content = mwd.Titel;
            lblUitgeverij.Content = mwd.Uitgeverij;
            lblLeeftijdVan.Content = mwd.Leeftijd_van;
            lblLeeftijdTot.Content = mwd.Leeftijd_tot;
            lblTaal.Content = mwd.Taal;
            lblBeschrijving.Text = mwd.Beschrijving;

        }
        public void ReloadEmployees(int selectedId)
        {
            // wis de lijst
            lstBox.Items.Clear();

            // laad alle werknemers in
            List<BibItems> allEmps = BibItems.GetAll();
            foreach (BibItems emp in allEmps)
            {
                ListBoxItem item = new ListBoxItem();
                item.Content = emp.ToString();
                item.Tag = emp.Id;
                item.IsSelected = selectedId == emp.Id;
                lstBox.Items.Add(item);
            }
        }
    }
}
