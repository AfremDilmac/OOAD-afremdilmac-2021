﻿using System;
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

namespace WpfPunten
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
        
        private void lstLijst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Hier kan men een ingevoerde item aanpassen 
            string[] array = lstLijst.SelectedItem.ToString().Split('-');
            boxNaam.Text = array[0];
            boxPunt.Text = array[1];

            if (boxNaam.Text == array[0])
            {
                lstLijst.SelectedItem = ($"{boxNaam.Text} - {boxPunt.Text}");
            }
        }

        private void btnToevoegen_Click(object sender, RoutedEventArgs e)
        {
            //naam en punt / 100 wordt toegevoegd naar de lijst

            string text =  ($"{boxNaam.Text} - {boxPunt.Text}/100");
           
            lstLijst.Items.Add(text);

        }

        private void btnVerwijder_Click(object sender, RoutedEventArgs e)
        {
            //Hier kan men een item in de lijst kiezen en verwijderen (vb: een item is aangepast dan kan men de oude versie verwijderen)
            lstLijst.Items.RemoveAt(lstLijst.Items.IndexOf(lstLijst.SelectedItem));
        }

       
    }
}
