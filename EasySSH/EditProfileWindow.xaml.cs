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
    /// Interaction logic for EditProfileWindow.xaml
    /// </summary>
    public partial class EditProfileWindow : Window
    {
        SavedProfile profile;
        public EditProfileWindow(SavedProfile selected_profile)
        {
            this.profile = selected_profile;
            InitializeComponent();
            this.profile_textBox.Text = this.profile.ProfileName;
            this.username_textBox.Text = this.profile.UserName;
            this.host_textBox.Text = this.profile.Host;
            this.password_textBox.Text = this.profile.Password;
            this.port_textBox.Text = this.profile.Port.ToString();
        }

        private void discard_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void confirm_Click(object sender, RoutedEventArgs e)
        {
            this.profile.ProfileName = this.profile_textBox.Text;
            this.profile.UserName = this.username_textBox.Text;
            this.profile.Host = this.host_textBox.Text;
            this.profile.Password = this.password_textBox.Text;
            int x = this.profile.Port;
            int.TryParse(this.port_textBox.Text, out x);
            this.profile.Port = x;
            this.Close();
        }
    }
}
