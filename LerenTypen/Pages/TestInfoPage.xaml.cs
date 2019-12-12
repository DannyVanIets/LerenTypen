using LerenTypen.Controllers;
using LerenTypen.Models;
using System;
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

            if (mainWindow.Ingelogd == 0 || ReviewController.CheckIfUserHasMadeAReview(testID, mainWindow.Ingelogd))
            {
                addReviewLabel.Visibility = Visibility.Hidden;
                reviewMustsLabel.Visibility = Visibility.Hidden;
                reviewScoreLabel.Visibility = Visibility.Hidden;
                reviewScoreTextbox.Visibility = Visibility.Hidden;
                reviewDescriptionLabel.Visibility = Visibility.Hidden;
                reviewDescriptionTextbox.Visibility = Visibility.Hidden;
                saveButton.Visibility = Visibility.Hidden;
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
            //Check if the option for sounds has been clicked on. If so, change the array position 0 to true. If not, to false.
            if (playSoundsCheckBox.IsChecked == true)
            {
                mainWindow.testOptions.Sound = true;
            }
            else
            {
                mainWindow.testOptions.Sound = false;
            }

            mainWindow.ChangePage(new TestExercisePage(testID, mainWindow));
        }

        private void AddReviewButton_Click(object sender, RoutedEventArgs e)
        {
            int reviewScore = int.Parse(reviewScoreTextbox.Text);
            string reviewDescription = reviewDescriptionTextbox.Text;

            if (string.IsNullOrWhiteSpace(reviewScoreTextbox.Text))
            {
                MessageBox.Show("Je moet een review score invoeren!", "Error");
            }
            else if (reviewScore < 1 || reviewScore > 5)
            {
                MessageBox.Show("De score moet groter of gelijk aan 1 en groter of gelijk aan 5!", "Error");
            }
            else if (reviewDescription.Length >= 140)
            {
                MessageBox.Show("De beschrijving moet kleiner zijn dan 140 tekens!", "Error");
            }
            else if (string.IsNullOrWhiteSpace(reviewDescription))
            {
                Review review = new Review(testID, mainWindow.Ingelogd, reviewScore);

                if (ReviewController.AddReviewWithoutDescription(review))
                {
                    MessageBox.Show("De review is succesvol toegevoegd!", "Succes");
                    mainWindow.ChangePage(new TestInfoPage(testID, mainWindow));
                }
                else
                {
                    MessageBox.Show("De review kon niet worden toegevoegd. Probeer het opnieuw of neem contact op met een administrator.", "Error");
                }
            }
            else
            {
                Review review = new Review(testID, mainWindow.Ingelogd, reviewScore, reviewDescription);

                if (ReviewController.AddReviewWithDescription(review))
                {
                    MessageBox.Show("De review is succesvol toegevoegd!", "Succes");
                    mainWindow.ChangePage(new TestInfoPage(testID, mainWindow));
                }
                else
                {
                    MessageBox.Show("De review kon niet worden toegevoegd. Probeer het opnieuw of neem contact op met een administrator.", "Error");
                }
            }
        }
    }
}
