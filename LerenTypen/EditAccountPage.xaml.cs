using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LerenTypen
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class EditAccountPage : Page
    {
        private MainWindow MainWindow;
        private Account Account;
        private Classes.Converter Converter;

        public EditAccountPage(MainWindow mainWindow)
        {
            InitializeComponent();
            MainWindow = mainWindow;
            Converter = new Classes.Converter();

            if (mainWindow.Ingelogd > 0)
            {
                Account = Database.GetAllAccountInformationExceptPassword(mainWindow.Ingelogd);

                firstNameTextBox.Text = Account.FirstName;
                lastNameTextbox.Text = Account.Surname;

                usernameTextBox.Text = Account.UserName;
                birthdateDatePicker.SelectedDate = Account.Birthdate;

                securityQuestionComboBox.Text = Account.SecurityQuestion;
                securityAnswerTextBox.Text = Account.SecurityAnswer;
            }
            else
            {
                MessageBox.Show("U bent niet ingelogd!", "Error");
                MainWindow.ChangePage(new HomePage(mainWindow));
            }
        }

        private void DeleteAccountButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            deleteAccountButton.Foreground = Brushes.Black;
        }

        private void DeleteAccountButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            deleteAccountButton.Foreground = Brushes.White;
        }

        private void SaveButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string firstname = firstNameTextBox.Text;
            string surname = lastNameTextbox.Text;
            string username = usernameTextBox.Text;

            string oldPassword = oldPasswordTextBox.Password;
            string newPassword = newPasswordTextBox.Password;
            string newPasswordRepeat = passwordRepeatTextBox.Password;

            DateTime birthdate = (DateTime)birthdateDatePicker.SelectedDate;
            string securityQuestion = securityQuestionComboBox.Text;
            string securityAnswer = securityAnswerTextBox.Text;

            if (string.IsNullOrEmpty(oldPassword) && string.IsNullOrEmpty(newPassword) && string.IsNullOrEmpty(newPasswordRepeat))
            {
                //Here we will update everything, except the password.
                if (Database.UpdateAccountWithoutPassword(MainWindow.Ingelogd, username, birthdate, firstname, surname, securityQuestion, securityAnswer))
                {
                    MessageBox.Show("Het account wordt succesvol geüpdate!", "Succes");
                    MainWindow.ChangePage(new EditAccountPage(MainWindow));
                }
                else
                {
                    MessageBox.Show("Het account kon niet worden geüpdate, probeer het opnieuw of neem contact op met de beheerders.", "Error");
                }
            }
            else if (string.IsNullOrEmpty(oldPassword) && (!string.IsNullOrEmpty(newPassword) || !string.IsNullOrEmpty(newPasswordRepeat)))
            {
                //Here we are gonna give errors for the password
                MessageBox.Show("U moet het oude wachtwoord invoeren voordat u een nieuw wachtwoord kunt invoeren.", "Error");
            }
            else if (!string.IsNullOrEmpty(oldPassword) && (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(newPasswordRepeat)))
            {
                //Here we are gonna give errors for the password
                MessageBox.Show("U moet een nieuw wachtwoord en herhaling van het nieuwe wachtwoord invoeren!", "Error");
            }
            else if (Converter.ComputeSha256Hash(oldPassword) != Database.GetPasswordFromAccount(MainWindow.Ingelogd))
            {
                MessageBox.Show("Dat is niet het goede oude wachtwoord!", "Error");
            }
            else if (newPassword != newPasswordRepeat)
            {
                MessageBox.Show("De nieuwe wachtwoorden komen niet overeen.", "Error");
            }
            else
            {
                //Hier wordt alles geüpdate!
                string hashedNewPassword = Converter.ComputeSha256Hash(newPassword);

                if (Database.UpdateAccountWithPassword(MainWindow.Ingelogd, username, hashedNewPassword, birthdate, firstname, surname, securityQuestion, securityAnswer))
                {
                    MessageBox.Show("Het account wordt succesvol geüpdate!", "Succes");
                    MainWindow.ChangePage(new EditAccountPage(MainWindow));
                }
                else
                {
                    MessageBox.Show("Het account kon niet worden geüpdate, probeer het opnieuw of neem contact op met de beheerders.", "Error");
                }
            }
        }
    }
}
