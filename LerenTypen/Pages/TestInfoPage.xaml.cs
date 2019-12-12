using LerenTypen.Controllers;
using LerenTypen.Models;
using System.Collections.Generic;
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

            Test test = TestController.GetTest(testID);
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
                case 1:
                    testTypeString = "woorden";
                    break;
                case 0:
                    testTypeString = "zinnen";
                    break;
            }
            testContentTextBox.AppendText($"Type toets: {testTypeString}\n");

            testContentTextBox.AppendText("\nOpgaven:\n");
            List<string> testContent = TestController.GetTestContent(testID);
            foreach (string line in testContent)
            {
                testContentTextBox.AppendText($"{line}\n");
            }

            usernameLinkText.Text = test.AuthorUsername;
            usernameLink.Click += (s, e) =>
            {
                mainWindow.ChangePage(new AccountInformationPage(mainWindow, test.AuthorID));
            };

            amountOfWordsLabel.Content = test.WordCount;
            timesMadeLabel.Content = test.TimesMade;
            avarageScoreLabel.Content = $"{test.AverageScore}%";
            highscoreLabel.Content = $"{test.Highscore}%";

            myResultsListView.ItemsSource = TestResultController.GetAllTestResultsFromAccount(mainWindow.Ingelogd, testID);

            Dictionary<int, int> top3Fastest = TestController.GetTop3FastestTypers(testID);
            foreach (KeyValuePair<int, int> kvp in top3Fastest)
            {
                top3FastestTypersListView.Items.Add($"{AccountController.GetUsername(kvp.Key)}: {kvp.Value} woorden per minuut");
            }

            Dictionary<int, int> top3Highscore = TestController.GetTop3Highscores(testID);
            foreach (KeyValuePair<int, int> kvp in top3Highscore)
            {
                top3HighestScoresListView.Items.Add($"{AccountController.GetUsername(kvp.Key)}: {(int)kvp.Value}% goed");
            }

            if (mainWindow.Ingelogd == 0)
            {
                startTestButton.Visibility = Visibility.Collapsed;
            }

            //Go through every option to check if they have been enabled in a previous test and been put on true.
            playSoundsCheckBox.IsChecked = mainWindow.testOptions.Sound;
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
            //Check if the option for sounds has been clicked on. If so, change the array position 0 to true. If not, to false.
            if (playSoundsCheckBox.IsChecked == true)
            {
                mainWindow.testOptions.Sound = true;
            }
            else
            {
                mainWindow.testOptions.Sound = false;
            }

            if (TestController.GetUnfinishedTestsFromAccount(mainWindow.Ingelogd).Contains(testID))
            {
                mainWindow.ChangePage(new TestExercisePage(testID, mainWindow, true));
            }
            else
            {
                mainWindow.ChangePage(new TestExercisePage(testID, mainWindow));
            }
        }
    }
}
