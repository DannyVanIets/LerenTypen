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
        private Dictionary<int, string> wrongAnswers;
        private List<string> rightAnswers;
        private List<string> lines;
        private int amountOfPauses;
        private int amountOfSeconds;
        private int amountOfMinutes;
        public TestResultsPage(int testID, MainWindow m, Dictionary<int, string> wrongAnswers, List<string> lines, List<string> rightAnswers, int amountOfPauses, int amountOfMinutes, int amountOfSeconds)
        {
            InitializeComponent();
            this.testID = testID;
            this.m = m;
            this.wrongAnswers = wrongAnswers;
            this.rightAnswers = rightAnswers;
            this.lines = lines;
            this.amountOfSeconds = amountOfSeconds;
            this.amountOfMinutes = amountOfMinutes;
            this.amountOfPauses = amountOfPauses;
            FillAnswerList(false);

            /*List<int> testInformation = Database.GetTestInformation(testID);
            foreach(int info in testInformation)
            {
                Console.WriteLine(info);
            }
            */
        }

        private void FillAnswerList(bool check)
        {
            AnswersLv.Items.Clear();

            foreach (KeyValuePair<int, string> answer in wrongAnswers)
            {
                AnswersLv.Items.Add($"{answer.Value} \nJuiste antwoord: {lines[answer.Key]}");
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

        private void CreateResults()
        {
            amountOfWrongTbl.Text = wrongAnswers.Count.ToString();
            amountOfBreaksTbl.Text = amountOfPauses.ToString();
            decimal percentageRight = 0;
            try
            {
                percentageRight = decimal.Divide(rightAnswers.Count, lines.Count) * 100;
            }
            catch (DivideByZeroException)
            {
                percentageRight = 100;
            }
            percentageRightTbl.Text = Math.Round(percentageRight).ToString() + "%";
            decimal secondsToMinutes;
            try
            {
                secondsToMinutes = decimal.Divide(amountOfSeconds, 60);
            }
            catch (DivideByZeroException)
            {
                secondsToMinutes = 0;
            }
            catch (OverflowException)
            {
                secondsToMinutes = 0;
            }
            decimal minutesSpend = amountOfMinutes + secondsToMinutes;
            decimal wordsPerMinute = 0;

            if (minutesSpend != 0)
            {
                wordsPerMinute = rightAnswers.Count / minutesSpend;
            }
            else
            {
                wordsPerMinute = 0;
            }
            wordsPerMinuteTbl.Text = Math.Round(wordsPerMinute).ToString();
           

            if (percentageRight.Equals(100))
            {
                awardStack.Visibility = System.Windows.Visibility.Visible;
            }


        }

    }
}
