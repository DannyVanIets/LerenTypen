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
        private int accountID = 1;
        private int testResultID;

        public TestResultsPage(int testID, MainWindow m, int testResultID)
        {
            InitializeComponent();
            this.testID = testID;
            this.testResultID = testResultID;
            this.m = m;

            wrongAnswers = new List<string>();
            hadToBe = new List<string>();
            rightAnswers = new List<string>();

            testNameLbl.Content = Database.GetTestName(testID);

            GetResults();

            FillAnswerList(false);
            amountOfWrongTbl.Text = wrongAnswers.Count.ToString();

            List<int> testInformation = Database.GetTestInformation(testID);
            createrRun.Text = Database.GetUserName(testInformation[0]);

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
                    li.Content = $"{answer} \nGoed gedaan!";
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

        private void GetResults()
        {

            amountOfWrongTbl.Text = wrongAnswers.Count.ToString();

            List<string> testResults = Database.GetTestResults(accountID, testID);

            rightAnswers = Database.GetTestResultsContentRight(testResultID);
            wrongAnswers = Database.GetTestResultsContentWrong(testResultID);
            hadToBe = Database.GetTestResultsContentHadToBe(testResultID);

            int amountOfPauses = int.Parse(testResults[1]);
            int wordsPerMinute = int.Parse(testResults[0]);
            amountOfBreaksTbl.Text = amountOfPauses.ToString();
            wordsPerMinuteTbl.Text = wordsPerMinute.ToString();
            decimal percentageRight = CalculatePercentageRight();
            string percentageRightStr = Math.Round(percentageRight).ToString() + "%";
            percentageRightTbl.Text = percentageRightStr;

            if (percentageRight.Equals(100))
            {
                awardStack.Visibility = System.Windows.Visibility.Visible;
            }
        }
        private decimal CalculatePercentageRight()
        {
            decimal percentageRight;
            try
            {
                percentageRight = decimal.Divide(rightAnswers.Count, rightAnswers.Count + wrongAnswers.Count) * 100;
            }
            catch (DivideByZeroException)
            {
                percentageRight = 100;
            }
            return percentageRight;
        }
    }
}
