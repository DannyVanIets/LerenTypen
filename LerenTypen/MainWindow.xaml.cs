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
        public int Ingelogd { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            frame.Navigate(new HomePage(this));
        }

        private void HomePageButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(new HomePage(this), homePageButton);
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

        //In this method we will first check if the user is logged in, if that's the case it means they want to logout. We will first ask a confirmation, then update the property, make sure the loginPageButton texts changes and then put them on the homepage. If the user is not logged in, it will direct them to the login page.
        private void LoginPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (Ingelogd > 0)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Weet je zeker dat je wilt uitloggen?", "Uitloggen", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    Ingelogd = 0;
                    UpdateLoginText();
                    MessageBox.Show("U bent succesvol uitgelogd! U wordt nu doorgestuurd naar de homepagina.", "Succes");
                    ChangePage(new HomePage());
                }
            }
            else
            {
                ChangePage(new LoginPage(this), loginPageButton);
            }
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
        /// Changes the page to the specified page if this page is not 
        /// already open and updates the menu buttons accordingly
        /// </summary>
        /// <param name="pageToChangeTo"></param>
        public void ChangePage(Page pageToChangeTo)
        {
            if (frame.Content != pageToChangeTo)
            {
                frame.Navigate(pageToChangeTo);

                ToggleButton pageToggleButton = null;
                if (pageToChangeTo is HomePage)
                {
                    //pageToggleButton = homePageButton;
                }
                else if (pageToChangeTo is TestOverviewPage)
                {
                    pageToggleButton = testOverviewPageButton;
                }
                else if (pageToChangeTo is TrendingTestsPage)
                {
                    pageToggleButton = trendingTestsPageButton;
                }
                else if (pageToChangeTo is TrendingTestsPage)
                {
                    pageToggleButton = trendingTestsPageButton;
                }
                else if (pageToChangeTo is TipPage)
                {
                    pageToggleButton = tipPageButton;
                }
                else if (pageToChangeTo is LeaderboardPage)
                {
                    pageToggleButton = leaderboardPageButton;
                }
                else if (pageToChangeTo is LoginPage)
                {
                    pageToggleButton = loginPageButton;
                }

                if (pageToggleButton != null)
                {
                    SwitchMenuButtons(pageToggleButton);
                }
            }
        }

        /// <summary>
        /// Checks the specified button and unchecks all other buttons of the menu
        /// </summary>
        /// <param name="buttonToSwitchTo">The ToggleButton to check</param>
        private void SwitchMenuButtons(ToggleButton buttonToSwitchTo)
        {
            homePageButton.IsChecked = false;
            testOverviewPageButton.IsChecked = false;
            trendingTestsPageButton.IsChecked = false;
            tipPageButton.IsChecked = false;
            leaderboardPageButton.IsChecked = false;
            loginPageButton.IsChecked = false;

            buttonToSwitchTo.IsChecked = true;
        }

        //This method is used to change the text of the loginPageButton, used if you login and logout.
        public void UpdateLoginText()
        {
            if (Ingelogd > 0)
            {
                loginPageButton.Content = "Uitloggen";
            }
            else
            {
                loginPageButton.Content = "Inloggen/registeren";
            }
        }
    }
}
