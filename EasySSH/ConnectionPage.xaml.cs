using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        private Random randGen = new Random(Guid.NewGuid().GetHashCode());

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
            this.ComboBoxQuickConnect.SelectedIndex = -1;
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
            var missing = new List<string>();

            if (this.TBHost.Text == "") missing.Add("host");
            if (this.TBUserName.Text == "") missing.Add("username");
            if (this.TBPassword.Password == "") missing.Add("password"); ;

            if (missing.Count != 0)
            {
                var missingBase = "Please fill in ";
                var missingFields = "";
                for (var i = 0; i < missing.Count(); i++)
                {
                    missingFields += missing[i] + (i != missing.Count() - 1 ? "," : "");
                }

                this.LabelErrorMessage.Text = missingBase + missingFields;
            }
            else
            {

                if (randGen.NextDouble() <= 0.50)
                {
                    this.LabelErrorMessage.Text = "";

                    if (this.CBSaveProfile.IsChecked.Value) SaveProfile();
                  
                    MessageBox.Show("Successfull connection! This is placeholder text. A terminal window would now open to the SSH connection",
                        "Dummy Success Message",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
                else
                {
                    this.LabelErrorMessage.Text = "Connection Error";
                    MessageBox.Show("Connection Error. A successfull connection could not be resolved. Please check inputs.",
                       "Connection Error!",
                       MessageBoxButton.OK,
                       MessageBoxImage.Error);   
                }
            }
        }

        private void SaveProfile()
        {
 
            var host = this.TBHost.Text;
            var port = int.Parse(this.TBPort.Text == "" ? "22" : this.TBPort.Text);
            var username = this.TBUserName.Text;
            var passowrd = this.TBPassword.Password;
            var profileName = this.TBProfileName.Text;
                    
            if (profileName == "") profileName = username;
            SavedProfiles.Instance.Add(profileName, username, host, passowrd, port);
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

            if (profile != null)
            {
                this.TBHost.Text = profile.Host;
                this.TBPort.Text = profile.Port.ToString();
                this.TBUserName.Text = profile.UserName;
                this.TBPassword.Password = profile.Password;
                this.TBProfileName.Text = profile.ProfileName;
            }
        }

        private void TBPort_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

    }
}
