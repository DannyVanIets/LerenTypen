
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

        // Check if all fields are filled, otherwise show an error message
        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (gebruikernaam.Text.Trim() == "" || voornaam.Text.Trim() == "" || achternaam.Text.Trim() == "" || 
                wachtwoord.Password.Trim() == "" || wachtwoord.Password.Trim() == "" ||
                geboortedatum.Text.Trim() == ""|| securityans.Text.Trim() == "")
            {
                MessageBox.Show("Vul alle velden in!", "Velden niet ingevuld!");
                return;
            }

            if (Database.UserExists(gebruikernaam.Text))
            {
                MessageBox.Show("Deze gebruikersnaam is al in gebruik!","Gebruikersnaam in gebruik");
                return;
            }

            if (wachtwoord.Password == wachtwoordherh.Password)
            {  
                string hashedpw = ComputeSha256Hash(wachtwoord.Password);
                Database.Registrer(gebruikernaam.Text, hashedpw.ToString(), geboortedatum.SelectedDate.Value.Date , voornaam.Text, achternaam.Text, securityvraag.Text, securityans.Text);
                MessageBox.Show("Er is succesvol geregistreerd!", "Succesvol Geregistreerd!");
                gebruikernaam.Text = string.Empty; achternaam.Text = string.Empty;
                voornaam.Text = string.Empty; wachtwoord.Password = string.Empty;
                wachtwoordherh.Password = string.Empty; geboortedatum.Text = string.Empty; securityans.Text = string.Empty; 
            }
            else
            {
                MessageBox.Show("De wachtwoorden zijn niet gelijk!", "Wachtwoorden ongelijk!");
            }

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
                accountmaken.IsEnabled = true; // Nederlands!
            }
        }

        private void Hyperlink_click(object sender , RoutedEventArgs e)
        {
            var privStatement = new Privacystatement();
            privStatement.Show();
        }

        protected void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string gebruiker = gebruikernaam.Text;
            string voorna = voornaam.Text; // Nederlands!
            string achtern = achternaam.Text;
            string password = wachtwoord.Password;
            string passherh = wachtwoordherh.Password;
            string geboorte = geboortedatum.Text;
            string securtiyvraag = securityvraag.Text;
            string securityans = securityvraag.Text;
        }
    }
}
