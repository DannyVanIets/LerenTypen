using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;


namespace LerenTypen
{

    public partial class AdminUserPanel : Window
    {
        private Account Account;


        public AdminUserPanel(int id)
        {
            InitializeComponent();
            try
            {

                Account = Database.GetUserAccount(id);

                firstNameTextBox.Text = Account.FirstName;
                lastNameTextbox.Text = Account.Surname;
                usernameTextBox.Text = Account.UserName;
                birthdateDatePicker.SelectedDate = Account.Birthdate;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                MessageBox.Show("U bent niet ingelogd!", "Error");

            }
        }


        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            string firstname = firstNameTextBox.Text;
            string surname = lastNameTextbox.Text;
            string username = usernameTextBox.Text;

            DateTime birthdate = birthdateDatePicker.DisplayDate;


            if (string.IsNullOrEmpty(firstname) || string.IsNullOrEmpty(surname) || string.IsNullOrEmpty(username))
            {

            }

            else
            {
                //Hier wordt alles geüpdate!

                if (Database.UpdateAccountWithPassword(, username, hashedNewPassword, birthdate, firstname, surname))
                {
                    MessageBox.Show("Het account wordt succesvol geüpdate!", "Succes");
                }
                else
                {
                    MessageBox.Show("Het account kon niet worden geüpdate, probeer het opnieuw of neem contact op met de beheerders.", "Error");
                }
            }
        }
    }
}

private void Admin_Button_Click(object sender, RoutedEventArgs e)
{

}
    }
}

