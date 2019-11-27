using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;


namespace LerenTypen
{

    public partial class AdminUserPanel : Window
    {
        private Account account;
        public AdminUserPanel(int id)
        {
            InitializeComponent();
            try
            {
                account = Database.GetUserAccount(id);
                firstNameTextBox.Text = account.FirstName;
                lastNameTextbox.Text = account.Surname;
                Gebruikernaam.Text = account.UserName;
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
            string username = account.UserName;
            string comboboxvalue = ((ComboBoxItem)GebruikersRol.SelectedItem).Tag.ToString();
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

                if (comboboxvalue == "student")
                {
                    Database.MakeStudent(username);
                    MessageBox.Show("De aangepaste info is Geupdate!", "Info Geupdate");
                    this.Close();
                }
                else if (comboboxvalue == "docent")
                {
                    Database.MakeTeacher(username);
                    MessageBox.Show("De aangepaste info is Geupdate!", "Info Geupdate");
                    this.Close();

                }
                else if (comboboxvalue == "admin")
                {
                    Database.MakeAdmin(username);
                    MessageBox.Show("De aangepaste info is Geupdate!", "Info Geupdate");
                    this.Close();

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