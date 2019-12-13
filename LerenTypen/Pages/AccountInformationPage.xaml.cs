using LerenTypen.Controllers;
using LerenTypen.Models;
using LiveCharts;
using LiveCharts.Wpf;
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
        List<TestTable> LastMadeContent;
        List<TestTable> CurrentContent = new List<TestTable>();
        List<TestTable> ContentNow = new List<TestTable>();
        private bool myPage;

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        public AccountInformationPage(MainWindow mainWindow, int UserID = 0)
        {
            InitializeComponent();
            //MainWindow is used to change pages.
            MainWindow = mainWindow;
            Date = new Date();
            //Make a new list for filling the table
            UserContent = new List<TestTable>();
            LastMadeContent = new List<TestTable>();

            //get User account
            if (UserID == 0 || UserID.Equals(mainWindow.Ingelogd))
            {
                Account = AccountController.GetUserAccount(mainWindow.Ingelogd);
                UserID = mainWindow.Ingelogd;
                myPage = true;
            }
            else
            {
                Account = AccountController.GetUserAccount(UserID);
                MyAccountPanel.Visibility = Visibility.Collapsed;
                AccountPanel.Visibility = Visibility.Visible;
                myPage = false;
            }
            // Fill all the labels with info
            string firstname = Account.FirstName;
            string lastname = Account.Surname;
            userNamelabel.Content = Account.UserName;
            FullNamelabel.Content = firstname + " " + lastname;
            Birthdatelabel.Content = Account.Birthdate;
            //Get averages from database and fill labels for that account.
            AverageWordsMinute.Content = AccountController.GetAverageWordsMinute(Account.UserName);
            AveragePercentageMinute.Content = AccountController.GetAverageTestResultpercentage(Account.UserName) + " %";
            try
            {
                UserContent = TestController.GetPrivateTestMyAccount(UserID);
                if (UserContent.Count.Equals(0))
                {
                    MijnToetsen.Visibility = Visibility.Collapsed;
                    AccountTests.Visibility = Visibility.Collapsed;
                    NoMyTestsLbl.Visibility = Visibility.Visible;
                    NoAccountTestsLbl.Visibility = Visibility.Visible;
                }
                else
                {
                    if (myPage)
                    {
                        MijnToetsen.ItemsSource = UserContent;
                        MijnToetsen.Items.Refresh();
                    }
                    else
                    {
                        List<TestTable> publicTests = new List<TestTable>();
                        int i = 0;
                        foreach (TestTable t in UserContent)
                        {
                            if (t.IsPrivate == false)
                            {
                                i++;
                                t.WPFNumber = i;
                                publicTests.Add(t);

                            }
                        }
                        AccountTests.ItemsSource = publicTests;
                        AccountTests.Items.Refresh();
                    }
                }
                CurrentContent = UserContent;
                LastMadeContent = TestController.GetAllMyTestsAlreadyMadeTop3(UserID);
                if (LastMadeContent.Count.Equals(0))
                {
                    LaatstGeoefendeToetsen.Visibility = Visibility.Collapsed;
                    NoRecentTestsLbl.Visibility = Visibility.Visible;
                }
                else
                {
                    LaatstGeoefendeToetsen.ItemsSource = LastMadeContent;
                    LaatstGeoefendeToetsen.Items.Refresh();
                    ContentNow = LastMadeContent;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            CreateChart();
        }

        public void getValues()
        {
            IChartValues values = new ChartValues<int>
                    {
                        TestResultController.GetWordsPerMinuteByPeriod( MainWindow.Ingelogd, Date.getDateXMonthsAgo(4),  Date.getDateXMonthsAgo(5)),
                        TestResultController.GetWordsPerMinuteByPeriod( MainWindow.Ingelogd, Date.getDateXMonthsAgo(2),  Date.getDateXMonthsAgo(3)),
                        TestResultController.GetWordsPerMinuteByPeriod( MainWindow.Ingelogd, Date.getDateXMonthsAgo(1),  Date.getDateXMonthsAgo(2)),
                        TestResultController.GetWordsPerMinuteByPeriod( MainWindow.Ingelogd, DateTime.Now,  Date.getDateXMonthsAgo(1))
                    };
            if (values.Contains(0))
            {
                TestResultController.GetWordsPerMinuteByPeriod(MainWindow.Ingelogd, Date.getDateXMonthsAgo(4), Date.getDateXMonthsAgo(5)),
                        TestResultController.GetWordsPerMinuteByPeriod(MainWindow.Ingelogd, Date.getDateXMonthsAgo(2), Date.getDateXMonthsAgo(3)),
                        TestResultController.GetWordsPerMinuteByPeriod(MainWindow.Ingelogd, Date.getDateXMonthsAgo(1), Date.getDateXMonthsAgo(2)),
                        TestResultController.GetWordsPerMinuteByPeriod(MainWindow.Ingelogd, DateTime.Now, Date.getDateXMonthsAgo(1))
            }
        }

        public void CreateChart()
        {
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Woorden per minuut",
                    Values = new ChartValues<int>
                    {
                        TestResultController.GetWordsPerMinuteByPeriod( MainWindow.Ingelogd, Date.getDateXMonthsAgo(4),  Date.getDateXMonthsAgo(5)),
                        TestResultController.GetWordsPerMinuteByPeriod( MainWindow.Ingelogd, Date.getDateXMonthsAgo(2),  Date.getDateXMonthsAgo(3)),
                        TestResultController.GetWordsPerMinuteByPeriod( MainWindow.Ingelogd, Date.getDateXMonthsAgo(1),  Date.getDateXMonthsAgo(2)),
                        TestResultController.GetWordsPerMinuteByPeriod( MainWindow.Ingelogd, DateTime.Now,  Date.getDateXMonthsAgo(1))
                    }

                }
            };

            Labels = new[] { Date.getDateXMonthsAgo(4).ToString("MMMM"), Date.getDateXMonthsAgo(3).ToString("MMMM"), Date.getDateXMonthsAgo(2).ToString("MMMM"), Date.getDateXMonthsAgo(1).ToString("MMMM"), DateTime.Now.ToString("MMMM") };

            //modifying any series values will also animate and update the chart

            DataContext = this;
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
            MijnToetsen.Items.Refresh();
        }

        private void AllMyTests_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.ChangePage(new AllMyTestsOverviewPage(MainWindow));
        }

        private void MakeNewTest_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.ChangePage(new CreateTestPage(MainWindow));

        }
    }
}