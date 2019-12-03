using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using LerenTypen.Controllers;
using LerenTypen.Models;

namespace LerenTypen
{
    public partial class AdminEditAccountWindow : Window
    {
        private Account account;
        public AdminEditAccountWindow(int id, int acctype)
        {
            InitializeComponent();
            try
            {
                account = AccountController.GetUserAccount(id);
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
                if (!string.IsNullOrEmpty(firstname) || !string.IsNullOrEmpty(surname) || !string.IsNullOrEmpty(username))
                {
                    AccountController.UpdateAccount(username, firstname, surname);
                }
                else
                {
                    MessageBox.Show("Vul alle velden in!", "Vul alles in");
                }

                if (comboboxvalue == "student")
                {
                    AccountController.MakeStudent(username);
                    MessageBox.Show("De aangepaste info is Geupdate!", "Info Geupdate");
                    this.Close();
                }
                else if (comboboxvalue == "docent")
                {
                    AccountController.MakeTeacher(username);
                    MessageBox.Show("De aangepaste info is Geupdate!", "Info Geupdate");
                    this.Close();


                }
                else if (comboboxvalue == "admin")
                {
                    AccountController.MakeAdmin(username);
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

     /*   private void DeleteAccountButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //DeleteAcc.Foreground = Brushes.Black;
        }

        private void DeleteAccountButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //DeleteAcc.Foreground = Brushes.White;
        }
*/
      /*  private void DeleteAcc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string username = account.UserName;
                AccountController.DeleteAccount(username);
                MessageBox.Show("Het account is verwijderd", "Account verwijderd!");
                this.Close();
            }
            catch(Exception r)
            {
                Console.WriteLine(r.ToString());
                MessageBox.Show("Error", "Error");
                this.Close();
            }
        }*/
    }
}