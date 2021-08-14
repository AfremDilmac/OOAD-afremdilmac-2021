using Library;
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
using System.Windows.Shapes;

namespace WebshopAdmin
{
    /// <summary>
    /// Logique d'interaction pour Klanten.xaml
    /// </summary>
    public partial class Klanten : Window
    {
        public Klanten()
        {
            InitializeComponent();
            // toon alle werknemers in de lijst
            ReloadEmployees(null);
        }

        public void ReloadEmployees(int? selectedId)
        {
            // wis lijst en labels
            lbxResults.Items.Clear();
            lblEmail.Content = "";
            lblGender.Content = "";
            lblGemeente.Content = "";
            lblPostcode.Content = "";
            lblnummer.Content = "";
            lbltelefoonnummer.Content = "";

            // laad alle werknemers in
            List<Users> allEmps = Users.GetAll();
            foreach (Users emp in allEmps)
            {
                ListBoxItem item = new ListBoxItem();
                item.Content = emp.ToString();
                item.Tag = emp.Id;
                item.IsSelected = selectedId == emp.Id;
                lbxResults.Items.Add(item);
            }
        }
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            ListBoxItem item = (ListBoxItem)lbxResults.SelectedItem;
            int employeeId = Convert.ToInt32(item.Tag);
            /* EditWindow editWin = new EditWindow(this, employeeId);
            editWin.Show();*/
        }
        
        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            /*
            NewWindow newWin = new NewWindow(this);
            newWin.Show();
             */
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            ListBoxItem li = (ListBoxItem)lbxResults.SelectedItem;
            if (li == null) return;
            int lidnummer = Convert.ToInt32(li.Tag);            
            MessageBoxResult result = MessageBox.Show($"Ben je zeker dat je lid #{lidnummer} wil verwijderen?", "Lid verwijderen", MessageBoxButton.YesNo);
            if (result != MessageBoxResult.Yes) return;
            Users lid = Users.FindById(lidnummer);
            lid.DeleteFromDb();
            ReloadEmployees(null);
        }
        private void LbxResults_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // stel button states in
            ListBoxItem item = (ListBoxItem)lbxResults.SelectedItem;
            btnEdit.IsEnabled = item != null;
            btnRemove.IsEnabled = item != null;
            if (item == null) return;

            // als een werknemer geselecteerd is, vraag details op
            int employeeId = Convert.ToInt32(item.Tag);
            Users emp = Users.FindById(employeeId);
            lblEmail.Content = emp.email;
            string gender = emp.geslacht;
            lblGender.Content = gender == "M" ? "man" : gender == "V" ? "vrouw" : "onbekend";
            lblGemeente.Content = emp.gemeente;
            lblPostcode.Content = emp.postcode;
            lblnummer.Content = emp.nummer;
            lbltelefoonnummer.Content = emp.telefoonnummer;
         
        }
    }
}
