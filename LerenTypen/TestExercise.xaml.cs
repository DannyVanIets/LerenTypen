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
        private int k = 4;
        private int i = 0;
        private int j = 0;
        private Line secondLine;
        private Line minuteLine;
        private DispatcherTimer t1;
        DispatcherTimer t2;
        private int currentLine;
        private List<string> lines;
        private List<string> wrongAnswers;
        private List<string> rightAnswers;
        private int amountOfPauses;
        private int testID;
        private bool testClosed;

        public TestExercise()
        {
            InitializeComponent();
            testClosed = false;
            textInputBox.Focus();
            lines = new List<string>();
            testID = 1;
            
            amountOfPauses = 0;
            wrongAnswers = new List<string>();
            rightAnswers = new List<string>();
            currentLine = 0;
            wrongCounter.Content = $"Aantal fouten: {wrongAnswers.Count}";

            t1 = new DispatcherTimer();
            t2 = new DispatcherTimer();
            t1.Interval = new TimeSpan(0, 0, 1);
            t1.Start();
            t1.Tick += StartTimer;

            GetTest(testID);

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

            Overlay.Visibility = System.Windows.Visibility.Visible;
            DrawClock();           
        }

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
       
        private void GetTest(int testID)
        {
            lines = Database.GetTestContent(testID);
            testName.Content = Database.GetTestName(testID);
        }
        private void StartTimer(object sender, EventArgs e)
        {
            countDown.Content = k-1;           
            k--;

            if (k.Equals(0))
            {
                Overlay.Visibility = System.Windows.Visibility.Collapsed;
                t1.Tick -= StartTimer;
                t1.Tick += UpdateTimer;
                t1.Tick += UpdateCanvas;
                k = 4;
                countDown.Content = "";
            }
        }
        private void ShowResumeButton()
        {
            Overlay.Visibility = System.Windows.Visibility.Visible;
            resumeButton.Visibility = System.Windows.Visibility.Visible;
        }

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
            if (!testClosed)
            {
                t1.Start();
            }
        }

        private void NextLineButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NextLine();
        }

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
        private void Resume()
        {
            resumeButton.Visibility = System.Windows.Visibility.Collapsed;
            t1.Tick -= UpdateTimer;
            t1.Tick -= UpdateCanvas;
            t1.Tick += StartTimer;
            t1.Start();
        }

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

        private void MuteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == System.Windows.Input.Key.Enter)
            {
                NextLine();
            }
        }
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

        private void CloseTest()
        {
            testClosed = true;
            t1.Stop();
            Console.WriteLine("close");            
        }
    }
}
