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

        //When the Info Aanpassen button is clicked
        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string firstname = firstNameTextBox.Text;
                string surname = lastNameTextbox.Text;
                string username = usernameTextBox.Text;
                DateTime birthdate = birthdateDatePicker.DisplayDate;

                if (!string.IsNullOrEmpty(firstname) || !string.IsNullOrEmpty(surname) || !string.IsNullOrEmpty(username))
                {
                    Database.AdminUpdateAccount(username, birthdate, firstname, surname);
                    MessageBox.Show("Account Informatie Upgedate!", "Account Geupdate");
                }
                else
                {
                    MessageBox.Show("Vul alle velden in!", "Vul alles in");
                }

            }
            catch
            {
                MessageBox.Show("Er is iets mis gegaan... Hallo product demo..", "Error");
            }
        }

        //When the make admin button is clicked


        private void Admin_Button_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTextBox.Text;
            try
            {
                Database.MaakAdmin(username);
                MessageBox.Show("Persoon is nu admin!", "Admin gemaakt");
            }
            catch(Exception q)
            {
                Console.WriteLine(q);
                MessageBox.Show("Er is iets mis gegaan... Hallo product demo..", "Error");
            }

        }
    }
}

