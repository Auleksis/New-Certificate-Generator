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

namespace WpfApp1.Pages
{
    /// <summary>
    /// Логика взаимодействия для HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public ShowTitles ShowTitles { get; set; }

        MainWindow window = (MainWindow)Application.Current.MainWindow;

        public HomePage()
        {
            ShowTitles = new ShowTitles() { "Aulex Soft" };
            InitializeComponent();
            ShowTitles.Add("show");
            last.ItemsSource = ShowTitles;
        }

        private void create_Click(object sender, RoutedEventArgs e)
        {
            window.mainFrame.NavigationService.Navigate(new Uri("Pages/CreateProjectPage.xaml", UriKind.Relative));
        }
    }
}
