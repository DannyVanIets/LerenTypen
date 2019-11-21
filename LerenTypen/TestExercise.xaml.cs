using System;
using System.Timers;
using System.Windows.Controls;
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
        private int k = 0;
        private int i = 0;
        private int j = 0;
        private Line secondeWijzer;
        private Line minutenWijzer;
        private DispatcherTimer t1;


        public TestExercise()
        {
            InitializeComponent();
            t1 = new DispatcherTimer();
            t1.Interval = new TimeSpan(0,0,1);
            t1.Start();
            t1.Tick += StartTimer;

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
            minutenWijzer = new Line();
            minutenWijzer.Stroke = Brushes.Black;
            minutenWijzer.X1 = 50;
            minutenWijzer.X2 = 50;
            minutenWijzer.Y1 = 50;
            minutenWijzer.Y2 = 0;
            minutenWijzer.StrokeThickness = 2;
            clock.Children.Add(minutenWijzer);

            secondeWijzer = new Line();
            secondeWijzer.Stroke = Brushes.Red;
            secondeWijzer.X1 = 50;
            secondeWijzer.X2 = 50;
            secondeWijzer.Y1 = 50;
            secondeWijzer.Y2 = 10;
            secondeWijzer.StrokeThickness = 1;
            clock.Children.Add(secondeWijzer);

           
        }
       

        private void StartTimer(object sender, EventArgs e)
        {            
            k++;
            if (k.Equals(3))
            {
                t1.Tick -= StartTimer;
                t1.Tick += UpdateTimer;
                t1.Tick += UpdateCanvas;
            }

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

            secondeWijzer.RenderTransform = new RotateTransform(rotationS, 50, 50);
            if (i.Equals(0))
            {
                minutenWijzer.RenderTransform = new RotateTransform(rotationM, 50, 50);
            }
            
            
        }

        private void NextLineButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
