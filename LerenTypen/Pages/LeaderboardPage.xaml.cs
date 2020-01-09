using LerenTypen.Controllers;
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
            HardLeaderboardWords.ItemsSource = LeaderboardController.GetHardTests(0);
            HardLeaderboardWords.Items.Refresh();

            AverageWordsboard.ItemsSource = LeaderboardController.GetMediumTests(0);
            AverageWordsboard.Items.Refresh();

            EasyLeaderboardWords.ItemsSource = LeaderboardController.GetEasyTests(0);
            EasyLeaderboardWords.Items.Refresh();
        }

        //Check all checkboxes if values are being changed or not.
        private void TimePick_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int value = TimePick.SelectedIndex;
            if (HardLeaderboardWords != null)
            {
                HardLeaderboardWords.Items.Refresh();
                HardLeaderboardWords.ItemsSource = LeaderboardController.GetHardTests(value);
                HardLeaderboardWords.Items.Refresh();
            }
        }
        
        private void TimePick2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int value = TimePick2.SelectedIndex;
            if (AverageWordsboard != null)
            {
                AverageWordsboard.ItemsSource = LeaderboardController.GetMediumTests(value);
                AverageWordsboard.Items.Refresh();
            }
        }
        private void TimePick3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int Value = TimePick3.SelectedIndex;
            if (EasyLeaderboardWords != null)
            {
                EasyLeaderboardWords.ItemsSource = LeaderboardController.GetEasyTests(Value);
                EasyLeaderboardWords.Items.Refresh();
            }
        }
    }
}