using LerenTypen.Controllers;
using LerenTypen.Models;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Documents;

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

            List<Test> trendingTests = TestController.GetTrendingTestsNameAndID(10);
            int counter = 1;
            foreach (Test test in trendingTests)
            {
                test.Number = counter;
                trendingTestsListView.Items.Add(test);
                counter++;
            }
        }

        private void LoginRegisterButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            mainWindow.ChangePage(new LoginPage(mainWindow));
        }

        private void MoreTrendingTestsLink_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            mainWindow.ChangePage(new TestOverviewPage(mainWindow, true));
        }

        private void TrendingTestsListViewItem_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Get test associated with the listview item
            Test test = ((ListViewItem)sender).Content as Test;

            mainWindow.ChangePage(new TestInfoPage(test.ID, mainWindow));
        }
    }
}