using Microsoft.Win32;
using System;
using System.Collections.Generic;
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

namespace WpfFileInfo
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

        private void btnBestand_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dialog.Filter = "Tekstbestanden|*.TXT;*.TEXT";
            string chosenFileName;
            bool? dialogResult = dialog.ShowDialog();
            if (dialogResult == true)
            {
                // user picked a file and pressed OK
                chosenFileName = dialog.FileName; // user accepted
                FileInfo fi = new FileInfo(chosenFileName);
                lblOutput.Content += $"bestandsnaam: {fi.Name}{Environment.NewLine}";
                lblOutput.Content += $"extensie: {fi.Extension}{Environment.NewLine}";
                lblOutput.Content += $"gemaakt op: {fi.CreationTime.ToString()}{Environment.NewLine}";
                lblOutput.Content += $"mapnaam: {fi.Directory.Name}{Environment.NewLine}";
                lblOutput.Content += $"aantal woorden: {fi.Length}";
            }
            else
            {
                lblOutput.Content = "geen bestand gekozen";
            }
        }
    }
}
