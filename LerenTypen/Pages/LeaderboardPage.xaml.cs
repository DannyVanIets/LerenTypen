using LerenTypen.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace LerenTypen
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class LeaderboardPage : Page
    {
        //Maak alle listen voor info aan
        MainWindow MainWindow;
        List<Test> HardBoard;
        List<Test> MediumBoard;
        List<Test> EasyBoard;
        List<Test> CurrentContent = new List<Test>();
        public LeaderboardPage(MainWindow mainWindow)
        {
            InitializeComponent();
            MainWindow = mainWindow;
            //Make all boards
            HardBoard = new List<Test>();
            MediumBoard= new List<Test>();
            EasyBoard = new List<Test>();

           
            //Refresh and Load all info
            HardLeaderboardWords.ItemsSource = Controllers.LeaderboardController.LeaderboardHardTests(0);
            HardLeaderboardWords.Items.Refresh();

            AverageWordsboard.ItemsSource = Controllers.LeaderboardController.LeaderboardMediumTests(0);
            AverageWordsboard.Items.Refresh();


            EasyLeaderboardWords.ItemsSource = Controllers.LeaderboardController.LeaderboardEasyTests(0);
            EasyLeaderboardWords.Items.Refresh();
        }

        //Check all checkboxes if values are being changed or not.
        private void Tijdkeuze_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int Value = TimePick.SelectedIndex;
           if (HardLeaderboardWords != null)
            {
                HardLeaderboardWords.ItemsSource = Controllers.LeaderboardController.LeaderboardHardTests(Value);
                HardLeaderboardWords.Items.Refresh();
            }
        }
        
        private void Tijdkeuze2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int Value = TimePick2.SelectedIndex;
            if (AverageWordsboard != null)
            {
                AverageWordsboard.ItemsSource = Controllers.LeaderboardController.LeaderboardMediumTests(Value);
                AverageWordsboard.Items.Refresh();
            }
        }
        private void Tijdkeuze3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int Value = TimePick3.SelectedIndex;
            if (EasyLeaderboardWords != null)
            {
                EasyLeaderboardWords.ItemsSource = Controllers.LeaderboardController.LeaderboardEasyTests(Value);
                EasyLeaderboardWords.Items.Refresh();
            }
        }
    }
}