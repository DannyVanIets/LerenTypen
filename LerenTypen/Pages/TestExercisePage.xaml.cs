using LerenTypen.Controllers;
using System;
using System.Collections.Generic;
using System.Media;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace LerenTypen
{
    /* <summary>
    Interaction logic for Page1.xaml

    This application uses these sounds from freesound:
    small crowd yelling 'YEAH' by Tomlija(http://freesound.org/people/Tomlija/ ),
    success 1, success 2 by Leszek_Szary (https://freesound.org/people/Leszek_Szary/),

    positive_beeps.wav, negative_beeps.wav by themusicalnomad (https://freesound.org/people/themusicalnomad/),
    Video Game SFX Positive Action Long Tail by djlprojects (https://freesound.org/people/djlprojects/),

    collect.wav by Wagna(http://freesound.org/people/Wagna/ ),
    Failure 1.wav by FunWithSound (https://freesound.org/people/FunWithSound/),

    Sad Trombone.wav by Benboncan(http://freesound.org/people/Benboncan/ ),
    negativebeep.wav by Leszek_Szary (https://freesound.org/people/Splashdust/),

    Wrong Buzzer by KevinVG207(http://freesound.org/people/KevinVG207/ ),
    Wrong answer. by SgtPepperArc36 (https://freesound.org/people/SgtPepperArc36/)

    </summary>*/
    public partial class TestExercisePage : Page
    {
        // Integers for counters
        private int k = 4;
        private int i = 0;
        private int j = 0;

        private Line secondLine;
        private Line minuteLine;

        private DispatcherTimer t1;
        private DispatcherTimer t2;
        private int currentLine; // user friendly line number (not zero-indexed)
        //private int currentLineIndex; // zero-indexed line number
        private int restoredWrongAnswers;
        private int restoredRightAnswers;
        private List<string> lines;
        private List<string> unfinishedLines;
        private Dictionary<int, string> wrongAnswers;
        private List<string> rightAnswers;
        private int amountOfPauses;
        private int testID;
        private bool testClosed;
        private string testName;
        private MainWindow m;
        //Soundplayer is the class we use for sounds. It can only include a file, play a file and stop playing any sounds.
        //It's pretty limited, but it's good enough for what we use it for. It also only supports .wav files!
        //We also already load in an sound that we can play on a loop. You can't hear this sound!
        private SoundPlayer sp = new SoundPlayer(@"soundsCorrect/EmptyWav.wav");
        Random random = new Random();

        private bool restoreState;
        private int unfinishedTestResultID;

        public TestExercisePage(int testID, MainWindow m, bool restoreState = false)
        {
            InitializeComponent();

            //Play the soundless sound indefinitely so that the application doesn't have a problem loading in the next sounds on time.
            //Soundplayer() sadly has a delay when the first sound is being loaded and this way, it loads it before we display the other sounds.
            sp.PlayLooping();

            // Bool to stop timer when test is closed
            testClosed = false;

            this.testID = testID;
            this.m = m;
            this.restoreState = restoreState;
            textInputBox.Focus();

            // Set the mute button's state according to the user's choice to play sounds or not
            if (m.testOptions.Sound)
            {
                Image unmutedImg = new Image();
                unmutedImg.Source = new BitmapImage(new Uri("/img/unmutedButton.png", UriKind.Relative));
                muteButton.Content = unmutedImg;

                // Set the tag to 0 (unmuted) so we can check the state later in the click event 
                muteButton.Tag = 0;
            }
            else
            {
                Image mutedImg = new Image();
                mutedImg.Source = new BitmapImage(new Uri("/img/mutedButton.png", UriKind.Relative));
                muteButton.Content = mutedImg;
                muteButton.Tag = 1;
            }

            amountOfPauses = 0;
            wrongAnswers = new Dictionary<int, string>();
            rightAnswers = new List<string>();
            currentLine = 0;
            wrongCounterLbl.Content = $"Aantal fout: {wrongAnswers.Count}";

            // Timer for game and showing answer
            t1 = new DispatcherTimer();
            t2 = new DispatcherTimer();
            t1.Interval = new TimeSpan(0, 0, 1);
            t1.Start();
            t1.Tick += StartTimer;

            // Gets the tests name and content using the given testID
            lines = TestController.GetTestContent(testID); // List for lines to be written out by user
            testName = TestController.GetTestName(testID);
            testNameLbl.Content = testName;
            lineNumberLbl.Content = $"1/{lines.Count}";

            if (restoreState)
            {
                // Restore the page in the same state as the unfinished test was saved

                unfinishedTestResultID = TestResultController.GetUnfinishedTestResultID(m.Ingelogd, testID);
                lines = TestController.GetAllLinesFromResult(unfinishedTestResultID);
                amountOfPauses = TestResultController.GetAmountOfPauses(unfinishedTestResultID);
                wrongAnswers = TestResultController.GetTestResultsContentWrong(testID, unfinishedTestResultID);
                rightAnswers = TestResultController.GetTestResultsContentRight(unfinishedTestResultID);
                int timeSeconds = TestResultController.GetTime(unfinishedTestResultID);
                i = timeSeconds % 60;
                j = timeSeconds / 60;

                currentLine = rightAnswers.Count + wrongAnswers.Count;
                restoredRightAnswers = rightAnswers.Count;
                restoredWrongAnswers = wrongAnswers.Count;
                wrongCounterLbl.Content = $"Aantal fouten: {wrongAnswers.Count}";
                lineNumberLbl.Content = $"{currentLine + 1}/{lines.Count}";
            }


            // Check if lines are found
            if (!lines.Count.Equals(0))
            {
                testLineLbl.Content = lines[currentLine];
            }
            else
            {
                MessageBox.Show("Geen regels gevonden", "Error");
                CloseTest();
            }

            // Make startup overlay visible for countdown
            Overlay.Visibility = Visibility.Visible;
            DrawClock();
            UpdateCanvas(null, new EventArgs());
            UpdateTimer(null, new EventArgs());
        }

        /// <summary>
        /// Draw Clock for displaying time
        /// </summary>
        private void DrawClock()
        {
            Ellipse el = new Ellipse();
            el.StrokeThickness = 2;
            el.Stroke = Brushes.Black;
            el.Width = 100;
            el.Height = 100;
            clock.Children.Add(el);

            for (int i = 0; i < 60; i++)
            {
                Line l3 = new Line();
                l3.Stroke = Brushes.Black;
                l3.X1 = 50;
                l3.X2 = 50;
                if (i % 5 == 0)
                {
                    l3.Y1 = 10;
                }
                else
                {
                    l3.Y1 = 5;
                }
                l3.Y2 = 0;
                l3.Opacity = 0.5;
                l3.StrokeThickness = 1;
                int rotation = 360 / 60 * i;
                //rotates each line to form clock lines
                l3.RenderTransform = new RotateTransform(rotation, 50, 50);
                clock.Children.Add(l3);
            }

            minuteLine = new Line();
            minuteLine.Stroke = Brushes.Black;
            minuteLine.X1 = 50;
            minuteLine.X2 = 50;
            minuteLine.Y1 = 50;
            minuteLine.Y2 = 0;
            minuteLine.StrokeThickness = 2;
            clock.Children.Add(minuteLine);

            secondLine = new Line();
            secondLine.Stroke = Brushes.Red;
            secondLine.X1 = 50;
            secondLine.X2 = 50;
            secondLine.Y1 = 50;
            secondLine.Y2 = 10;
            secondLine.StrokeThickness = 1;
            clock.Children.Add(secondLine);
        }

        /// <summary>
        /// Timer for countdown at the beginning of the exercise
        /// </summary>
        private void StartTimer(object sender, EventArgs e)
        {
            countDownLbl.Content = k - 1;
            k--;

            // Stop startTimer event and start events for the exercise
            if (k.Equals(0))
            {
                Overlay.Visibility = Visibility.Collapsed;
                t1.Tick -= StartTimer;
                t1.Tick += UpdateTimer;
                t1.Tick += UpdateCanvas;
                // Reset startup values
                k = 4;
                countDownLbl.Content = "";
            }
        }

        /// <summary>
        /// Method for showing resume button in overlay grid
        /// </summary>
        private void ShowResumeButton()
        {
            Overlay.Visibility = Visibility.Visible;
            resumeButton.Visibility = Visibility.Visible;
            lineCheckLbl.Content = "Toets gepauzeerd";
            lineCheckLbl.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Method for updating digital timer content using timer t1
        /// </summary>
        private void UpdateTimer(object sender, EventArgs e)
        {
            i++;
            if (i.Equals(60))
            {
                i = 0;
                j++;
            }

            if (i < 10)
            {
                timerLbl.Content = $"{j}:0{i}";
            }
            else
            {
                timerLbl.Content = $"{j}:{i}";
            }
        }

        /// <summary>
        /// Method for updating the clock drawn in canvas using timer t1
        /// </summary>
        private void UpdateCanvas(object sender, EventArgs e)
        {
            int rotationS = 360 / 60 * i;
            int rotationM = 360 / 60 * j;

            secondLine.RenderTransform = new RotateTransform(rotationS, 50, 50);
            if (i.Equals(0))
            {
                minuteLine.RenderTransform = new RotateTransform(rotationM, 50, 50);
            }
        }

        /// <summary>
        /// Method for showing if input is right or wrong after user hits enter or next, answer is shown in overlay grid.
        /// </summary>
        private void ShowRightOrWrong(bool right, string input)
        {
            textInputBox.IsEnabled = false;

            //Check if the option for sounds is turned on. If not, only change the brushes.
            if (m.testOptions.Sound)
            {
                //sp.stop() stops any sound or music that's still going on.
                sp.Stop();
                string file = "";
                int randomNumber = random.Next(0, 6);

                //Here we check if the answer is correct or not.
                if (right)
                {
                    countDownLbl.Foreground = Brushes.Green;

                    //Switch case for the number that the randomNumber is. Loads in a different file for every number!
                    //All the correct sounds are in the soundsCorrect folder. They come from: https://freesound.org/ and are free to use
                    switch (randomNumber)
                    {
                        case 0:
                            file = @"soundsCorrect/collect.wav";
                            break;
                        case 1:
                            file = @"soundsCorrect/crowdyeah.wav";
                            break;
                        case 2:
                            file = @"soundsCorrect/positive.wav";
                            break;
                        case 3:
                            file = @"soundsCorrect/positivebeep.wav";
                            break;
                        case 4:
                            file = @"soundsCorrect/succes1.wav";
                            break;
                        case 5:
                            file = @"soundsCorrect/succes2.wav";
                            break;
                    }
                }
                else
                {
                    countDownLbl.Foreground = Brushes.Red;

                    //All the wrong sounds are in the soundsWrong folder. They come from: https://freesound.org/ and are free to use
                    switch (randomNumber)
                    {
                        case 0:
                            file = @"soundsWrong/failure-1.wav";
                            break;
                        case 1:
                            file = @"soundsWrong/negativebeep.wav";
                            break;
                        case 2:
                            file = @"soundsWrong/negative-beeps.wav";
                            break;
                        case 3:
                            file = @"soundsWrong/sad-trombone.wav";
                            break;
                        case 4:
                            file = @"soundsWrong/wrong-answer.wav";
                            break;
                        case 5:
                            file = @"soundsWrong/wrong-buzzer.wav";
                            break;
                    }
                }

                //Sp.soundlocation is used to make sure the soundplayer goes to the right file and sp.load loads in the file.
                sp.SoundLocation = file;
                sp.Load();

                //This lambda query is used to delay the application until the loading from the soundfile is complete.
                //This way it doesn't stop the whole user-interface.
                Task.Factory.StartNew(() => { while (!sp.IsLoadCompleted) ; });
                sp.Play();
            }
            else
            {
                if (right)
                {
                    countDownLbl.Foreground = Brushes.Green;
                }
                else
                {
                    countDownLbl.Foreground = Brushes.Red;
                }
            }

            Overlay.Visibility = Visibility.Visible;
            t1.Stop();

            lineCheckLbl.Visibility = Visibility.Visible;
            lineCheckLbl.Content = lines[currentLine];

            if (input.Trim().Equals(""))
            {
                countDownLbl.Content = "Geen invoer";
            }
            else
            {
                countDownLbl.Content = input;
            }

            t2.Interval = new TimeSpan(0, 0, 2);
            t2.Tick += StopShowingRightOrWrong;
            t2.Start();
        }

        /// <summary>
        /// Method for collapsing overlay and resetting variables after timer t2 fires event in showRightOrWrong
        /// </summary>        
        private void StopShowingRightOrWrong(object sender, EventArgs e)
        {
            t2.Stop();
            t2.Tick -= StopShowingRightOrWrong;
            Overlay.Visibility = Visibility.Collapsed;
            lineCheckLbl.Visibility = Visibility.Collapsed;
            lineCheckLbl.Content = "";
            countDownLbl.Content = "";
            countDownLbl.Foreground = Brushes.Black;
            textInputBox.IsEnabled = true;
            textInputBox.Focus();
            // Checking if last line has been answered to make sure timer is not started again after ending.
            if (!testClosed)
            {
                t1.Start();
            }
            else
            {
                CloseTest();
            }
        }

        private void NextLineButton_Click(object sender, RoutedEventArgs e)
        {
            NextLine();
        }

        /// <summary>
        /// Stops timer and adds one to amount of pauses
        /// </summary>       
        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            t1.Stop();
            amountOfPauses++;
            ShowResumeButton();
        }

        private void ResumeButton_Click(object sender, RoutedEventArgs e)
        {
            Resume();
        }

        /// <summary>
        /// Uses StartTimer to resume application with countdown, startTimer adds updateTimer and updateCanvas again after countdown.
        /// </summary>
        private void Resume()
        {
            resumeButton.Visibility = Visibility.Collapsed;
            lineCheckLbl.Content = "";
            lineCheckLbl.Visibility = Visibility.Collapsed;
            textInputBox.Focus();
            t1.Tick -= UpdateTimer;
            t1.Tick -= UpdateCanvas;
            t1.Tick += StartTimer;
            t1.Start();
        }

        /// <summary>
        /// Shows message box to ask if user is sure to quit, when answered yes method CloseTest is called.
        /// </summary>        
        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            StopTest();
        }

        public MessageBoxResult AskStopTest()
        {
            // Check if the overlay isn't visible so that the user can't quit the test between answers (can cause issues!)
            if (Overlay.Visibility != Visibility.Visible)
            {
                MessageBoxResult choice = MessageBox.Show("Je staat op het punt de toets te stoppen. Wil je je voortgang opslaan zodat je later verder kan gaan waar je gebleven bent?",
                    "Toets verlaten?", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);

                if (choice == MessageBoxResult.Yes)
                {
                    StopTest();
                }
                else if (choice == MessageBoxResult.No)
                {
                    StopTest(false);
                }

                return choice;
            }

            return MessageBoxResult.Cancel;
        }

        /// <summary>
        /// Method for early closing test, user is navigated to testoverview and unfinished test is saved to db
        /// </summary>
        private void StopTest(bool save = true)
        {
            // Delete the unfinished result if test is resumed
            if (restoreState)
            {
                TestResultController.DeleteTestResult(unfinishedTestResultID);
            }

            if (save)
            {
                SaveResults(false);
            }

            m.frame.Navigate(new TestOverviewPage(m));
        }

        /// <summary>
        /// Mute or unmute the sounds for the test
        /// </summary>
        private void MuteButton_Click(object sender, RoutedEventArgs e)
        {
            if ((int)muteButton.Tag == 0)
            {
                m.testOptions.Sound = false;
                Image mutedImg = new Image();
                mutedImg.Source = new BitmapImage(new Uri("/img/mutedButton.png", UriKind.Relative));
                muteButton.Content = mutedImg;
                muteButton.Tag = 1;
            }
            else if ((int)muteButton.Tag == 1)
            {
                m.testOptions.Sound = true;
                Image unmutedImg = new Image();
                unmutedImg.Source = new BitmapImage(new Uri("/img/unmutedButton.png", UriKind.Relative));
                muteButton.Content = unmutedImg;
                muteButton.Tag = 0;
            }
        }

        /// <summary>
        /// Allows user to hit enter for next line.
        /// </summary>
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                NextLine();
            }
        }

        /// <summary>
        /// Shows next line and makes sure input is checked and if answer is right or wrong is shown.
        /// If last line has been made, CloseTest is called.
        /// </summary>
        private void NextLine()
        {
            string input = textInputBox.Text;
            textInputBox.Text = "";
            bool right = CheckInput(input);
            ShowRightOrWrong(right, input);

            bool shouldGoToNextLine;

            shouldGoToNextLine = true;
            if (!(currentLine < lines.Count - 1))
            {
                shouldGoToNextLine = false;
            }

            if (shouldGoToNextLine)
            {
                currentLine++;
                testLineLbl.Content = lines[currentLine];

                lineNumberLbl.Content = $"{currentLine + 1}/{lines.Count}";
            }
            else
            {
                testClosed = true;
            }
        }

        /// <summary>
        /// Checks if users input equals the currentLine when trimmed, if wrong input, 
        /// amount of wrong answers gets added by one and the line is added to the test again to be made 4 lines later.
        /// </summary>
        private bool CheckInput(string input, int key = -1)
        {
            bool answerCorrect;
            answerCorrect = input.Trim().Equals(lines[currentLine].Trim());

            if (answerCorrect)
            {
                rightAnswers.Add(input);
                return true;
            }
            else
            {

                wrongAnswers.Add(currentLine, input);

                wrongCounterLbl.Content = $"Aantal fouten: {wrongAnswers.Count}";
                if (currentLine + 4 < lines.Count)
                {
                    lines.Insert(currentLine + 4, lines[currentLine]);
                }
                else
                {
                    lines.Add(lines[currentLine]);
                }
            }
            return false;
        }

        /// <summary>
        /// Method for closing test and navigating to resultspage
        /// </summary>
        private void CloseTest()
        {
            testClosed = true;
            t1.Stop();

            // Delete the unfinished result if test is resumed
            if (restoreState)
            {
                TestResultController.DeleteTestResult(unfinishedTestResultID);
            }

            int resultID = SaveResults();
            TestResultsPage testResultsPage = new TestResultsPage(testID, m, resultID);
            m.frame.Navigate(testResultsPage);
        }

        /// <summary>
        /// Results are stored in database after exercising test
        /// </summary>
        /// <returns></returns>
        private int SaveResults(bool finished = true)
        {
            int amountOfWrong = wrongAnswers.Count;
            decimal wordsPerMinute = CalculateWordsPerMinute();
            decimal percentageRight = CalculatePercentageRight();
            int resultID = TestResultController.SaveResults(testID, m.Ingelogd, (int)wordsPerMinute, amountOfPauses, rightAnswers, wrongAnswers, lines, (int)percentageRight, j * 60 + i, finished);
            return resultID;
        }

        /// <summary>
        /// Words per minute are calculated to be stored in database
        /// </summary>
        /// <returns></returns>
        private decimal CalculateWordsPerMinute()
        {
            decimal secondsToMinutes;
            try
            {
                secondsToMinutes = decimal.Divide(i, 60);
            }
            catch (DivideByZeroException)
            {
                secondsToMinutes = 0;
            }
            catch (OverflowException)
            {
                secondsToMinutes = 0;
            }
            decimal minutesSpend = j + secondsToMinutes;
            decimal wordsPerMinute = 0;

            if (minutesSpend != 0)
            {
                wordsPerMinute = TestController.GetAmountOfWordsFromTest(testID) / minutesSpend;
            }
            else
            {
                wordsPerMinute = 0;
            }
            wordsPerMinute = Math.Round(wordsPerMinute);
            return wordsPerMinute;
        }

        /// <summary>
        /// Calculates the percentage of answers answered right
        /// </summary>
        /// <returns></returns>
        private decimal CalculatePercentageRight()
        {
            decimal percentageRight;
            try
            {
                percentageRight = decimal.Divide(rightAnswers.Count, rightAnswers.Count + wrongAnswers.Count) * 100;
            }
            catch (DivideByZeroException)
            {
                percentageRight = 100;
            }
            return percentageRight;
        }
    }
}
