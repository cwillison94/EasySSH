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
        private bool profileChanged = false;

        public ConnectionPage()
        {
            InitializeComponent();
            this.ComboBoxQuickConnect.DisplayMemberPath = "QuickName";
            this.ComboBoxQuickConnect.SelectedValuePath = "ProfileID";
            this.ComboBoxQuickConnect.ItemsSource = SavedProfiles.Instance.ListAll();

            this.TBCommandLineDisplay.Text = @"ssh <username> @<host>";
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = (MenuItem)sender;

            System.Diagnostics.Debug.WriteLine("Menu Item Name: " + menuItem.Name.ToString());
            switch (menuItem.Name.ToString())
            {
                case "settings":
                    this.NavigationService.Navigate(new SettingsPage());
                    break;
                case "manageProfile":
                    this.NavigationService.Navigate(new ManageProfilePage());
                    break;
                case "about":
                    this.NavigationService.Navigate(new AboutPage());
                    break;
            }
        }

        private void ClearInputs()
        {
            this.TBHost.Clear();
            this.TBPort.Clear();
            this.TBUserName.Clear();
            this.TBPassword.Clear();
            this.TBProfileName.Clear();
        }

        private void UpdateCommandDisplay()
        {
            var username = this.TBUserName.Text == "" ? "<username>" : this.TBUserName.Text;
            var host = this.TBHost.Text == "" ? "<username>" : this.TBHost.Text;

            var commandDisplay = "ssh " + username + "@" + host;

            if (this.TBPort.Text != "")
            {
                commandDisplay += " -p " + this.TBPort.Text;
            }

            this.TBCommandLineDisplay.Text = commandDisplay;
        }

        private void ButtonConnect_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            ClearInputs();
        }

        private void TBHost_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateCommandDisplay();
        }

        private void TBUserName_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateCommandDisplay();
        }

        private void TBPort_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateCommandDisplay();
        }

        private void TBProfileName_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void CBSaveProfile_Checked(object sender, RoutedEventArgs e)
        {
            if (this.TBProfileName != null)
                this.TBProfileName.IsEnabled = this.CBSaveProfile.IsChecked.Value;
        }

        private void CBSaveProfile_Unchecked(object sender, RoutedEventArgs e)
        {
            if (this.TBProfileName != null)
                this.TBProfileName.IsEnabled = this.CBSaveProfile.IsChecked.Value;
        }

        private void ComboBoxQuickConnect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var profile = (SavedProfile)this.ComboBoxQuickConnect.SelectedItem;

            this.TBHost.Text = profile.Host;
            this.TBPort.Text = profile.Port.ToString();
            this.TBUserName.Text = profile.UserName;
            this.TBPassword.Password = profile.Password;
            this.TBProfileName.Text = profile.ProfileName;
        }

    }
}
