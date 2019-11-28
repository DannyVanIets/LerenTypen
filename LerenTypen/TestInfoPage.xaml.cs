using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace LerenTypen
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class TestInfoPage : Page
    {
        private MainWindow mainWindow;
        private int testID;

        public TestInfoPage(int testID, MainWindow mainWindow)
        { 
            InitializeComponent();

            this.mainWindow = mainWindow;
            this.testID = testID;

            Test test = Database.GetTest(testID);
            testNameLabel.Content = test.Name;

            string difficultyString = "";
            switch (test.Difficulty)
            {
                case 0:
                    difficultyString = "makkelijk";
                    break;
                case 1:
                    difficultyString = "gemiddeld";
                    break;
                case 2:
                    difficultyString = "moeilijk";
                    break;

            }
            difficultyLabel.Content = $"Moeilijkheidsgraad: {difficultyString}";

            testVersionLabel.Content = $"Versie {test.Version} - {test.CreatedDateTime}";

            string testTypeString = "";
            switch (test.Type)
            {
                case 0:
                    testTypeString = "woorden";
                    break;
                case 1:
                    testTypeString = "zinnen";
                    break;
            }
            testContentTextBox.AppendText($"Type toets: {testTypeString}\n");

            testContentTextBox.AppendText("\nOpgaven:\n");
            List<string> testContent = Database.GetTestContent(testID);
            foreach (string line in testContent)
            {
                testContentTextBox.AppendText($"{line}\n");
            }

            usernameLinkText.Text = test.AuthorUsername;
            usernameLink.Click += (s, e) =>
            {
                // Go to account info page, use test.AuthorID
            };

            amountOfWordsLabel.Content = test.WordCount;
            timesMadeLabel.Content = test.TimesMade;
            avarageScoreLabel.Content = $"{test.AverageScore}%";
            highscoreLabel.Content = $"{test.Highscore}%";

            myResultsListView.ItemsSource = Database.GetAllTestResultsFromAccount(test.AuthorID, testID);

            Dictionary<int, int> top3Fastest = Database.GetTop3FastestTypers(testID);
            foreach (KeyValuePair<int, int> kvp in top3Fastest)
            {
                top3FastestTypersListView.Items.Add($"{Database.GetUserName(kvp.Key)}: {kvp.Value} woorden per minuut");
            }

            Dictionary<int, int> top3Highscore = Database.GetTop3Highscores(testID);
            foreach (KeyValuePair<int, int> kvp in top3Fastest)
            {
                top3HighestScoresListView.Items.Add($"{Database.GetUserName(kvp.Key)}: {(int)kvp.Value}% goed");
            }
        }

        private void MyResultsListView_PreviewMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ListViewItem listViewItem = sender as ListViewItem;

            if (listViewItem != null)
            {
                TestResult clickedResult = listViewItem.DataContext as TestResult;
                mainWindow.ChangePage(new TestResultsPage(testID, mainWindow, clickedResult.ID));
            }
        }

        private void StartTestButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.ChangePage(new TestExercisePage(testID, mainWindow));
        }
    }
}
