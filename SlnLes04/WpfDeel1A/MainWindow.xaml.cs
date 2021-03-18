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

namespace WpfDeel1A
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            miSave.IsEnabled = false;
            miSaveAs.IsEnabled = false;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            //Open
            if (tbxContent.Text != "")
            {
                var Result = MessageBox.Show($"Als je nu het programma verlaat, gaan wijzingen verloren", "Warning !", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (Result == MessageBoxResult.OK)
                {
                    OpenFileDialog dialog = new OpenFileDialog();
                    dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    dialog.Filter = "Tekstbestanden|*.TXT;*.TEXT";
                    string chosenFileName;
                    if (dialog.ShowDialog() == true)
                    {

                        // user picked a file and pressed OK
                        chosenFileName = dialog.FileName;
                        tbxContent.Text = File.ReadAllText(dialog.FileName);
                        FileInfo fi = new FileInfo(chosenFileName);
                        lblStatus.Content = $"#chars: {fi.Length}";
                        miSave.IsEnabled = true;
                        tiTitle.Header = fi.Name;
                        currentFilePath = dialog.InitialDirectory;
                    }
                    else
                    {
                        miSave.IsEnabled = false;
                        // user cancelled or escaped dialog window
                    }
                }
                else if (Result == MessageBoxResult.Cancel)
                {
                   
                }

            }
            else if (tbxContent.Text == "")
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                dialog.Filter = "Tekstbestanden|*.TXT;*.TEXT";
                string chosenFileName;
                if (dialog.ShowDialog() == true)
                {

                    // user picked a file and pressed OK
                    chosenFileName = dialog.FileName;
                    tbxContent.Text = File.ReadAllText(dialog.FileName);
                    FileInfo fi = new FileInfo(chosenFileName);
                    lblStatus.Content = $"#chars: {fi.Length}";
                    miSave.IsEnabled = true;
                    tiTitle.Header = fi.Name;
                    currentFilePath = dialog.InitialDirectory;
                }
                else
                {
                    miSave.IsEnabled = false;
                    // user cancelled or escaped dialog window
                }
            }
        }
        private string currentFilePath = "";
        private string initialFolderPath;
       
        private void exitItem_Click(object sender, RoutedEventArgs e)
        {
            //Exit
            try
            {
                if (miSave.IsEnabled == true || miSaveAs.IsEnabled == true)
                {
                    MessageBox.Show(
                   $"Als je nu het programma verlaat, gaan wijzingen verloren", "Warning !", MessageBoxButton.OKCancel,  MessageBoxImage.Warning
                   );
                    // 0 wilt zeggen dat er niets is fout gelopen
                    Environment.Exit(0);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            //About
            Window1 sW = new Window1();
            sW.Show();

        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            //Copy
            string text = Clipboard.GetText();
            Clipboard.SetText(tbxContent.SelectedText);

            if (Clipboard.ContainsText() == false)
            {
                miPaste.IsEnabled = false;
            }

        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            //Cut
            string text = Clipboard.GetText();
            Clipboard.SetText(tbxContent.SelectedText);
            if (tbxContent.SelectedText != "")
            {
                tbxContent.SelectedText = "";
            }
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            //Paste
            string text = Clipboard.GetText();
            tbxContent.Text = tbxContent.Text.Insert(tbxContent.SelectionStart, text);

        }

        private void tbxContent_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (tbxContent.SelectedText == "")
            {
                miCopy.IsEnabled = false;
                miCut.IsEnabled = false;
              
            }
            if (tbxContent.SelectedText != "")
            {
                miCopy.IsEnabled = true;
                miCut.IsEnabled = true;
               
            }

            if (tbxContent.Text != "")
            {
                miSave.IsEnabled = true;
                miSaveAs.IsEnabled = true;

            }

            lblStatus.Content = $"#chars: {tbxContent.Text.Length}";
        }

        private void miSave_Click(object sender, RoutedEventArgs e)
        {
            //Save 

            if (currentFilePath == "")
            {
                StreamWriter writer;
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "Tekstbestanden|*.TXT;*.TEXT";
                if (dialog.ShowDialog() == true)
                {
                    currentFilePath = dialog.FileName;
                    writer = File.CreateText(currentFilePath);
              
                    writer.Write(tbxContent.Text);
                    writer.Close();
                }

            }
            else if (currentFilePath != "")
            {
                // prepare
                string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string filePath = System.IO.Path.Combine(folderPath, "myfile.txt");
                // write all text at once
                File.WriteAllText(filePath, tbxContent.Text);

            }

           

        }
           
        private void miSaveAs_Click(object sender, RoutedEventArgs e)
        {
            StreamWriter writer;
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Tekstbestanden|*.TXT;*.TEXT";
            dialog.InitialDirectory = initialFolderPath;
            if (dialog.ShowDialog() == true)
            {
                currentFilePath = dialog.FileName;
                writer = File.CreateText(currentFilePath);
                writer.Write(tbxContent.Text);
                writer.Close();
            }
        }

        private void miNew_Click(object sender, RoutedEventArgs e)
        {
            tbxContent.Text = "";
        }
    }
}
