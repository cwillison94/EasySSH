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

namespace EasySSH
{
    /// <summary>
    /// Interaction logic for ManageProfile.xaml
    /// </summary>
    public partial class ManageProfilePage : Page
    {
        public ManageProfilePage()
        {
            InitializeComponent();

            this.listView.ItemsSource = SavedProfiles.Instance.ListAll();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = (MenuItem)sender;

            System.Diagnostics.Debug.WriteLine("Menu Item Name: " + menuItem.Name.ToString());
            switch (menuItem.Name.ToString())
            {
                case "connection":
                    this.NavigationService.Navigate(new ConnectionPage());

                    break;

                case "settings":
                    //this.NavigationService.Navigate(new SettingsPage());
                    break;

                case "about":
                    //this.NavigationService.Navigate(new AboutPage());

                    break;
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            SavedProfile selectedProfile = button.DataContext as SavedProfile;

            System.Diagnostics.Debug.WriteLine(selectedProfile.QuickName);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ConnectionPage());
        }
    }
}
