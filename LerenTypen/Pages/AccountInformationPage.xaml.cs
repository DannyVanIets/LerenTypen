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
        private int userID;
        private List<DateTime> statisticsMarks;
        private List<string> labels;
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
            OverallChart();
        }

        public void OverallChart()
        {
            statisticsMarks = new List<DateTime>();
            labels = new List<string>();

            int divider = TestResultController.GetDateRange(userID);
            statisticsMarks.Add(Date.GetDateXMinutesAgo(divider));
            statisticsMarks.Add(Date.GetDateXMinutesAgo(divider / 4 * 3));
            statisticsMarks.Add(Date.GetDateXMinutesAgo(divider / 4 * 2));
            statisticsMarks.Add(Date.GetDateXMinutesAgo(divider / 4));
            statisticsMarks.Add(DateTime.Now);

            string format = "ddd";
            //Week in minutes
            if (divider > 10080)
            {
                format = "ddd-M";
            }
            //month in minutes
            if (divider > 43200)
            {
                format = "MMM";
            }
            //year in minutes
            if (divider > 525600)
            {
                format = "MMM-Y";
            }
            //3years in minutes
            if (divider > 2073600)
            {
                format = "yyyy";
            }

            Labels.Labels = new string[] { $"{statisticsMarks[0].ToString(format)} - {statisticsMarks[1].ToString(format)}", $"{statisticsMarks[1].ToString(format)} - {statisticsMarks[2].ToString(format)}", $"{statisticsMarks[2].ToString(format)} - {statisticsMarks[3].ToString(format)}", $"{statisticsMarks[3].ToString(format)} - {statisticsMarks[4].ToString(format)}" };

            LineSeries.Title = "Woorden per minuut";
            ChartValues<int> values = new ChartValues<int>();
            for (int i = 1; i < statisticsMarks.Count; i++)
            {
                values.Add(TestResultController.GetWordsPerMinuteByPeriod(userID, statisticsMarks[i - 1], statisticsMarks[i]));
            }
            LineSeries.Values = values;
            Chart.Update();
        }

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


        private void YearChart()
        {
            statisticsMarks.Clear();
            labels.Clear();
            string format = "MMM";
            for (int m = 0; m < 13; m++)
            {
                DateTime date = Date.GetDateXMonthsAgo(12 - m);
                statisticsMarks.Add(date);
                if (m != 0)
                {
                    labels.Add(date.ToString(format));
                }
            }
            UpdateChart();
        }

        private void MonthChart()
        {
            statisticsMarks.Clear();
            labels.Clear();
            System.Globalization.CultureInfo cul = System.Globalization.CultureInfo.CurrentCulture;

            for (int m = 0; m < 5; m++)
            {
                DateTime date = Date.GetDateXWeeksAgo(4 - m);
                int weekNum = cul.Calendar.GetWeekOfYear(
                    date,
                    System.Globalization.CalendarWeekRule.FirstFourDayWeek,
                    DayOfWeek.Monday);
                statisticsMarks.Add(date);
                if (m != 0)
                {
                    labels.Add($"Week {weekNum}");
                }

            }
            UpdateChart();
        }

        private void WeekChart()
        {
            statisticsMarks.Clear();
            labels.Clear();
            string format = "ddd";
            for (int m = 0; m < 8; m++)
            {
                DateTime date = Date.GetDateXDaysAgo(7 - m);
                statisticsMarks.Add(date);

                if (m != 0)
                {
                    labels.Add(date.ToString(format));
                }
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

        private void comboboxStatistics_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboboxStatistics.SelectedIndex.Equals(0))
            {
                OverallChart();
            }
            else if (comboboxStatistics.SelectedIndex.Equals(1))
            {
                YearChart();
            }
            else if (comboboxStatistics.SelectedIndex.Equals(2))
            {
                MonthChart();
            }
            else if (comboboxStatistics.SelectedIndex.Equals(3))
            {
                WeekChart();
            }
        }
    }
}