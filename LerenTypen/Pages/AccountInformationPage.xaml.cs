using LerenTypen.Controllers;
using LerenTypen.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace LerenTypen
{
    public partial class AccountInformationPage : Page
    {
        private MainWindow MainWindow;
        private Account Account;
        private Date Date;

        public AccountInformationPage(MainWindow mainWindow)
        {
            InitializeComponent();
            //MainWindow is used to change pages.
            MainWindow = mainWindow;
            Date = new Date();

            if (mainWindow.Ingelogd > 0)
            {
                Account = AccountController.GetUserAccount(mainWindow.Ingelogd);


                string voornaam = Account.FirstName;
                string achternaam = Account.Surname;
                userNamelabel.Content = Account.UserName;
                FullNamelabel.Content = voornaam + " " + achternaam;
                Birthdatelabel.Content = Account.Birthdate;
            }
            else
            {
                MessageBox.Show("U bent niet ingelogd!", "Error");
                MainWindow.ChangePage(new HomePage(mainWindow));
            }
        }
    }
}