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
        private int i = 0;
        private int j = 0;
        private Line l1;
        private Line l4;


        public TestExercise()
        {
            InitializeComponent();
            DispatcherTimer t1 = new DispatcherTimer();
            t1.Interval = new TimeSpan(0,0,1);
            t1.Start();
            t1.Tick += UpdateTimer;
            t1.Tick += UpdateCanvas;

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
            l4 = new Line();
            l4.Stroke = Brushes.Black;
            l4.X1 = 50;
            l4.X2 = 50;
            l4.Y1 = 50;
            l4.Y2 = 0;
            l4.StrokeThickness = 2;
            clock.Children.Add(l4);



            l1 = new Line();
            l1.Stroke = Brushes.Red;
            l1.X1 = 50;
            l1.X2 = 50;
            l1.Y1 = 50;
            l1.Y2 = 10;
            l1.StrokeThickness = 1;
            clock.Children.Add(l1);

           
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

            l1.RenderTransform = new RotateTransform(rotationS, 50, 50);
            l4.RenderTransform = new RotateTransform(rotationM, 50, 50);
            
            
        }

        private void NextLineButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
