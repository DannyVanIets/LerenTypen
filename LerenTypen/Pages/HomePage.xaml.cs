using LerenTypen.Controllers;
using LerenTypen.Models;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;

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
                loginRegisterButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                loginRegisterButton.Visibility = Visibility.Visible;
            }

            //List<Test> trendingTests = TestController.GetTrendingTestsNameAndID(10);
            List<Test> trendingTests = new List<Test>();
            if (trendingTests.Count > 0)
            {
                int counter = 1;
                foreach (Test test in trendingTests)
                {
                    test.Number = counter;
                    trendingTestsListView.Items.Add(test);
                    counter++;
                }
            }
            else
            {
                trendingTestsListView.Visibility = Visibility.Collapsed;
                noTrendingTestsLabel.Visibility = Visibility.Visible;
                moreTrendingTestsLink.IsEnabled = false;
            }
        }

        private void LoginRegisterButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.ChangePage(new LoginPage(mainWindow));
        }

        private void MoreTrendingTestsLink_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.ChangePage(new TestOverviewPage(mainWindow, true));
        }

        private void TrendingTestsListViewItem_Click(object sender, MouseButtonEventArgs e)
        {
            // Get test associated with the listview item
            Test test = ((ListViewItem)sender).Content as Test;

            mainWindow.ChangePage(new TestInfoPage(test.ID, mainWindow));
        }
    }
}