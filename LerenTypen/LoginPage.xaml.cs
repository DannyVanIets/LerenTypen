using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
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

        static string ComputeSha256Hash(string plainData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(plainData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
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

        private void login_click(object sender, System.Windows.RoutedEventArgs e)
        {
            string loginUsername = username_login_textbox.Text;
            string loginPassword = password_login_textbox.Password;

            if (string.IsNullOrEmpty(loginUsername) && string.IsNullOrEmpty(loginPassword))
            {
                MessageBox.Show("U moet een gebruikersnaam en wachtwoord invoeren!", "Error");
            }
            else if (string.IsNullOrEmpty(loginUsername))
            {
                MessageBox.Show("U moet een gebruikersnaam invoeren!", "Error");
            }
            else if (string.IsNullOrEmpty(loginPassword))
            {
                MessageBox.Show("U moet een wachtwoord invoeren!", "Error");
            }
            else
            {
                string hashedpw = ComputeSha256Hash(loginPassword);
                MainWindow.ingelogd = Database.SelectUsernameAndPasswordQuery(loginUsername, hashedpw);

                if (MainWindow.ingelogd > 0)
                {
                    MessageBox.Show("U bent succescol ingelogd! U wordt nu gestuurd naar de homepagina.", "Succes");
                    MainWindow.menuIngelogdCheck();
                    MainWindow.ChangePage(new HomePage());
                }
                else
                {
                    MessageBox.Show("Er bestaat geen account met deze gegevens!", "Error");
                }
            }
        }
    }
}