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
            //This variable is used if you want to change the page.
            MainWindow = mainWindow;
        }

        private string ComputeSha256Hash(string plainData)
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
                Database.Register(username.Text, hashedpw.ToString(), birthdate.SelectedDate.Value.Date , firstname.Text, lastname.Text, securityvraag.Text, securityans.Text);
                MessageBox.Show("U bent succesvol ingelogd!"+"\n"+"U wordt nu doorgestuurd naar de homepagina." , "Succes");
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


        private void Hyperlink_click(object sender, RoutedEventArgs e)
        {
            var newWindow = new Privacystatement();
            newWindow.Show();
        }

        //This method will be used once someone starts working on the forgot password.
        private void Forgot_password_button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //MainWindow.ChangePage(new ForgotPasswordPage());
        }

        //Once this button is pressed, it will check if the username and/or password are empty. If they are, a message will appear telling the user about it. If they are all filled in, we're gonna check if it's connected to an account.
        private void Login_click(object sender, System.Windows.RoutedEventArgs e)
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
                //In here we're first gonna hash the password and then send the username and hashedpassword to the database. We will return a number and if that number is higher than 0, it means we're logged in. We will send a message to the student and send them to the homepage. If not, we will send a message to the user telling that that account doesn't exist.

                string hashedpw = ComputeSha256Hash(loginPassword);
                MainWindow.Ingelogd = Database.GetAccountIDForLogin(loginUsername, hashedpw);

                if (MainWindow.Ingelogd > 0)
                {
                    MessageBox.Show("U bent succesvol ingelogd! U wordt nu doorgestuurd naar de homepagina.", "Succes");
                    MainWindow.UpdateLoginButton();
                    MainWindow.ChangePage(new HomePage(MainWindow));
                }
                else
                {
                    MessageBox.Show("Er bestaat geen account met deze gegevens!", "Error");
                }
            }

        }

        private void Password_login_textbox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter && MainWindow.IsActive)
            {
                Login_click(sender, e);
            }

            e.Handled = true;
        }
    }
}