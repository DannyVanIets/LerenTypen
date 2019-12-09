using LerenTypen.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Documents;

namespace LerenTypen
{
    /// <summary>
    /// Page for the overview of all the tests
    /// </summary>
    public partial class TestOverviewPage : Page
    {
        /// <summary>
        /// A list with all the data
        /// </summary>
        private List<TestTable> TableContent;

        /// <summary>
        /// A list with the currently displayed data
        /// </summary>
        private List<TestTable> CurrentContent = new List<TestTable>();

        /// <summary>
        /// A list with the searchresult data
        /// </summary>
        private List<TestTable> SearchResult = new List<TestTable>();

        /// <summary>
        /// Bool to tell the app if it has loaded for the first time
        /// </summary>
        private bool IsPageInitialized = false;

        /// <summary>
        /// Number that corresponds to a filter
        /// </summary>
        private int ActiveFilter = 0;

        /// <summary>
        /// Startvalue of the amount of words in the filter
        /// </summary>
        private int StartValue = 0;

        /// <summary>
        /// Endvalue of the amount of words in the filter
        /// </summary>
        private int EndValue = 999999;

        /// <summary>
        /// Combines startvalue and endValue
        /// </summary>
        private int[] StartAndEnd = new int[2];

        private MainWindow MainWindow { get; set; }
        public TestOverviewPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.MainWindow = mainWindow;
            TableContent = new List<TestTable>();

            // Hide some buttons if a user hasnt logged in yet
            if (MainWindow.Ingelogd == 0)
            {
                AllTestsOverview_Button_MakeOwnTest.Visibility = System.Windows.Visibility.Hidden;
                AllTestsOverview_Button_ShowOwnTestOnly.Visibility = System.Windows.Visibility.Hidden;
                AllTestsOverview_CheckBox_MadeBefore.Visibility = System.Windows.Visibility.Hidden;
            }

            // Add the data to the ListView and refresh to show
            try
            {
                TableContent = TestController.GetAllTests();

                // Bool to prevent the select event/ToonAlles_event at startup app
                IsPageInitialized = true;
                CurrentContent = TableContent;
                ActiveFilter = 0;
                Filter(FindFilter(ActiveFilter)[0], FindFilter(ActiveFilter)[1]);
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Er zijn geen toetsen");
            }
        }

        /// <summary>
        /// Filter to show everything
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToonAlles_Clicker(object sender, System.Windows.RoutedEventArgs e)
        {
            if (IsPageInitialized)
            {
                if (AllTestsOverview_TextBox_Search.Text.Equals("Zoek gebruiker/toetsnaam") || AllTestsOverview_TextBox_Search.Text.Equals(""))
                {
                    CurrentContent = TableContent;
                }
                else
                {
                    CurrentContent = SearchResult;
                }
                ActiveFilter = 0;
                Filter(FindFilter(ActiveFilter)[0], FindFilter(ActiveFilter)[1]);
            }
        }

        /// <summary>
        /// Filters everything with less than 50 words
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LessThan50_Clicker(object sender, System.Windows.RoutedEventArgs e)
        {
            if (AllTestsOverview_TextBox_Search.Text.Equals("Zoek gebruiker/toetsnaam") || AllTestsOverview_TextBox_Search.Text.Equals(""))
            {
                CurrentContent = TableContent;
            }
            else
            {
                CurrentContent = SearchResult;
            }
            ActiveFilter = 1;
            Filter(FindFilter(ActiveFilter)[0], FindFilter(ActiveFilter)[1]);
        }

        /// <summary>
        /// Filters everything between 50 and 100
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Between50And100_Clicker(object sender, System.Windows.RoutedEventArgs e)
        {
            if (AllTestsOverview_TextBox_Search.Text.Equals("Zoek gebruiker/toetsnaam") || AllTestsOverview_TextBox_Search.Text.Equals(""))
            {
                CurrentContent = TableContent;
            }
            else
            {
                CurrentContent = SearchResult;
            }
            ActiveFilter = 2;
            Filter(FindFilter(ActiveFilter)[0], FindFilter(ActiveFilter)[1]);
        }

        /// <summary>
        /// Filters everything between 100 and 150
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Between100And150_Clicker(object sender, System.Windows.RoutedEventArgs e)
        {
            if (AllTestsOverview_TextBox_Search.Text.Equals("Zoek gebruiker/toetsnaam") || AllTestsOverview_TextBox_Search.Text.Equals(""))
            {
                CurrentContent = TableContent;
            }
            else
            {
                CurrentContent = SearchResult;
            }
            ActiveFilter = 3;
            Filter(FindFilter(ActiveFilter)[0], FindFilter(ActiveFilter)[1]);
        }

