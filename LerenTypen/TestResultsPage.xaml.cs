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
        public TestResultsPage(int testID, MainWindow m, Dictionary<int, string> wrongAnswers, List<string> lines, List<string> rightAnswers)
        {
            InitializeComponent();
            this.testID = testID;
            this.m = m;
            this.wrongAnswers = wrongAnswers;

            foreach (KeyValuePair<int, string> answer in wrongAnswers)
            {
                AnswersLv.Items.Add($"{answer.Value} \nJuiste antwoord: {lines[answer.Key]}");
            }
            foreach (string answer in rightAnswers)
            {
                AnswersLv.Items.Add($"{answer} \nGoed gedaan!");
            }


        }

        private void RestartButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            m.frame.Navigate(new TestExercisePage(testID, m));
        }       
        
    }
}
