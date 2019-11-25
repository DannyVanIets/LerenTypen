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
        public TestResultsPage(int testID, MainWindow m, Dictionary<int, string> wrongAnswers, List<string> lines, List<string> rightAnswers)
        {
            InitializeComponent();
            this.testID = testID;
            this.m = m;
            this.wrongAnswers = wrongAnswers;
            this.rightAnswers = rightAnswers;
            this.lines = lines;
            FillAnswerList(false);


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
    }
}
