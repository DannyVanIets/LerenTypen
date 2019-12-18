using LerenTypen.Controllers;
using LerenTypen.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LerenTypen
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class TestInfoPage : Page
    {
        private MainWindow mainWindow;
        private int testID;
        private int Reviewscore;

        public TestInfoPage(int testID, MainWindow mainWindow)
        {
            InitializeComponent();

            this.mainWindow = mainWindow;
            this.testID = testID;
            
            // Get tests
            Test test = TestController.GetTest(testID);
            testNameLabel.Content = test.Name;

            //Get all the review info
            List<Review> inforeview = ReviewController.GetUserReviewDetails(testID);

            foreach (Review review in inforeview)
            {
                //Make border around review part.
                Border border = new Border();
                border.BorderThickness = new Thickness(1);
                border.BorderBrush = Brushes.Black;
                UserInfoFill.Children.Add(border);
                
                //Get username
                Label username = new Label();
                username.FontSize = 15;
                username.Content = review.AccountUsername;

                //get the score then convert them into stars.
                StackPanel starscore = new StackPanel();
                starscore.Orientation = Orientation.Horizontal;
                double Reviewscore = TestController.GetRatingScore(testID);
                int ratingscore = (int)Math.Floor(Reviewscore);

                //Print all the full stars
                for (int i = 0; i < ratingscore; i++)
                {
                    Image fullstar = new Image();
                    fullstar.Source = new BitmapImage(new Uri("/img/FullStar.png", UriKind.Relative));
                    fullstar.Width = 16;
                    starscore.Children.Add(fullstar);
                }
                //print the half stars for the score
                if (Reviewscore % 1 != 0)
                {
                    Image halfstar = new Image();
                    halfstar.Source = new BitmapImage(new Uri("/img/HalfStar.png", UriKind.Relative));
                    halfstar.Width = 16;
                    starscore.Children.Add(halfstar);
                }
                //print the date which the user made the review
                Label date = new Label();
                date.FontSize = 15;
                date.Content = review.ReviewDateAdded;
                //Put the stackpanel together
                StackPanel scores = new StackPanel();
                scores.Orientation = Orientation.Horizontal;
                scores.Children.Add(username);
                scores.Children.Add(starscore);
                scores.Children.Add(date);

                UserInfoFill.Children.Add(scores);
                //check if the description isnt empty
                if (review.ReviewDescription != null)
                {
                    Label description = new Label();
                    description.FontSize = 15;
                    description.Content = review.ReviewDescription;
                    UserInfoFill.Children.Add(description);
                }
            }

            double Review = TestController.GetRatingScore(testID);

            int rating = (int)Math.Floor(Review);

            for (int i = 0; i < rating; i++)
            {
                Image fullstar = new Image();
                fullstar.Source = new BitmapImage(new Uri("/img/FullStar.png", UriKind.Relative));
                fullstar.Width = 16;
                ReviewScorePanel.Children.Add(fullstar);
            }

            if (Review % 1 != 0)
            {
                Image halfstar = new Image();
                halfstar.Source = new BitmapImage(new Uri("/img/HalfStar.png", UriKind.Relative));
                halfstar.Width = 16;
                ReviewScorePanel.Children.Add(halfstar);
            }

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
            mainWindow.ChangePage(new TestExercisePage(testID, mainWindow));
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
            else
            {
                MessageBox.Show("Je moet een geheel cijfer invoeren bij aantal sterren!");
            }
        }
    }
}