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
            List<Profile> profiles = new List<Profile>();
            profiles.Add(new Profile() { profile = "Moore", username = "doej", host = "moore.cas.mcmaster.ca", password="password", port=22 });
            profiles.Add(new Profile() { profile = "Moore Alt", username = "johnsgm", host = "moore.cas.mcmaster.ca", password = "password", port = 22 });
            listView.ItemsSource = profiles;
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
            Profile selectedProfile = button.DataContext as Profile;
            Console.Write(selectedProfile.profile);

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ConnectionPage());
        }
    }

    public class Profile
    {
        public string profile { get; set; }

        public string username { get; set; }

        public string host { get; set; }

        public string password { get; set; }

        public int port { get; set; }
    }
}
