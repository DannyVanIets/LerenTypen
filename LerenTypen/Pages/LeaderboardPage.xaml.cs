using System.Windows;
using System.Windows.Controls;

namespace LerenTypen
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class LeaderboardPage : Page
    {
        MainWindow MainWindow;
        public LeaderboardPage(MainWindow mainWindow)
        {
            InitializeComponent();
            MainWindow = mainWindow;
        }


        private int DatePicked()
        {
            int Value = Tijdkeuze.SelectedIndex;
            if (Value == 0)
            {
                MessageBox.Show("Weekly");
            } else if (Value == 1)
            {
                MessageBox.Show("monthly");
            } else if (Value == 2)
            {
                MessageBox.Show("yearly");
            }
            else
            {
                MessageBox.Show("error");
                System.Console.WriteLine("error");
            }
            return Value;
        }

        //Check all checkboxes if values are being changed or not.
        private void Tijdkeuze_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePicked();
            
        }
        
        private void Tijdkeuze2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePicked();
        }

        private void Tijdkeuze3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePicked();
        }

        private void Tijdkeuze4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePicked();
        }

        private void Tijdkeuze5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void Tijdkeuze6_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePicked();
        }
    }
}
