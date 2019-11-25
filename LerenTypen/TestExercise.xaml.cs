using System;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace LerenTypen
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class TestExercise : Page
    {
        // Integers for counters
        private int k = 4;
        private int i = 0;
        private int j = 0;

        private Line secondLine;
        private Line minuteLine;

        private DispatcherTimer t1;
        private DispatcherTimer t2;
        private int currentLine;
        private List<string> lines;
        private List<string> wrongAnswers;
        private List<string> rightAnswers;
        private int amountOfPauses;
        private int testID;
        private bool testClosed;

        public TestExercise(int testID)
        {
            InitializeComponent();
            // Bool to stop timer when test is closed
            testClosed = false;
            textInputBox.Focus();
            // List for lines to be written out by user
            lines = new List<string>();
            this.testID = testID;
            
            amountOfPauses = 0;
            wrongAnswers = new List<string>();
            rightAnswers = new List<string>();
            currentLine = 0;
            wrongCounter.Content = $"Aantal fouten: {wrongAnswers.Count}";

            // Timer for game and showing answer
            t1 = new DispatcherTimer();
            t2 = new DispatcherTimer();
            t1.Interval = new TimeSpan(0, 0, 1);
            t1.Start();
            t1.Tick += StartTimer;

            GetTest(testID);

            // Check if lines are found
            if (!lines.Count.Equals(0))
            {
                testLine.Content = lines[currentLine];
                lineNumber.Content = $"1/{lines.Count}";
            }
            else
            {
                MessageBox.Show("Geen regels gevonden", "Error");
                CloseTest();
            }

            // Make startup overlay visible for countdown
            Overlay.Visibility = System.Windows.Visibility.Visible;
            DrawClock();           
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
        /// gets the tests name and content using the given testID
        /// </summary>
        private void GetTest(int testID)
        {
            lines = Database.GetTestContent(testID);
            testName.Content = Database.GetTestName(testID);
        }

        /// <summary>
        /// Timer for countdown at the beginning of the exercise
        /// </summary>        
        private void StartTimer(object sender, EventArgs e)
        {
            countDown.Content = k-1;           
            k--;

            // Stop startTimer event and start events for the exercise
            if (k.Equals(0))
            {
                Overlay.Visibility = System.Windows.Visibility.Collapsed;
                t1.Tick -= StartTimer;
                t1.Tick += UpdateTimer;
                t1.Tick += UpdateCanvas;
                // Reset startup values
                k = 4;
                countDown.Content = "";
            }
        }
        /// <summary>
        /// Method for showing resume button in overlay grid
        /// </summary>
        private void ShowResumeButton()
        {
            Overlay.Visibility = System.Windows.Visibility.Visible;
            resumeButton.Visibility = System.Windows.Visibility.Visible;
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
                timer.Content = $"{j}:0{i}";
            }
            else
            {
                timer.Content = $"{j}:{i}";
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
        private void ShowRightOrWrong(Boolean right, string input)
        {
            textInputBox.IsEnabled = false;            
            if (right){
                countDown.Foreground = Brushes.Green;                
            }
            else
            {
                countDown.Foreground = Brushes.Red;
            }
            Overlay.Visibility = System.Windows.Visibility.Visible;
            t1.Stop();

            lineCheckLbl.Visibility = Visibility.Visible;
            lineCheckLbl.Content = lines[currentLine];
            countDown.Content = input;

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
            Overlay.Visibility = System.Windows.Visibility.Collapsed;
            lineCheckLbl.Visibility = Visibility.Collapsed;
            lineCheckLbl.Content = "";
            countDown.Content = "";
            countDown.Foreground = Brushes.Black;
            textInputBox.IsEnabled = true;
            textInputBox.Focus();
            // Checking if last line has been answered to make sure timer is not started again after ending.
            if (!testClosed)
            {
                t1.Start();
            }
        }

        private void NextLineButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NextLine();
        }

        /// <summary>
        /// Stops timer and adds one to amount of pauses
        /// </summary>       
        private void PauseButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            t1.Stop();
            amountOfPauses++;
            ShowResumeButton();
        }

        private void ResumeButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Resume();           
        }

        /// <summary>
        /// Uses StartTimer to resume application with countdown, startTimer adds updateTimer and updateCanvas again after countdown.
        /// </summary>
        private void Resume()
        {
            resumeButton.Visibility = System.Windows.Visibility.Collapsed;
            lineCheckLbl.Content = "";
            lineCheckLbl.Visibility = Visibility.Collapsed;
            t1.Tick -= UpdateTimer;
            t1.Tick -= UpdateCanvas;
            t1.Tick += StartTimer;
            t1.Start();
        }
        
        /// <summary>
        /// Shows message box to ask if user is sure to quit, when answered yes method CloseTest is called.
        /// </summary>        
        private void StopButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (MessageBox.Show("Weet je zeker dat je de toets wilt verlaten?", "Toets verlaten?", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                
            }
            else
            {
                CloseTest();
            }
        }

        /// <summary>
        /// Empty method for now (other user story)
        /// </summary>
        private void MuteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Allowes user to hit enter for next line.
        /// </summary>
        private void TextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == System.Windows.Input.Key.Enter)
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
            currentLine++;

            if (currentLine < lines.Count)
            {                
                testLine.Content = lines[currentLine];
                lineNumber.Content = $"{currentLine+1}/{lines.Count}";
            }
            else
            {
                CloseTest();
            }
                     
        }

        /// <summary>
        /// Checks if users input equals the currentLine when trimmed, if wrong input, 
        /// amount of wrong answers gets added by one and the line is added to the test again to be made 4 lines later.
        /// </summary>
        private bool CheckInput(string input)
        {
            if (input.Trim().Equals(lines[currentLine].Trim()))
            {
                rightAnswers.Add(input);
                return true;
            }
            else
            {
                wrongAnswers.Add(input);
                wrongCounter.Content = $"Aantal fouten: {wrongAnswers.Count}";
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
        /// Method for closing test, cannot be made fully functional yet. (to be going to resultsPage)
        /// </summary>
        private void CloseTest()
        {
            testClosed = true;
            t1.Stop();
            Console.WriteLine("close");            
        }
    }
}
