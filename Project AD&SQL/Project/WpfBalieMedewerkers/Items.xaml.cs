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


namespace WpfBalieMedewerkers
{
    /// <summary>
    /// Logique d'interaction pour Items.xaml
    /// </summary>
    public partial class Items : Page
    {
        string connString = ConfigurationManager.AppSettings["connString"];
        public Items()
        {

           // Reload(null);
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

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            ListBoxItem item = (ListBoxItem)lstBox.SelectedItem;
            //Als er hier een error is is het omdat men eerst iets moet selecteren en dan op insert drukken
            int employeeId = Convert.ToInt32(item.Tag);
            ItemInsert newWin = new ItemInsert(this, employeeId);
            newWin.Show();
        }

        private void btnModify_Click(object sender, RoutedEventArgs e)
        {
            ListBoxItem item = (ListBoxItem)lstBox.SelectedItem;
            //Als er hier een error is is het omdat men eerst iets moet selecteren en dan op insert drukken
            int employeeId = Convert.ToInt32(item.Tag);
            ItemEdit editWin = new ItemEdit(this, employeeId);
            editWin.Show();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            // vraag id geselecteerde werknemer op
            ListBoxItem item = (ListBoxItem)lstBox.SelectedItem;
            int employeeId = Convert.ToInt32(item.Tag);
            BibItems emp = BibItems.FindById(employeeId);

            // vraag bevestiging
            MessageBoxResult result = MessageBox.Show($"Ben je zeker dat je item #{employeeId} wil verwijderen?", "Werknemer verwijderen", MessageBoxButton.YesNo);
            if (result != MessageBoxResult.Yes) return;
            // verwijder werknemer
            emp.DeleteFromDb();
            ReloadEmployees(null);
        }
    }
}
