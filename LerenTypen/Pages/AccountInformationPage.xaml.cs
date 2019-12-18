using LerenTypen.Controllers;
using LerenTypen.Models;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace LerenTypen
{
    public partial class AccountInformationPage : Page
    {
        private MainWindow MainWindow;
        private Account Account;
        private Date Date;
        private List<TestTable> UserContent;
        private List<TestTable> LastMadeContent;
        private List<TestTable> CurrentContent = new List<TestTable>();
        private List<TestTable> ContentNow = new List<TestTable>();
        private bool myPage;
        private int userID;
        //Make lists for statistics (statisticsMarks is for statistic date points)
        private List<DateTime> statisticsMarks = new List<DateTime>();
        private CultureInfo dutch = new CultureInfo("nl-NL", false);
        private List<string> labels = new List<string>();
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
            userID = UserID;
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
                    MyTests.Visibility = Visibility.Collapsed;
                    AccountTests.Visibility = Visibility.Collapsed;
                    NoMyTestsLbl.Visibility = Visibility.Visible;
                    NoAccountTestsLbl.Visibility = Visibility.Visible;
                }
                else
                {
                    if (myPage)
                    {
                        MyTests.ItemsSource = UserContent;
                        MyTests.Items.Refresh();
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
                    LastMadeTests.Visibility = Visibility.Collapsed;
                    NoRecentTestsLbl.Visibility = Visibility.Visible;
                }
                else
                {
                    LastMadeTests.ItemsSource = LastMadeContent;
                    LastMadeTests.Items.Refresh();
                    ContentNow = LastMadeContent;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            CreateChart();
        }

        public void CreateChart()
        {
            LineSeries.Title = "Woorden per minuut";

            // Checks date range for using the right filtering when page is loaded
            int ResultSpanInHours = TestResultController.GetDateRange(userID);
            if (ResultSpanInHours == 0)
            {
                StatisticsGrid.Visibility = Visibility.Collapsed;
                LblNoResults.Visibility = Visibility.Visible;
            }
            if (ResultSpanInHours < 168)
            {
                ComboboxStatistics.SelectedIndex = 3;
            }
            //Week in hours
            else if (ResultSpanInHours > 168 && ResultSpanInHours < 2000)
            {
                ComboboxStatistics.SelectedIndex = 2;
            }
            //3months in hours
            else if (ResultSpanInHours > 2000 && ResultSpanInHours < 16000)
            {
                ComboboxStatistics.SelectedIndex = 1;
            }
            //2years in hours
            else if (ResultSpanInHours > 16000)
            {
                ComboboxStatistics.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Updates the chart using the lists contents
        /// </summary>
        private void UpdateChart()
        {
            Labels.Labels = labels.ToArray();
            ChartValues<int> values = new ChartValues<int>();
            for (int i = 1; i < statisticsMarks.Count; i++)
            {
                values.Add(TestResultController.GetWordsPerMinuteByPeriod(userID, statisticsMarks[i - 1], statisticsMarks[i]));
            }
            LineSeries.Values = values;
            Chart.Update();
        }

        /// <summary>
        /// Fills the lists with the current information (5 years)
        /// </summary>
        private void FiveYearChart()
        {
            string format = "yyyy";
            for (int m = 0; m < 6; m++)
            {
                DateTime date = DateController.GetDateXYearsAgo(4 - m);
                statisticsMarks.Add(date);
                labels.Add(date.ToString(format));
            }
            UpdateChart();
        }

        /// <summary>
        /// Fills the lists with the current information (12 months)
        /// </summary>
        private void YearChart()
        {
            string format = "MMMMMMMMM";
            for (int m = 0; m < 13; m++)
            {
                DateTime date = DateController.GetDateXMonthsAgo(11 - m);
                statisticsMarks.Add(date);
                labels.Add(date.ToString(format, dutch));
            }
            UpdateChart();
        }

        /// <summary>
        /// Fills the lists with the current information (4 weeks)
        /// </summary>
        private void MonthChart()
        {
            for (int m = 0; m < 5; m++)
            {
                DateTime date = DateController.GetDateXWeeksAgo(3 - m);
                int weekNum = dutch.Calendar.GetWeekOfYear(
                    date,
                    CalendarWeekRule.FirstFourDayWeek,
                    DayOfWeek.Monday);
                statisticsMarks.Add(date);
                labels.Add($"Week {weekNum}");
            }
            UpdateChart();
        }

        /// <summary>
        /// Fills the lists with the current information (seven days)
        /// </summary>
        private void WeekChart()
        {
            string format = "ddddddddd";
            for (int m = 0; m < 8; m++)
            {
                DateTime date = DateController.GetDateXDaysAgo(6 - m);
                statisticsMarks.Add(date);
                labels.Add(date.ToString(format, dutch));
            }
            UpdateChart();
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
                MyTests.Items.Refresh();
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
            MyTests.Items.Refresh();
        }

        private void AllMyTests_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.ChangePage(new AllMyTestsOverviewPage(MainWindow));
        }

        private void MakeNewTest_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.ChangePage(new CreateTestPage(MainWindow));

        }

        private void ComboboxStatistics_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            statisticsMarks.Clear();
            labels.Clear();

            if (ComboboxStatistics.SelectedIndex.Equals(0))
            {
                FiveYearChart();
            }
            else if (ComboboxStatistics.SelectedIndex.Equals(1))
            {
                YearChart();
            }
            else if (ComboboxStatistics.SelectedIndex.Equals(2))
            {
                MonthChart();
            }
            else if (ComboboxStatistics.SelectedIndex.Equals(3))
            {
                WeekChart();
            }
        }

        /// <summary>
        /// Go to Testinfo page when clicking test
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Hyperlink_Click(object sender, EventArgs e)
        {
            Hyperlink link = (Hyperlink)sender;
            int id = Convert.ToInt32(link.Tag);
            MainWindow.ChangePage(new TestInfoPage(id, MainWindow));
        }
    }
}