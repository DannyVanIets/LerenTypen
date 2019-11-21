using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace LerenTypen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TestOverviewPageButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(new TestOverviewPage(), testOverviewPageButton);
        }

        private void TrendingTestsPageButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(new TrendingTestsPage(), trendingTestsPageButton);
        }

        private void TipPageButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(new TipPage(), tipPageButton);
        }

        private void LeaderboardPageButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(new LeaderboardPage(), leaderboardPageButton);
        }
     
        private void LoginPageButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(new LoginPage(), loginPageButton);
        }

        /// <summary>
        /// Changes the page to the specified page if this page is not 
        /// already open and updates the menu buttons accordingly
        /// </summary>
        /// <param name="pageToChangeTo"></param>
        /// <param name="pageToggleButton"></param>
        private void ChangePage(Page pageToChangeTo, ToggleButton pageToggleButton)
        {
            if (frame.Content != pageToChangeTo)
            {
                frame.Navigate(pageToChangeTo);
                SwitchMenuButtons(pageToggleButton);
            }           
        }

        /// <summary>
        /// Checks the specified button and unchecks all other buttons of the menu
        /// </summary>
        /// <param name="buttonToSwitchTo">The ToggleButton to check</param>
        private void SwitchMenuButtons(ToggleButton buttonToSwitchTo)
        {
            testOverviewPageButton.IsChecked = false;
            trendingTestsPageButton.IsChecked = false;
            tipPageButton.IsChecked = false;
            leaderboardPageButton.IsChecked = false;
            loginPageButton.IsChecked = false;

            buttonToSwitchTo.IsChecked = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(new TestExercise(), Test);
        }
    }
}
