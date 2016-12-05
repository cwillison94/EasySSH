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
    /// Interaction logic for ConnectionPage.xaml
    /// </summary>
    public partial class ConnectionPage : Page
    {
        public ConnectionPage()
        {
            InitializeComponent();
            this.ComboBoxQuickConnect.DisplayMemberPath = "QuickName";
            this.ComboBoxQuickConnect.SelectedValuePath = "ProfileID";
            this.ComboBoxQuickConnect.ItemsSource = SavedProfiles.Instance.ListAll();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = (MenuItem)sender;

            System.Diagnostics.Debug.WriteLine("Menu Item Name: " + menuItem.Name.ToString());
            switch (menuItem.Name.ToString())
            {
                case "settings":
                    //this.NavigationService.Navigate(new SettingsPage());
                    break;
                case "manageProfile":
                    //this.NavigationService.Navigate(new ManageProfilePage());

                    break;
                case "about":
                    //this.NavigationService.Navigate(new AboutPage());

                    break;
            }
        }
    }
}
