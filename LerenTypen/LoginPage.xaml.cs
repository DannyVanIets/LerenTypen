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
        private MainWindow MainWindow;

        public LoginPage(MainWindow mainWindow)
        {
            InitializeComponent();
            //This variable is used if you want to change the page.
            MainWindow = mainWindow;
        }

        //This method will be used once someone starts working on the forgot password.
        private void Forgot_password_button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindow.ChangePage(new ForgotPasswordPage());
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
                    MainWindow.UpdateLoginText();
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