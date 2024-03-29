﻿using LerenTypen.Controllers;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

namespace LerenTypen
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class TestResultsPage : Page
    {
        private int testID;
        private MainWindow m;
        private List<string> wrongAnswers;
        private List<string> hadToBe;
        private List<string> rightAnswers;
        private int testResultID;
        private string username;

        public TestResultsPage(int testID, MainWindow m, int testResultID)
        {
            InitializeComponent();
            this.testID = testID;
            this.testResultID = testResultID;
            this.m = m;

            wrongAnswers = new List<string>();
            hadToBe = new List<string>();
            rightAnswers = new List<string>();

            testNameLbl.Content = TestController.GetTestName(testID);
            GetResults();
            FillAnswerList(false);
            amountOfWrongTbl.Text = wrongAnswers.Count.ToString();
            List<int> testInformation = TestController.GetTestInformation(testID);
            username = AccountController.GetUsername(testInformation[0]);
            createrRun.Text = username;
            amountOfWordsLbl.Content = $"Aantal woorden: {TestController.GetAmountOfWordsFromTest(testID)}";

            string difficulty;
            if (testInformation[1].Equals(0))
            {
                difficulty = "Makkelijk";
            }
            else if (testInformation[1].Equals(1))
            {
                difficulty = "Midden";
            }
            else
            {
                difficulty = "Moeilijk";
            }
            difficultyLbl.Content = difficulty;
        }

        /// <summary>
        /// Fills the answer list with answers, bool check is to check if only wrong answers have to be shown
        /// </summary>
        /// <param name="check"></param>
        private void FillAnswerList(bool check)
        {
            AnswersLv.Items.Clear();
            int i = 0;
            foreach (string answer in wrongAnswers)
            {
                ListViewItem li = new ListViewItem();
                li.Foreground = Brushes.Red;

                if (!answer.Trim().Equals(""))
                {
                    li.Content = $"{answer} \nJuiste antwoord: {hadToBe[i]}";
                    AnswersLv.Items.Add(li);
                }
                else
                {
                    li.Content = $"Geen invoer \nJuiste antwoord: {hadToBe[i]}";
                    AnswersLv.Items.Add(li);
                }
                i++;
            }
            if (!check)
            {
                foreach (string answer in rightAnswers)
                {
                    ListViewItem li = new ListViewItem();
                    li.Foreground = Brushes.Green;
                    li.Content = $"{answer}";
                    AnswersLv.Items.Add(li);
                }
            }
        }

        private void RestartButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            m.frame.Navigate(new TestExercisePage(testID, m));
        }

        private void OnlyWrongCb_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            FillAnswerList(true);
        }

        private void OnlyWrongCb_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            FillAnswerList(false);
        }

        /// <summary>
        /// Gets the results from the database and adds an awardsticker if percentage right is 100
        /// </summary>
        private void GetResults()
        {
            amountOfWrongTbl.Text = wrongAnswers.Count.ToString();

            List<string> testResults = TestResultController.GetTestResults(testResultID);
            rightAnswers = TestResultController.GetTestResultsContentRight(testResultID);
            wrongAnswers = TestResultController.GetTestResultsContentWrong(testResultID);
            hadToBe = TestResultController.GetTestResultsContentHadToBe(testResultID);

            int amountOfPauses = Convert.ToInt32(testResults[1]);
            int wordsPerMinute = int.Parse(testResults[0]);
            amountOfBreaksTbl.Text = amountOfPauses.ToString();
            wordsPerMinuteTbl.Text = wordsPerMinute.ToString();
            int percentageRight = int.Parse(testResults[2]);
            string percentageRightStr = percentageRight.ToString() + "%";
            percentageRightTbl.Text = percentageRightStr;
            if (percentageRight > 55)
            {
                percentageRightTbl.Foreground = Brushes.Green;
            }
            else
            {
                percentageRightTbl.Foreground = Brushes.Red;
            }
            if (percentageRight.Equals(100))
            {
                awardStack.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void UsernameClick(object sender, System.Windows.RoutedEventArgs e)
        {
            m.ChangePage(new AccountInformationPage(m, AccountController.GetAccountIDFromUsername(username)));
        }
    }
}
