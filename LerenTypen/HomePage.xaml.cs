using System.Collections.Generic;
using System.Windows.Controls;

namespace LerenTypen
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        private MainWindow mainWindow;

        public HomePage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;  
            
            if (mainWindow.Ingelogd > 0)
            {
                loginRegisterButton.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                loginRegisterButton.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void LoginRegisterButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            mainWindow.ChangePage(new LoginPage(mainWindow));
        }

        private void MoreTrendingTestsLink_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            mainWindow.ChangePage(new TrendingTestsPage());
        }

        public HomePage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;

        }
    }
}