using System;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace LerenTypen
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private MainWindow MainWindow;

        public LoginPage(MainWindow mainWindow)
        {
            InitializeComponent();
            MainWindow = mainWindow;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string gebruikersnaam = gebruikernaam.Text;
            string voorn = voornaam.Text;
            string achtern = achternaam.Text;
            string ww = wachtwoord.Text;
            string wwherh = wachtwoordherh.Text;
            string geboort = geboortedatum.Text;
            string security = securityvraag.Text;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
        private void CheckBox_unChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            if (checkboxakkoord.IsChecked == false)
            {
                accountmaken.IsEnabled = false;
            }
        }

        private void CheckBox_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            if (checkboxakkoord.IsChecked == true)
            {
                accountmaken.IsEnabled = true;
            }
        }

        private void forgot_password_button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindow.ChangePage(new ForgotPasswordPage());
        }
    }
}
