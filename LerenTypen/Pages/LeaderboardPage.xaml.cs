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
            MoeilijkeLeaderboardWords.ItemsSource = Controllers.LeaderboardController.LeaderboardHardTestsWords(0);
            MoeilijkeLeaderboardWords.Items.Refresh();

            MoeilijkeLeaderboardPerc.ItemsSource = Controllers.LeaderboardController.LeaderboardHardTestsWordsPerc(0);
            MoeilijkeLeaderboardPerc.Items.Refresh();

            MiddelLeaderboardWords.ItemsSource = Controllers.LeaderboardController.LeaderboardMediumTestsWords(0);
            MiddelLeaderboardWords.Items.Refresh();

            MiddelLeaderboardperc.ItemsSource = Controllers.LeaderboardController.LeaderboardMediumTestsPerc(0);
            MiddelLeaderboardperc.Items.Refresh();

            EasyLeaderboardwords.ItemsSource = Controllers.LeaderboardController.LeaderboardEasyTestsWords(0);
            EasyLeaderboardwords.Items.Refresh();

            EasyeLeaderboardperc.ItemsSource = Controllers.LeaderboardController.LeaderboardEasyTestsPerc(0);
            EasyeLeaderboardperc.Items.Refresh();
        }

        //Check all checkboxes if values are being changed or not.
        private void Tijdkeuze_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int Value = Tijdkeuze.SelectedIndex;
           if (MoeilijkeLeaderboardWords != null)
            {
                MoeilijkeLeaderboardWords.ItemsSource = Controllers.LeaderboardController.LeaderboardHardTestsWords(Value);
                MoeilijkeLeaderboardWords.Items.Refresh();
            }
        }
        
        private void Tijdkeuze2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int Value = Tijdkeuze2.SelectedIndex;
            if (MoeilijkeLeaderboardPerc != null)
            {
                MoeilijkeLeaderboardPerc.ItemsSource = Controllers.LeaderboardController.LeaderboardHardTestsWordsPerc(Value);
                MoeilijkeLeaderboardPerc.Items.Refresh();
            }
        }
        private void Tijdkeuze3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int Value = Tijdkeuze3.SelectedIndex;
            if (MiddelLeaderboardWords != null)
            {
                MiddelLeaderboardWords.ItemsSource = Controllers.LeaderboardController.LeaderboardMediumTestsWords(Value);
                MiddelLeaderboardWords.Items.Refresh();
            }
        }
        private void Tijdkeuze4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int Value = Tijdkeuze4.SelectedIndex;
            if (MiddelLeaderboardperc != null)
            {
                MiddelLeaderboardperc.ItemsSource = Controllers.LeaderboardController.LeaderboardMediumTestsPerc(Value);
                MiddelLeaderboardperc.Items.Refresh();
            }
        }
        private void Tijdkeuze5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int Value = Tijdkeuze5.SelectedIndex;
            if (EasyLeaderboardwords != null)
            {
                EasyLeaderboardwords.ItemsSource = Controllers.LeaderboardController.LeaderboardEasyTestsWords(Value);
                EasyLeaderboardwords.Items.Refresh();
            }
        }
        private void Tijdkeuze6_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int Value = Tijdkeuze6.SelectedIndex;
            if (EasyeLeaderboardperc != null)
            {
                EasyeLeaderboardperc.ItemsSource = Controllers.LeaderboardController.LeaderboardEasyTestsPerc(Value);
                EasyeLeaderboardperc.Items.Refresh();
            }
        }
    }
}