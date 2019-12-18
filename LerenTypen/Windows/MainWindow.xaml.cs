﻿using LerenTypen.Controllers;
using LerenTypen.Models;
using Renci.SshNet;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Collections.Generic;

namespace LerenTypen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // The account ID if the user is logged in, otherwise 0
        public int Ingelogd { get; set; }

        private SshClient client;

        //This class is used to remember the selected options across all tests.
        public TestOptions testOptions = new TestOptions();

        public MainWindow()
        {
            InitializeComponent();

            client = new SshClient("145.44.233.184", "student", "toor2019");
        connectSSH:
            try
            {
                client.Connect();
                var port = new ForwardedPortLocal("127.0.0.1", 1433, "localhost", 1433);
                client.AddForwardedPort(port);
                port.Start();
            }
            catch (Exception)
            {
                var result = MessageBox.Show("Error bij het maken van verbinding met de server. Controleer uw internetverbinding. Wilt u opnieuw proberen te verbinden?", "LerenTypen", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    goto connectSSH;
                }
                else
                {
                    Environment.Exit(1);
                }
            }

            frame.Navigate(new HomePage(this));
        }

        private void HomePageButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(new HomePage(this), homePageButton);
        }

        private void TestOverviewPageButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(new TestOverviewPage(this), testOverviewPageButton);
        }

        private void TipPageButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(new TipPage(this), tipPageButton);
        }

        private void LeaderboardPageButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(new LeaderboardPage(this), leaderboardPageButton);
        }

        //In this method we will first check if the user is logged in, if that's the case it means they want to logout. We will first ask a confirmation, then update the property, make sure the loginPageButton texts changes and then put them on the homepage. If the user is not logged in, it will direct them to the login page.
        private void LoginPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (Ingelogd > 0)
            {
                loginPageButton.ContextMenu.IsOpen = true;
                loginPageButton.IsChecked = false;
            }
            else
            {
                ChangePage(new LoginPage(this), loginPageButton);
            }
        }
        private void AllUsersPageButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(new AllUsersPage(this), allUsersPageButton);
        }

        //Checks if the user is logged in and sends them to the EditAccountPage if so.
        private void EditAccountPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (Ingelogd > 0)
            {
                ChangePage(new EditAccountPage(this));
            }
        }

        private void AccountInformationPage_Click(object sender, RoutedEventArgs e)
        {
            if (Ingelogd > 0)
            {
                ChangePage(new AccountInformationPage(this));
            }
        }

        /// <summary>
        /// Changes the page to the specified page if this page is not 
        /// already open and updates the menu buttons accordingly
        /// </summary>
        /// <param name="pageToChangeTo"></param>
        /// <param name="pageToggleButton"></param>
        public void ChangePage(Page pageToChangeTo, ToggleButton pageToggleButton = null)
        {
            // Check if the current page is not the same as the page to change to
            if (frame.Content.GetType() != pageToChangeTo.GetType())
            {
                ChangePageHelper(pageToChangeTo, pageToggleButton);
            }
            else if (pageToggleButton != null)
            {
                pageToggleButton.IsChecked = true;
            }
        }

        /// <summary>
        /// Private helper function for ChangePage that changes the page
        /// </summary>
        private void ChangePageHelper(Page pageToChangeTo, ToggleButton pageToggleButton)
        {
            if (pageToggleButton == null)
            {
                if (pageToChangeTo is HomePage)
                {
                    pageToggleButton = homePageButton;
                }
                if (pageToChangeTo is TestOverviewPage)
                {
                    pageToggleButton = testOverviewPageButton;
                }
                else if (pageToChangeTo is TipPage)
                {
                    pageToggleButton = tipPageButton;
                }
                else if (pageToChangeTo is AllUsersPage)
                {
                    pageToggleButton = allUsersPageButton;
                }
                else if (pageToChangeTo is LeaderboardPage)
                {
                    pageToggleButton = leaderboardPageButton;
                }
                else if (pageToChangeTo is LoginPage)
                {
                    pageToggleButton = loginPageButton;
                }
            }

            bool shouldChangePage = true;
            if (frame.Content is TestExercisePage)
            {
                MessageBoxResult? choice = ShowTestExcersiseQuitWarning();

                if (choice == MessageBoxResult.Cancel)
                {
                    shouldChangePage = false;
                }
            }

            if (frame.Content is TestExercisePage || frame.Content is TestResultsPage)
            {
                CheckForUnfinishedTests();
            }

            if (shouldChangePage)
            {
                frame.Content = pageToChangeTo;
                SwitchMenuButtons(pageToggleButton);         
            }
            else
            {
                pageToggleButton.IsChecked = false;
            }

        }

        /// <summary>
        /// Checks the specified button and unchecks all other buttons of the menu
        /// </summary>
        /// <param name="buttonToSwitchTo">The ToggleButton to check</param>
        private void SwitchMenuButtons(ToggleButton buttonToSwitchTo)
        {
            homePageButton.IsChecked = false;
            testOverviewPageButton.IsChecked = false;
            tipPageButton.IsChecked = false;
            leaderboardPageButton.IsChecked = false;
            allUsersPageButton.IsChecked = false;
            loginPageButton.IsChecked = false;

            if (buttonToSwitchTo != null)
            {
                buttonToSwitchTo.IsChecked = true;
            }
        }

        /// <summary>
        /// This method is used to change the the loginPageButton (change its text and add a submenu), used if you login and logout.
        /// </summary>
        public void UpdateLoginButton()
        {
            if (Ingelogd > 0)
            {
                loginPageButton.Content = $"Welkom {AccountController.GetUsername(Ingelogd)} ▼";
                loginPageButton.ContextMenu = (ContextMenu)FindResource("accountMenu");

                if (AccountController.IsAdmin(Ingelogd))
                {
                    allUsersPageButton.Visibility = Visibility.Visible;
                }

                CheckForUnfinishedTests();
            }
            else
            {
                loginPageButton.Content = "Inloggen/registeren";
                loginPageButton.ContextMenu = null;
                allUsersPageButton.Visibility = Visibility.Collapsed;

                resumeTestsButton.Visibility = Visibility.Collapsed;
                resumeTestsButton.ContextMenu = null;
            }
        }

        /// <summary>
        /// Used to show a button in the menu to resume unfinished tests if the logged in user has any
        /// </summary>
        public void CheckForUnfinishedTests()
        {
            List<int> unfinishedTestIDs = TestController.GetUnfinishedTestIDsFromAccount(Ingelogd);
            if (unfinishedTestIDs.Count > 0)
            {
                resumeTestsButton.Visibility = Visibility.Visible;

                if (resumeTestsButton.ContextMenu == null)
                {
                    resumeTestsButton.ContextMenu = new ContextMenu();
                }
                else
                {
                    resumeTestsButton.ContextMenu.Items.Clear();
                }

                foreach (int id in unfinishedTestIDs)
                {
                    MenuItem item = new MenuItem();
                    item.Header = TestController.GetTestName(id);
                    item.Click += ((s, e) =>
                    {
                        ChangePage(new TestExercisePage(id, this, true));
                    });
                    resumeTestsButton.ContextMenu.Items.Add(item);
                }
            }
            else
            {
                resumeTestsButton.Visibility = Visibility.Collapsed;
                resumeTestsButton.ContextMenu = null;
            }
        }

        public void LogoutUser(bool silent = false)
        {
            Ingelogd = 0;
            UpdateLoginButton();

            if (!silent)
            {
                MessageBox.Show("U bent succesvol uitgelogd! U wordt nu doorgestuurd naar de homepagina.", "Succes");
            }

            ChangePage(new HomePage(this));
        }

        private void LogoutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Weet je zeker dat je wilt uitloggen?", "Uitloggen", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                LogoutUser();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            client.Disconnect();
            client.Dispose();
        }

        private void ResumeTestsButton_Click(object sender, RoutedEventArgs e)
        {
            resumeTestsButton.ContextMenu.IsOpen = true;
            resumeTestsButton.IsChecked = false;
        }

        /// <summary>
        /// Shows a warning about quitting the test when the user is on the excercise page
        /// </summary>
        public MessageBoxResult? ShowTestExcersiseQuitWarning()
        {
            if (frame.Content is TestExercisePage)
            {
                TestExercisePage page = (TestExercisePage)frame.Content;
                MessageBoxResult choice = page.AskStopTest();
                return choice;
            }

            return null;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult? choice = ShowTestExcersiseQuitWarning();

            if (choice != null && choice == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
        }
    }
}