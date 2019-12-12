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
        List<Test> HardBoardWords1;
        List<Test> HardBoardPerc1;
        List<Test> MediumBoardWords1;
        List<Test> MediumBoardPerc1;
        List<Test> EasyBoardWords1;
        List<Test> EasyBoardPerc1;
        List<Test> CurrentContent = new List<Test>();
        public LeaderboardPage(MainWindow mainWindow)
        {
            InitializeComponent();
            MainWindow = mainWindow;
            //Make all boards
            HardBoardPerc1= new List<Test>();
            HardBoardWords1 = new List<Test>();

            MediumBoardPerc1 = new List<Test>();
            MediumBoardWords1 = new List<Test>();

            EasyBoardPerc1 = new List<Test>();
            EasyBoardWords1 = new List<Test>();

           
            //Refresh and Load all info
            HardLeaderboardWords.ItemsSource = Controllers.LeaderboardController.LeaderboardHardTestsWords(0);
            HardLeaderboardWords.Items.Refresh();

            HardLeaderboardPerc.ItemsSource = Controllers.LeaderboardController.LeaderboardHardTestsWordsPerc(0);
            HardLeaderboardPerc.Items.Refresh();

            AverageLeaderboardWords.ItemsSource = Controllers.LeaderboardController.LeaderboardMediumTestsWords(0);
            AverageLeaderboardWords.Items.Refresh();

            AverageLeaderboardperc.ItemsSource = Controllers.LeaderboardController.LeaderboardMediumTestsPerc(0);
            AverageLeaderboardperc.Items.Refresh();

            EasyLeaderboardwords.ItemsSource = Controllers.LeaderboardController.LeaderboardEasyTestsWords(0);
            EasyLeaderboardwords.Items.Refresh();

            EasyeLeaderboardperc.ItemsSource = Controllers.LeaderboardController.LeaderboardEasyTestsPerc(0);
            EasyeLeaderboardperc.Items.Refresh();
        }

        //Check all checkboxes if values are being changed or not.
        private void Tijdkeuze_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int Value = TimePick.SelectedIndex;
           if (HardLeaderboardWords != null)
            {
                HardLeaderboardWords.ItemsSource = Controllers.LeaderboardController.LeaderboardHardTestsWords(Value);
                HardLeaderboardWords.Items.Refresh();
            }
        }
        
        private void Tijdkeuze2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int Value = TimePick2.SelectedIndex;
            if (HardLeaderboardPerc != null)
            {
                HardLeaderboardPerc.ItemsSource = Controllers.LeaderboardController.LeaderboardHardTestsWordsPerc(Value);
                HardLeaderboardPerc.Items.Refresh();
            }
        }
        private void Tijdkeuze3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int Value = TimePick3.SelectedIndex;
            if (AverageLeaderboardWords != null)
            {
                AverageLeaderboardWords.ItemsSource = Controllers.LeaderboardController.LeaderboardMediumTestsWords(Value);
                AverageLeaderboardWords.Items.Refresh();
            }
        }
        private void Tijdkeuze4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int Value = TimePick4.SelectedIndex;
            if (AverageLeaderboardperc != null)
            {
                AverageLeaderboardperc.ItemsSource = Controllers.LeaderboardController.LeaderboardMediumTestsPerc(Value);
                AverageLeaderboardperc.Items.Refresh();
            }
        }
        private void Tijdkeuze5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int Value = TimePick5.SelectedIndex;
            if (EasyLeaderboardwords != null)
            {
                EasyLeaderboardwords.ItemsSource = Controllers.LeaderboardController.LeaderboardEasyTestsWords(Value);
                EasyLeaderboardwords.Items.Refresh();
            }
        }
        private void Tijdkeuze6_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int Value = TimePick6.SelectedIndex;
            if (EasyeLeaderboardperc != null)
            {
                EasyeLeaderboardperc.ItemsSource = Controllers.LeaderboardController.LeaderboardEasyTestsPerc(Value);
                EasyeLeaderboardperc.Items.Refresh();
            }
        }
    }
}