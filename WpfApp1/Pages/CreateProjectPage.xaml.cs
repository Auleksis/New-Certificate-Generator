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
using Microsoft.Win32;
using System.Windows.Forms;

namespace WpfApp1.Pages
{
    /// <summary>
    /// Логика взаимодействия для CreateProjectPage.xaml
    /// </summary>
    public partial class CreateProjectPage : Page
    {
        MainWindow window = (MainWindow)System.Windows.Application.Current.MainWindow;

        string imageFile;

        public CreateProjectPage()
        {
            InitializeComponent();            
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            window.mainFrame.NavigationService.GoBack();
        }

        private void ChooseFolder_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if(folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                FolderText.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void OpenImage_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.JPG)|*.JPG|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                imageFile = openFileDialog.FileName;
                ImageFile.Source = new BitmapImage(new Uri(imageFile));
            }
        }

        private void CreateProject_Click(object sender, RoutedEventArgs e)
        {
            Editor editor = new Editor(imageFile);
            editor.Owner = window;
            editor.Show();
        }
    }
}
