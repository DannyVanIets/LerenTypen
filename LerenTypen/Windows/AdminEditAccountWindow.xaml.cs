using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using LerenTypen.Controllers;
using LerenTypen.Models;

namespace LerenTypen
{
    public partial class AdminEditAccountWindow : Window
    {
        private Account account;
        private int accountID;
        private MainWindow mainWindow;

        public bool Logout { get; private set; } = false;

        public AdminEditAccountWindow(int id, int acctype, MainWindow mainWindow)
        {
            InitializeComponent();
            accountID = id;
            this.mainWindow = mainWindow;

            try
            {
                account = AccountController.GetAccountNamesAndBirthdate(id);
                firstNameTextBox.Text = account.FirstName;
                lastNameTextbox.Text = account.Surname;
                EditPageUserName.Content += " " + account.UserName;
                UserType.SelectedIndex = acctype;
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
            string comboboxvalue = ((ComboBoxItem)UserType.SelectedItem).Tag.ToString();

            try
            {
                //get amount of admin accounts on application
                int amount = AccountController.GetAmountOfAdmins(2);

                // Check if own account is being edited
                if (accountID == mainWindow.Ingelogd)
                {
                    MessageBoxResult result = MessageBox.Show("Je staat op het punt om je eigen account aan te passen. Als je doorgaat zal je opnieuw moeten inloggen. Wil je doorgaan?", "Eigen account aanpassen", MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.Yes)
                    {
                        Logout = true;
                    }
                    else
                    {
                        return;
                    }
                }

                if (!string.IsNullOrEmpty(firstname) || !string.IsNullOrEmpty(surname) || !string.IsNullOrEmpty(username))
                {
                    AccountController.UpdateAccount(username, firstname, surname);
                }

                if (comboboxvalue == "student")
                {
                    if (amount == 1)
                    {
                        MessageBoxResult messageBoxResult = MessageBox.Show("Weet je zeker dat je het laastste admin account wilt archiveren?", "Alle admins archiveren", MessageBoxButton.YesNo);
                        if (messageBoxResult == MessageBoxResult.Yes)
                        {
                            MessageBox.Show("Dit is niet mogelijk! Maak eerst een ander account admin", "Error");
                            this.Close();
                            Logout = false;
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        AccountController.MakeStudent(username);
                        MessageBox.Show("De aangepaste info is Geupdate!", "Info Geupdate");
                        this.Close();
                    }
                }

                else if (comboboxvalue == "docent")
                {
                    if (amount == 1)
                    {
                        MessageBoxResult messageBoxResult = MessageBox.Show("Weet je zeker dat je het laastste admin account wilt archiveren?", "Alle admins archiveren", MessageBoxButton.YesNo);
                        if (messageBoxResult == MessageBoxResult.Yes)
                        {
                            MessageBox.Show("Dit is niet mogelijk! Maak eerst een ander account admin", "Error");
                            this.Close();
                            Logout = false;
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        AccountController.MakeTeacher(username);
                        MessageBox.Show("De aangepaste info is Geupdate!", "Info Geupdate");
                        this.Close();
                    }
                }

                else if (comboboxvalue == "admin")
                {
                    MessageBox.Show("Dit is niet mogelijk! Maak eerst een ander account admin", "Error");
                    this.Close();
                    Logout = false;
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

        private void DeleteAccountButton_MouseEnter(object sender, MouseEventArgs e)
        {
            DeleteAcc.Foreground = Brushes.Black;
        }

        private void DeleteAccountButton_MouseLeave(object sender, MouseEventArgs e)
        {
            DeleteAcc.Foreground = Brushes.White;
        }
        private void DeleteAcc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Weet je zeker dat je het account wilt archiveren?", "Account archiveren", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    MessageBox.Show("Het account is succesvol gearchiveerd!", "Succes");
                    string username = account.UserName;
                    AccountController.DeleteAccount(username);
                    this.Close();
                }
            }
            catch (Exception r)
            {
                Console.WriteLine(r.Message);
                MessageBox.Show("Het account kon niet gearchiveerd worden, probeer het later opnieuw of neem contact op met de beheerders.", "Error");
            }
        }
    }
}
