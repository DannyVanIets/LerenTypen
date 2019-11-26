using System;
using System.Collections.Generic;
using System.Windows.Controls;

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
        public TestResultsPage(int testID, MainWindow m)
        {
            InitializeComponent();
            this.testID = testID;
            this.m = m;

            wrongAnswers = new List<string>();
            hadToBe = new List<string>();
            rightAnswers = new List<string>();

            
            FillAnswerList(false);
            GetResults();
            List<int> testInformation = Database.GetTestInformation(testID);
            string difficulty;
            if (testInformation[1].Equals(0))
            {
                difficulty = "Makkelijk";
            }else if (testInformation[1].Equals(1))
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
                AnswersLv.Items.Add($"{answer} \nJuiste antwoord: {hadToBe[i]}");
                i++;
            }
            if (!check)
            {
                foreach (string answer in rightAnswers)
                {
                    AnswersLv.Items.Add($"{answer} \nGoed gedaan!");
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
            string percentageRight = CalculatePercentageRight();
            amountOfWrongTbl.Text = wrongAnswers.Count.ToString();
            
            Tuple<List<string>,int> testResults = Database.GetTestResults(accountID, testID);
            int testResultID = testResults.Item2;
            List<string> testResultsContent = Database.GetTestResultsContent(testResultID);
            int i = 0;
            foreach (string result in testResultsContent)
            {
                if (i % 1 == 0)
                {
                    if (result.Equals(null))
                    {
                        rightAnswers.Add(testResultsContent[i - 1]);
                    }
                    else
                    {
                        wrongAnswers.Add(testResultsContent[i - 1]);
                        hadToBe.Add(result);
                    }
                }
                i++;
            }
            List<string> testResultsItem = testResults.Item1;
            Console.WriteLine(testResultsItem.Count);

            int amountOfPauses = int.Parse(testResultsItem[0]);
            int wordsPerMinute = int.Parse(testResults.Item1[1]);
            amountOfBreaksTbl.Text = amountOfPauses.ToString();
            wordsPerMinuteTbl.Text = wordsPerMinute.ToString();


            if (percentageRight.Equals(100))
            {
                awardStack.Visibility = System.Windows.Visibility.Visible;
            }


        }
        private string CalculatePercentageRight()
        {
            decimal percentageRight = 0;
            try
            {
                percentageRight = decimal.Divide(rightAnswers.Count,rightAnswers.Count+wrongAnswers.Count) * 100;
            }
            catch (DivideByZeroException)
            {
                percentageRight = 100;
            }
            string percentageRightStr = Math.Round(percentageRight).ToString() + "%";

            return percentageRightStr;
        }

    }
}
