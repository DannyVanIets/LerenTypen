﻿using LerenTypen.Controllers;
using LerenTypen.Models;
using System;
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
        List<TestTable> UserContent;
        List<TestTable> CurrentContent = new List<TestTable>();


        public AccountInformationPage(MainWindow mainWindow)
        {
            InitializeComponent();
            //MainWindow is used to change pages.
            MainWindow = mainWindow;
            Date = new Date();
            //Make a new list for filling the table
            UserContent = new List<TestTable>();
            if (mainWindow.Ingelogd > 0)
            {
                //get User account
                Account = AccountController.GetUserAccount(mainWindow.Ingelogd);
                // Fill all the labels with info
                string voornaam = Account.FirstName;
                string achternaam = Account.Surname;
                userNamelabel.Content = Account.UserName;
                FullNamelabel.Content = voornaam + " " + achternaam;
                Birthdatelabel.Content = Account.Birthdate;
                //Get averages from database and fill labels for that account.
                AverageWordsMinute.Content = AccountController.GetAverageWordsMinute(Account.UserName);
                AveragePercentageMinute.Content = AccountController.GetAverageTestResultpercentage(Account.UserName);

                try
                {
                    UserContent = TestController.GetPrivateTestMyAccount(MainWindow.Ingelogd);
                    MijnToetsen.ItemsSource = UserContent;
                    MijnToetsen.Items.Refresh();
                    CurrentContent = UserContent;

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            else
            {
                MessageBox.Show("U bent niet ingelogd!", "Error");
                MainWindow.ChangePage(new HomePage(mainWindow));
            }
        }


        private void DG_Checkbox_Check(object sender, RoutedEventArgs e)
        {
            try
            {
                CheckBox checkbox = sender as CheckBox;
                TestTable tt = checkbox.DataContext as TestTable;
                int id = tt.TestId;
                TestController.UpdateTestToPrivate(id);
                //tt.IsPrivate = checkbox.IsChecked;

                MessageBox.Show("IK update nu iets."); 
                MijnToetsen.Items.Refresh();
            }
            catch (Exception y)
            {
                Console.WriteLine(y.ToString());
            }
        }

        private void DG_Checkbox_Uncheck(object sender, RoutedEventArgs e)
        {
            CheckBox checkbox = sender as CheckBox;
            TestTable tt = checkbox.DataContext as TestTable;
            int id = tt.TestId;

            TestController.UpdateTestToPublic(id);
            MessageBox.Show("Hij moet nu naar 0");
            MijnToetsen.Items.Refresh();
        }
    }
}