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
            //MainWindow is used to change pages.
            MainWindow = mainWindow;
            //Converter is used to convert strings to hashed passwords.
            Converter = new Classes.Converter();

            //First we will check if the user is logged in, if not, they will be send back to the Homepage with a message that they're not logged in.
            //If a user is logged in, we will fill in all the information from his account into the textboxes.
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

        //Once this button has been pressed, we will convert everything from the textboxes into strings and datetime.
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

            //We will first check if the textboxes that aren't passwords are empty. If they are, show a message that that is not allowed!
            if (string.IsNullOrEmpty(firstname) || string.IsNullOrEmpty(surname) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(securityQuestion) || string.IsNullOrEmpty(securityAnswer))
            {
                MessageBox.Show("Je moet alle textvelden hebben ingevuld!", "Error");
            }
            else
            {
                //We will check if the oldpassword, newpassword and newpasswordrepeat haven't been filled in and update everything except the passwords.
                if (string.IsNullOrEmpty(oldPassword) && string.IsNullOrEmpty(newPassword) && string.IsNullOrEmpty(newPasswordRepeat))
                {
                    //Here we will update everything, except the password. First we will check if it went succesfully or not.
                    if (Database.UpdateAccountWithoutPassword(MainWindow.Ingelogd, username, birthdate, firstname, surname, securityQuestion, securityAnswer))
                    {
                        MessageBox.Show("Het account wordt succesvol geüpdate!", "Succes");
                        //We do a refresh of the page, so that the old information is updated
                        MainWindow.ChangePage(new EditAccountPage(MainWindow));
                    }
                    else
                    {
                        MessageBox.Show("Het account kon niet worden geüpdate, probeer het opnieuw of neem contact op met de beheerders.", "Error");
                    }
                }
                //In here we will check if the oldpassword hasn't been filled in, but the newpassword and/or newpasswordrepeat have been.
                else if (string.IsNullOrEmpty(oldPassword) && (!string.IsNullOrEmpty(newPassword) || !string.IsNullOrEmpty(newPasswordRepeat)))
                {
                    MessageBox.Show("U moet het oude wachtwoord invoeren voordat u een nieuw wachtwoord kunt invoeren.", "Error");
                }
                //In here we will check if the oldpassword has been filled in, but the newpassword and/or newpasswordrepeat haven't been.
                else if (!string.IsNullOrEmpty(oldPassword) && (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(newPasswordRepeat)))
                {
                    MessageBox.Show("U moet een nieuw wachtwoord en herhaling van het nieuwe wachtwoord invoeren!", "Error");
                }
                //Here we will check if the oldpassword isn't the same one as in the database and give a message if that's true.
                else if (Converter.ComputeSha256Hash(oldPassword) != Database.GetPasswordFromAccount(MainWindow.Ingelogd))
                {
                    MessageBox.Show("Dat is niet het goede oude wachtwoord!", "Error");
                }
                //Here we will check if the newpassword and newpasswordrepeat are both correct.
                else if (newPassword != newPasswordRepeat)
                {
                    MessageBox.Show("De nieuwe wachtwoorden komen niet overeen.", "Error");
                }
                //In the else we will update everything with the passwords.
                else
                {
                    string hashedNewPassword = Converter.ComputeSha256Hash(newPassword);

                    //First we gotta check if it has been succesfully updated. In both cases we will give out a message.
                    if (Database.UpdateAccountWithPassword(MainWindow.Ingelogd, username, hashedNewPassword, birthdate, firstname, surname, securityQuestion, securityAnswer))
                    {
                        MessageBox.Show("Het account wordt succesvol geüpdate!", "Succes");
                        //We do a refresh of the page, so that the old information is updated and the passwords haven't been filled in.
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
}
