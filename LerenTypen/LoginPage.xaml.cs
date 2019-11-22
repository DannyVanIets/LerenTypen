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
        // Er word gekeken als de velden ingevuld zijn, anders word alles afgebroken
        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (username.Text.Trim() == "" || firstname.Text.Trim() == "" || lastname.Text.Trim() == "" || password.Password.Trim() == "" || passwordherh.Password.Trim() == "" || birthdate.Text.Trim() == "" || securityans.Text.Trim() == "")
            {
                MessageBox.Show("Vul alle velden in!", "Velden niet ingevuld!");
                return;
            }

            if (Database.UserExists(username.Text))
            {
                MessageBox.Show("Deze gebruikersnaam is al in gebruik!","Gebruikersnaam in gebruik");
                return;
            }

            /*Het wachtwoord en wachtwoord herhalen field worden vergeleken, als ze anders zijn krijg je melding.
            Klik op de registreer button en het wachtwoord word gehasht.*/
            if (password.Password == passwordherh.Password)
            {  
                string hashedpw = ComputeSha256Hash(password.Password);
                Database.Registrer(username.Text, hashedpw.ToString(), birthdate.SelectedDate.Value.Date , firstname.Text, lastname.Text, securityvraag.Text, securityans.Text);
                MessageBox.Show("U bent succesvol geregistreerd!" , "Succesvol geregistreerd!");
                username.Text = string.Empty; lastname.Text = string.Empty;
                firstname.Text = string.Empty; password.Password = string.Empty;
                passwordherh.Password = string.Empty; birthdate.Text = string.Empty; securityans.Text = string.Empty;
            }
            else
            {
                MessageBox.Show("De wachtwoorden zijn niet gelijk!", "Wachtwoorden ongelijk!");
            }

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string gebruiker = username.Text;
            string voorna = firstname.Text;
            string achtern = lastname.Text;
            string passwordd = password.Password;
            string passherh = passwordherh.Password;
            string geboorte = birthdate.Text;
            string securtiyvraag = securityvraag.Text;
            string securityans = securityvraag.Text;
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

        private void Hyperlink_click(object sender , RoutedEventArgs e)
        {
            var newWindow = new Privacystatement();
            newWindow.Show();
        }
    }
}
