﻿using LerenTypen.Controllers;
using LerenTypen.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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

            if (AccountController.IsTeacher(mainWindow.Ingelogd))
            {
                editTestBtn.Visibility = Visibility.Visible;

            }

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
                playSoundsCheckBox.IsEnabled = false;
            }

            //Go through every option to check if they have been enabled in a previous test and been put on true.
            playSoundsCheckBox.IsChecked = mainWindow.testOptions.Sound;

            //Check if the user has logged in or has already filled in a review for this test.
            //If that's true, then the user isn't allowed to see anything that has to do with inserting a new review.
            if (mainWindow.Ingelogd == 0 || ReviewController.CheckIfUserHasMadeAReview(testID, mainWindow.Ingelogd))
            {
                addReviewLabel.Visibility = Visibility.Collapsed;
                reviewMustsLabel.Visibility = Visibility.Collapsed;
                reviewScoreLabel.Visibility = Visibility.Collapsed;
                reviewScoreTextbox.Visibility = Visibility.Collapsed;
                reviewDescriptionLabel.Visibility = Visibility.Collapsed;
                reviewDescriptionTextbox.Visibility = Visibility.Collapsed;
                saveButton.Visibility = Visibility.Collapsed;
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

            // Check if the test already has an unfinished result, if so, resume this result
            if (TestController.GetUnfinishedTestIDsFromAccount(mainWindow.Ingelogd).Contains(testID))
            {
                mainWindow.ChangePage(new TestExercisePage(testID, mainWindow, true));
            }
            else
            {
                mainWindow.ChangePage(new TestExercisePage(testID, mainWindow));
            }
        }

        /// <summary>
        /// This function is used once you press the button that you want to add a review.
        /// </summary>
        private void AddReviewButton_Click(object sender, RoutedEventArgs e)
        {
            //Checks if the reviewScore is numberic and filled in.
            if (ReviewController.OnlyNumberic(reviewScoreTextbox.Text) && !string.IsNullOrWhiteSpace(reviewScoreTextbox.Text))
            {
                int reviewScore = int.Parse(reviewScoreTextbox.Text);
                string reviewDescription = reviewDescriptionTextbox.Text;

                //Check if the reviewScore are between 1 and 5.
                if (reviewScore < 1 || reviewScore > 5)
                {
                    MessageBox.Show("De score moet groter of gelijk aan 1 en groter of gelijk aan 5!", "Error");
                }
                //Check if the reviewDescription is longer than 140 charachters.
                else if (reviewDescription.Length >= 141)
                {
                    MessageBox.Show("De beschrijving moet kleiner zijn dan 140 tekens!", "Error");
                }
                //Checks if the revieDescription has been filled in. If not, it adds a review without a description.
                //If it is, it does a different query and adds a review with a description.
                else if (string.IsNullOrWhiteSpace(reviewDescription))
                {
                    Review review = new Review(testID, mainWindow.Ingelogd, reviewScore);

                    //Checks if the review has been succesfully added.
                    if (ReviewController.AddReviewWithoutDescription(review))
                    {
                        MessageBox.Show("De review is succesvol toegevoegd!", "Succes");
                        mainWindow.frame.Navigate(new TestInfoPage(testID, mainWindow));
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
                        mainWindow.frame.Navigate(new TestInfoPage(testID, mainWindow));
                    }
                    else
                    {
                        MessageBox.Show("De review kon niet worden toegevoegd. Probeer het opnieuw of neem contact op met een administrator.", "Error");
                    }
                }
            }
            else
            {
                MessageBox.Show("Je moet een geheel cijfer invoeren bij aantal sterren!");
            }
        }

        private void EditTestBtn_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.ChangePage(new CreateTestPage(mainWindow, testID));
        }
    }
}