﻿using System.Windows.Controls;

namespace LerenTypen
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class TestInfoPage : Page
    {
        public TestInfoPage(int testID)
        {
            InitializeComponent();

            Test test = Database.GetTest(testID);
            testNameLabel.Content = test.Name;
            difficultyLabel.Content = test.Difficulty;
            testVersionLabel.Content = $"{test.Version} {test.CreatedDateTime}";

            usernameLinkText.Text = test.AuthorUsername;
            usernameLink.Click += (s, e) =>
            {
                // Go to account info page, use test.AuthorID
            };

            amountOfWordsLabel.Content = test.WordCount;
            timesMadeLabel.Content = test.TimesMade;
            avarageScoreLabel.Content = $"{test.AverageScore} %";
            highscoreLabel.Content = $"{test.Highscore}";         
        }
    }
}