        /// <summary>
        /// Filters everything between 150 and 200
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Between150And200_Clicker(object sender, System.Windows.RoutedEventArgs e)
        {
            if (AllTestsOverview_TextBox_Search.Text.Equals("Zoek gebruiker/toetsnaam") || AllTestsOverview_TextBox_Search.Text.Equals(""))
            {
                CurrentContent = TableContent;
            }
            else
            {
                CurrentContent = SearchResult;
            }
            ActiveFilter = 4;
            Filter(FindFilter(ActiveFilter)[0], FindFilter(ActiveFilter)[1]);
        }

        /// <summary>
        /// Filters for items with more than 200 words
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoreThan200_Clicker(object sender, System.Windows.RoutedEventArgs e)
        {
            if (AllTestsOverview_TextBox_Search.Text.Equals("Zoek gebruiker/toetsnaam") || AllTestsOverview_TextBox_Search.Text.Equals(""))
            {
                CurrentContent = TableContent;
            }
            else
            {
                CurrentContent = SearchResult;
            }
            ActiveFilter = 5;
            Filter(FindFilter(ActiveFilter)[0], FindFilter(ActiveFilter)[1]);
        }

        /// <summary>
        /// Function that happens when the user uses the searchbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Search_Event(object sender, TextChangedEventArgs e)
        {
            if (AllTestsOverview_TextBox_Search.Text.Equals(""))
            {
                if (AllTestsOverview_CheckBox_MadeBefore.IsChecked.Value)
                {
                    CurrentContent = TestController.GetAllTestsAlreadyMade(MainWindow.Ingelogd);
                }
                else
                {
                    CurrentContent = TableContent;
                }
                Filter(FindFilter(ActiveFilter)[0], FindFilter(ActiveFilter)[1]);
            }
            if (!AllTestsOverview_TextBox_Search.Text.Equals("Zoek gebruiker/toetsnaam") && !AllTestsOverview_TextBox_Search.Text.Equals(""))
            {
                if (AllTestsOverview_CheckBox_MadeBefore.IsChecked.Value)
                {
                    CurrentContent = TestController.GetAllTestsAlreadyMade(MainWindow.Ingelogd);
                }
                else
                {
                    CurrentContent = TableContent;
                }
                //CurrentContent = TableContent;
                string searchterm = AllTestsOverview_TextBox_Search.Text;
                SearchResult = (from t in CurrentContent
                                where t.WPFName.IndexOf(searchterm, StringComparison.OrdinalIgnoreCase) >= 0 || t.Uploader.IndexOf(searchterm, StringComparison.OrdinalIgnoreCase) >= 0
                                select t).ToList();

                CurrentContent = SearchResult;
                Filter(FindFilter(ActiveFilter)[0], FindFilter(ActiveFilter)[1]);

            }
        }

