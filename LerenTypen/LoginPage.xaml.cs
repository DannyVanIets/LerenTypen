
using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace LerenTypen
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();

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
        // Er word gekeken als de velden ingevuld zijn, anders word alles afgebroken
        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (gebruikernaam.Text.Trim() == "" || voornaam.Text.Trim() == "" || achternaam.Text.Trim() == "" || wachtwoordd.Password.Trim() == "" || wachtwoordd.Password.Trim() == "" || geboortedatum.Text.Trim() == "" || securityans.Text.Trim() == "")
            {
                MessageBox.Show("Vul alle velden in!", "Velden niet ingevuld!");
                return;
            }

            /*Het wachtwoord en wachtwoord herhalen field worden vergeleken, als ze anders zijn krijg je melding.
            Klik op de registreer button en het wachtwoord word gehasht.*/
            if (wachtwoordd.Password == wachtwoordherh.Password)
            {  
                string hashedpw = ComputeSha256Hash(wachtwoordd.Password);
                Database.Registreer(gebruikernaam.Text, hashedpw.ToString(), geboortedatum.Text, voornaam.Text, achternaam.Text, securityvraag.Text ,securityans.Text);
                MessageBox.Show("Er is succesvol geregistreerd!" , "Succesvol Geregistreerd!");
                gebruikernaam.Text = string.Empty; achternaam.Text = string.Empty;
                voornaam.Text = string.Empty; wachtwoordd.Password = string.Empty;
                wachtwoordherh.Password = string.Empty; geboortedatum.Text = string.Empty; securityans.Text = string.Empty;
            }
            else
            {
                MessageBox.Show("De wachtwoorden zijn niet gelijk!", "Wachtwoorden ongelijk!");
            }

        }
        //De button word niet clickable als de checkbox unchecked is.
        private void CheckBox_unChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            if (checkboxakkoord.IsChecked == false)
            {
                accountmaken.IsEnabled = false;
            }
        }
        //De button word clickable als de checkbox checked is.
        private void CheckBox_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            if (checkboxakkoord.IsChecked == true)
            {
                accountmaken.IsEnabled = true;

            }
        }

        private void Hyperlink_click(object sender , RoutedEventArgs e)
        {
            var newWindow = new Privacystatement();
            newWindow.Show();
           }

        protected void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string gebruiker = gebruikernaam.Text;
            string voorna = voornaam.Text;
            string achtern = achternaam.Text;
            string password = wachtwoordd.Password;
            string passherh = wachtwoordherh.Password;
            string geboorte = geboortedatum.Text;
            string securtiyvraag = securityvraag.Text;
            string securityans = securityvraag.Text;

        }
    }
}
