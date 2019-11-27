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
                Gebruikernaam.Text = Account.UserName;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                MessageBox.Show("U bent niet ingelogd!", "Error");

            }
        }

        //When the make admin button is clicked
        private void Update_Button_Click(object sender, RoutedEventArgs e)
        {
            string firstname = firstNameTextBox.Text;
            string surname = lastNameTextbox.Text;
            string username = Account.UserName;
            string v = ((ComboBoxItem)GebruikersRol.SelectedItem).Tag.ToString();

            try
            {

                if (!string.IsNullOrEmpty(firstname) || !string.IsNullOrEmpty(surname) || !string.IsNullOrEmpty(username))
                {
                    Database.AdminUpdateAccount(username, firstname, surname);
                }
                else
                {
                    MessageBox.Show("Vul alle velden in!", "Vul alles in");
                }

                if (v == "student")
                {
                    Database.MaakStudent(username);
                    MessageBox.Show("De aangepaste info is Geupdate!", "Info Geupdate");
                }
                else if (v == "docent")
                {
                    Database.MaakDocent(username);
                    MessageBox.Show("De aangepaste info is Geupdate!", "Info Geupdate");
                }
                else if (v == "admin")
                {
                    Database.MaakAdmin(username);
                    MessageBox.Show("De aangepaste info is Geupdate!", "Info Geupdate");
                }
                else
                {
                    MessageBox.Show("Geen geldige rol", "Error");
                }
            }

            catch (Exception q)
            {
                Console.WriteLine(q);
                MessageBox.Show("Er is iets mis gegaan... Hallo product demo..", "Error");
            }

        }
    }
}