        /// <summary>
        /// Function that removes the standard text the first time the user clicks the searchbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Search_Remove_Event(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (AllTestsOverview_TextBox_Search.Text.Equals("Zoek gebruiker/toetsnaam"))
            {
                AllTestsOverview_TextBox_Search.Text = "";
            }
        }

        /// <summary>
        /// Function that filters based on the parameters and displays the results
        /// </summary>
        /// <param name="startValue"></param>
        /// <param name="endValue"></param>
        private void Filter(int startValue, int endValue)
        {
            List<TestTable> FilterList = new List<TestTable>();

            foreach (var item in CurrentContent)
            {
                if (item.AmountOfWords >= startValue && item.AmountOfWords <= endValue)
                {
                    FilterList.Add(item);
                }
            }
            AllTestsOverview_ListView_AllTestsTable.ItemsSource = FilterList;
            AllTestsOverview_ListView_AllTestsTable.Items.Refresh();
        }

        /// <summary>
        /// Function that finds the corresponding startvalue/endvalue based on which active filternumber is given and returns these values.
        /// </summary>
        /// <param name="activeFilter"></param>
        /// <returns></returns>
        private int[] FindFilter(int activeFilter)
        {
            switch (activeFilter)
            {
                case 0:
                    StartValue = 1;
                    EndValue = Int32.MaxValue;
                    StartAndEnd[0] = StartValue;
                    StartAndEnd[1] = EndValue;
                    return StartAndEnd;
                case 1:
                    StartValue = 1;
                    EndValue = 49;
                    StartAndEnd[0] = StartValue;
                    StartAndEnd[1] = EndValue;
                    return StartAndEnd;
                case 2:
                    StartValue = 50;
                    EndValue = 99;
                    StartAndEnd[0] = StartValue;
                    StartAndEnd[1] = EndValue;
                    return StartAndEnd;
                case 3:
                    StartValue = 100;
                    EndValue = 149;
                    StartAndEnd[0] = StartValue;
                    StartAndEnd[1] = EndValue;
                    return StartAndEnd;
                case 4:
                    StartValue = 150;
                    EndValue = 199;
                    StartAndEnd[0] = StartValue;
                    StartAndEnd[1] = EndValue;
                    return StartAndEnd;
                case 5:
                    StartValue = 200;
                    EndValue = Int32.MaxValue;
                    StartAndEnd[0] = StartValue;
                    StartAndEnd[1] = EndValue;
                    return StartAndEnd;
                default:
                    return null;
            }
        }

        /// <summary>
        /// Button to make a new test
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllTestsOverview_Button_MakeOwnTest_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (MainWindow.Ingelogd == 0)
            {
                Console.WriteLine("User niet ingelogd");
            }
            else
            {
                MainWindow.ChangePage(new CreateTestPage(MainWindow));
            }
        }

        /// <summary>
        /// Button that filters tests only made by the user himself
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllTestsOverview_Button_ShowOwnTestOnly_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (MainWindow.Ingelogd != 0)
            {
                MainWindow.ChangePage(new AllMyTestsOverviewPage(MainWindow));
            }
        }

        /// <summary>
        /// If checked, checkbox that shows tests previously made by the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllTestsOverview_CheckBox_MadeBefore_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            if (MainWindow.Ingelogd == 0)
            {
                Console.WriteLine("User niet ingelogd");
            }
            else
            {
                CurrentContent = TestController.GetAllTestsAlreadyMade(MainWindow.Ingelogd);
                if (!AllTestsOverview_TextBox_Search.Text.Equals("Zoek gebruiker/toetsnaam") && !AllTestsOverview_TextBox_Search.Text.Equals(""))
                {
                    string searchterm = AllTestsOverview_TextBox_Search.Text;
                    SearchResult = (from t in CurrentContent
                                    where t.WPFName.IndexOf(searchterm, StringComparison.OrdinalIgnoreCase) >= 0 || t.Uploader.IndexOf(searchterm, StringComparison.OrdinalIgnoreCase) >= 0
                                    select t).ToList();

                    CurrentContent = SearchResult;
                    Filter(FindFilter(ActiveFilter)[0], FindFilter(ActiveFilter)[1]);
                }
                else
                {
                    AllTestsOverview_ListView_AllTestsTable.ItemsSource = CurrentContent;
                    AllTestsOverview_ListView_AllTestsTable.Items.Refresh();
                }
            }
        }

        /// <summary>
        /// Unchecks the box that only shows tests previously made by the user, therefore showing all the tests
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllTestsOverview_CheckBox_MadeBefore_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            if (MainWindow.Ingelogd == 0)
            {
                Console.WriteLine("User niet ingelogd");
            }
            else
            {
                AllTestsOverview_ListView_AllTestsTable.ItemsSource = TableContent;
                CurrentContent = TableContent;
                //CurrentContent = TestController.GetAllTestsAlreadyMade(MainWindow.Ingelogd);
                if (!AllTestsOverview_TextBox_Search.Text.Equals("Zoek gebruiker/toetsnaam") && !AllTestsOverview_TextBox_Search.Text.Equals(""))
                {
                    string searchterm = AllTestsOverview_TextBox_Search.Text;
                    SearchResult = (from t in CurrentContent
                                    where t.WPFName.IndexOf(searchterm, StringComparison.OrdinalIgnoreCase) >= 0 || t.Uploader.IndexOf(searchterm, StringComparison.OrdinalIgnoreCase) >= 0
                                    select t).ToList();

                    CurrentContent = SearchResult;
                    Filter(FindFilter(ActiveFilter)[0], FindFilter(ActiveFilter)[1]);
                }
                else
                {
                    AllTestsOverview_ListView_AllTestsTable.ItemsSource = TableContent;
                    AllTestsOverview_ListView_AllTestsTable.Items.Refresh();
                }
            }
        }

        /// <summary>
        /// Hyperlink so a user can click on a testname to go to its testinformation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LV_AllTestOverview_Hyperlink_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Hyperlink link = (Hyperlink)sender;
            int id = Convert.ToInt32(link.Tag);
            MainWindow.ChangePage(new TestInfoPage(id, MainWindow));
        }

        /// <summary>
        /// Hyperlink that sends a user to the corresponding userPage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DG_ATO_Hyperlink_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Hyperlink link = (Hyperlink)sender;
            string id = link.Tag.ToString();
            //System.Windows.MessageBox.Show(id);

        }
    }
}
