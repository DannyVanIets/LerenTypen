using System.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

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
        List<TestTable> TableContent;

        /// <summary>
        /// A list with the currently displayed data
        /// </summary>
        List<TestTable> CurrentContent = new List<TestTable>();

        /// <summary>
        /// A list with the searchresult data
        /// </summary>
        List<TestTable> SearchResult = new List<TestTable>();

        /// <summary>
        /// bool to tell the app if it has loaded for the first time
        /// </summary>
        bool isInitialized = false;

        /// <summary>
        /// number that corresponds to a filter
        /// </summary>
        int ActiveFilter = 0;

        /// <summary>
        /// Startvalue of the amount of words in the filter
        /// </summary>
        int StartValue = 0;

        /// <summary>
        /// Endvalue of the amount of words in the filter
        /// </summary>
        int EndValue = 999999;

        /// <summary>
        /// Combines startvalue and endValue
        /// </summary>
        int[] StartAndEnd = new int[2];

        private MainWindow MainWindow { get; set; }
        public TestOverviewPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.MainWindow = mainWindow;

            TableContent = new List<TestTable>();

            ////add the data to the datagrid and refresh to show

            TableContent = Database.GetAllTests();


            try
            {
                //TableCounter(TableContent);


                AllTestsOverview_DataGrid_AllTestsTable.ItemsSource = TableContent;

                AllTestsOverview_DataGrid_AllTestsTable.Items.Refresh();

                //Bool to prevent the select event/ToonAlles_event at startup app
                isInitialized = true;
                CurrentContent = TableContent;
            }
            catch (NullReferenceException nre)
            {

            }

        }
        /// <summary>
        /// Filter to show everything
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToonAlles_Clicker(object sender, System.Windows.RoutedEventArgs e)
        {
            if (isInitialized)
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
        /// Filters everything less than 50
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LessThan50_Clicker(object sender, System.Windows.RoutedEventArgs e)
        {

            List<TestTable> ItemsLessThan50 = new List<TestTable>();
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
            List<TestTable> ItemsBetween50And100 = new List<TestTable>();
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
        /// filters everything between 100 and 150
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Between100And150_Clicker(object sender, System.Windows.RoutedEventArgs e)
        {
            List<TestTable> ItemsBetween100And150 = new List<TestTable>();
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
        /// filters everything between 150 and 200
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Between150And200_Clicker(object sender, System.Windows.RoutedEventArgs e)
        {
            List<TestTable> ItemsBetween150And200 = new List<TestTable>();
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
        /// filters for items with more than 200 words
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoreThan200_Clicker(object sender, System.Windows.RoutedEventArgs e)
        {
            List<TestTable> ItemsMoreThan200 = new List<TestTable>();
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
                CurrentContent = TableContent;
                Filter(FindFilter(ActiveFilter)[0], FindFilter(ActiveFilter)[1]);
            }

            if (!AllTestsOverview_TextBox_Search.Text.Equals("Zoek gebruiker/toetsnaam") && !AllTestsOverview_TextBox_Search.Text.Equals(""))
            {
                CurrentContent = TableContent;
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
                if (item.AmountOfWords > startValue && item.AmountOfWords < endValue)
                {
                    FilterList.Add(item);
                }
            }
            AllTestsOverview_DataGrid_AllTestsTable.ItemsSource = FilterList;
            AllTestsOverview_DataGrid_AllTestsTable.Items.Refresh();
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
                    StartValue = 0;
                    EndValue = 99999;
                    StartAndEnd[0] = StartValue;
                    StartAndEnd[1] = EndValue;
                    return StartAndEnd;
                case 1:
                    StartValue = 0;
                    EndValue = 50;
                    StartAndEnd[0] = StartValue;
                    StartAndEnd[1] = EndValue;
                    return StartAndEnd;
                case 2:
                    StartValue = 49;
                    EndValue = 100;
                    StartAndEnd[0] = StartValue;
                    StartAndEnd[1] = EndValue;
                    return StartAndEnd;
                case 3:
                    StartValue = 99;
                    EndValue = 150;
                    StartAndEnd[0] = StartValue;
                    StartAndEnd[1] = EndValue;
                    return StartAndEnd;
                case 4:
                    StartValue = 149;
                    EndValue = 200;
                    StartAndEnd[0] = StartValue;
                    StartAndEnd[1] = EndValue;
                    return StartAndEnd;
                case 5:
                    StartValue = 199;
                    EndValue = 999999;
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
            MainWindow.ChangePage(new CreateTestPage(MainWindow));
        }

        /// <summary>
        /// button that filters tests only made by the user himself
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllTestsOverview_Button_ShowOwnTestOnly_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            AllTestsOverview_TextBox_Search.Text = Database.GetUserName(MainWindow.Ingelogd);


        }

        /// <summary>
        /// If checked, checkbox that shows tests previously made by the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllTestsOverview_CheckBox_MadeBefore_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            CurrentContent = Database.GetAllTestsAlreadyMade(MainWindow.Ingelogd);
            //TableCounter(CurrentContent);
            AllTestsOverview_DataGrid_AllTestsTable.ItemsSource = CurrentContent;
            AllTestsOverview_DataGrid_AllTestsTable.Items.Refresh();
        }

        /// <summary>
        /// Unchecks the box that only shows tests previously made by the user, therefore showing all the tests
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllTestsOverview_CheckBox_MadeBefore_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {

            AllTestsOverview_DataGrid_AllTestsTable.ItemsSource = TableContent;

            AllTestsOverview_DataGrid_AllTestsTable.Items.Refresh();
        }
    }
}
