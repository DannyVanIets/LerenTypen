using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using LerenTypen.Controllers;
using LerenTypen.Models;

namespace LerenTypen
{
    /// <summary>
    /// On this page everything about an user editing their own account will be displayed.
    /// </summary>
    public partial class EditAccountPage : Page
    {
        private MainWindow MainWindow;
        private Account Account;
        private Date Date;

        public EditAccountPage(MainWindow mainWindow)
        {
            InitializeComponent();
            //MainWindow is used to change pages.
            MainWindow = mainWindow;

            //Date is used to get the current date and the date from 100 years ago.
            Date = new Date();

            //First we will check if the user is logged in, if not, they will be send back to the Homepage with a message that they're not logged in.
            //If a user is logged in, we will fill in all the information from his account into the textboxes.
            if (mainWindow.Ingelogd > 0)
            {
                Account = AccountController.GetAllAccountInformation(mainWindow.Ingelogd);

                firstNameTextBox.Text = Account.FirstName;
                lastNameTextbox.Text = Account.Surname;

                usernameTextBox.Text = Account.UserName;
                birthdateDatePicker.SelectedDate = Account.Birthdate;

                birthdateDatePicker.DisplayDateStart = Date.dateOfToday;
                birthdateDatePicker.DisplayDateEnd = Date.dateOfTodayHundredYearsAgo;

                //securityQuestionComboBox.SelectedValue = "Wat is je geboorteplaats?";
                securityQuestionComboBox.SelectedValue = Account.SecurityQuestion;
                securityAnswerTextBox.Text = Account.SecurityAnswer;
            }
            else
            {
                MessageBox.Show("U bent niet ingelogd!", "Error");
                MainWindow.ChangePage(new HomePage(mainWindow));
            }
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

            //Checks if the birthdate isn't younger than 1 year or older than 100 years.
            //This also checks straight away if the birthdate is really a date.
            if (birthdate > Date.dateOfToday || birthdate < Date.dateOfTodayHundredYearsAgo)
            {
                MessageBox.Show($"Geboortedatum moet tussen {Date.dateOfTodayHundredYearsAgo.Day}-{Date.dateOfTodayHundredYearsAgo.Month}-{Date.dateOfTodayHundredYearsAgo.Year} en {Date.dateOfToday.Day}-{Date.dateOfToday.Month}-{Date.dateOfToday.Year} zijn.", "Error");
            }
            //We will first check if the textboxes that aren't passwords are empty. If they are, show a message that that is not allowed!
            else if (string.IsNullOrWhiteSpace(firstname) || string.IsNullOrWhiteSpace(surname) || string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(securityQuestion) || string.IsNullOrWhiteSpace(securityAnswer))
            {
                MessageBox.Show("Je moet alle textvelden hebben ingevuld, behalve de wachtwoord velden!", "Error");
            }
            //Check if the username already exists and isn't the same as the old one.
            else if (Account.UserName != username && LoginController.UserExists(username))
            {
                MessageBox.Show("Deze gebruikersnaam bestaat al! Kies A.U.B. een andere.", "Error");
            }
            else
            {
                //We will check if the oldpassword, newpassword and newpasswordrepeat haven't been filled in and update everything except the passwords.
                if (string.IsNullOrWhiteSpace(oldPassword) && string.IsNullOrWhiteSpace(newPassword) && string.IsNullOrWhiteSpace(newPasswordRepeat))
                {
                    //Here we will update everything, except the password. First we will check if it went succesfully or not.
                    if (AccountController.UpdateAccount(MainWindow.Ingelogd, username, birthdate, firstname, surname, securityQuestion, securityAnswer))
                    {
                        MainWindow.UpdateLoginButton();
                        MessageBox.Show("Het account is succesvol geüpdate!", "Succes");
                        //We do a refresh of the page, so that the old information is updated
                        MainWindow.ChangePage(new EditAccountPage(MainWindow));
                    }
                    else
                    {
                        MessageBox.Show("Het account kon niet worden geüpdate, probeer het opnieuw of neem contact op met de beheerders.", "Error");
                    }
                }
                //In here we will check if the oldpassword hasn't been filled in, but the newpassword and/or newpasswordrepeat have been.
                else if (string.IsNullOrWhiteSpace(oldPassword) && (!string.IsNullOrWhiteSpace(newPassword) || !string.IsNullOrWhiteSpace(newPasswordRepeat)))
                {
                    MessageBox.Show("U moet het oude wachtwoord invoeren voordat u een nieuw wachtwoord kunt invoeren.", "Error");
                }
                //In here we will check if the oldpassword has been filled in, but the newpassword and/or newpasswordrepeat haven't been.
                else if (!string.IsNullOrWhiteSpace(oldPassword) && (string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(newPasswordRepeat)))
                {
                    MessageBox.Show("U moet een nieuw wachtwoord en herhaling van het nieuwe wachtwoord invoeren!", "Error");
                }
                //Here we will give an error if the password is longer than 25 letters.
                else if (newPassword.Length > 25)
                {
                    MessageBox.Show("Het wachtwoord mag niet langer zijn dan 25 tekens!", "Error");
                }
                //Here we will check if the oldpassword isn't the same one as in the database and give a message if that's true.
                else if (LoginController.ComputeSha256Hash(oldPassword) != AccountController.GetPasswordFromAccount(MainWindow.Ingelogd))
                {
                    MessageBox.Show("Dat is niet het goede oude wachtwoord!", "Error");
                }
                //Here we will check if the newpassword and newpasswordrepeat are both correct.
                else if (newPassword != newPasswordRepeat)
                {
                    MessageBox.Show("De nieuwe wachtwoorden komen niet overeen.", "Error");
                }
                //Here we will check if the new password isn't the same as the old one.
                else if (LoginController.ComputeSha256Hash(newPassword) == AccountController.GetPasswordFromAccount(MainWindow.Ingelogd))
                {
                    MessageBox.Show("Het nieuwe wachtwoord mag niet hetzelfde zijn als het oude wachtwoord!", "Error");
                }
                //In the else we will update everything with the passwords.
                else
                {
                    string hashedNewPassword = LoginController.ComputeSha256Hash(newPassword);

                    //First we gotta check if it has been succesfully updated. In both cases we will give out a message.
                    if (AccountController.UpdateAccountWithPassword(MainWindow.Ingelogd, username, hashedNewPassword, birthdate, firstname, surname, securityQuestion, securityAnswer))
                    {
                        MainWindow.UpdateLoginButton();
                        MessageBox.Show("Het account is succesvol geüpdate!", "Succes");
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

        //This is used to change the colors of the button. Same as DeleteAccountButton_MouseLeave.
        private void DeleteAccountButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            deleteAccountButton.Foreground = Brushes.Black;
        }

        private void DeleteAccountButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            deleteAccountButton.Foreground = Brushes.White;
        }

        private void DeleteAccountButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Weet je zeker dat je je account wilt verwijderen?", "Account archiveren", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {               
                    AccountController.DeleteAccount(Account.UserName);
                    MessageBox.Show("Je account is succesvol gearchiveerd!", "Succes");
                    MainWindow.LogoutUser(true);
                }
            }
            catch (Exception r)
            {
                Console.WriteLine(r.Message);
                MessageBox.Show("Het archiveren is niet succesvol uitgevoerd. Probeer het opnieuw of neem contact op met een administrator.", "Error");
            }
        }
    }
}
