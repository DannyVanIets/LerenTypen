using System.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace LerenTypen
{
    /// <summary>
    /// Page for the overview of all the tests
    /// </summary>
    public partial class AllMyTestsOverviewPage : Page
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
        public AllMyTestsOverviewPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.MainWindow = mainWindow;

            TableContent = new List<TestTable>();

            ////add the data to the datagrid and refresh to show

            try
            {
                TableContent = Database.GetAllTestswithIsPrivate();
                
                //Good version
                //string searchterm = Database.GetUserName(MainWindow.Ingelogd);
                //testversion
                string searchterm = Database.GetUserName(4);
                SearchResult = (from t in TableContent
                                where t.Uploader.IndexOf(searchterm, StringComparison.OrdinalIgnoreCase) >= 0
                                select t).ToList();
                MessageBox.Show(searchterm);
                TableContent = SearchResult;

                //TableCounter(TableContent);


                AllMyTestsOverviewPage_DataGrid_AllTestsTable.ItemsSource = SearchResult;

                AllMyTestsOverviewPage_DataGrid_AllTestsTable.Items.Refresh();

                //Bool to prevent the select event/ToonAlles_event at startup app
                isInitialized = true;
                CurrentContent = SearchResult;
            }
            catch (Exception)
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
            if (isInitialized)
            {
                if (AllMyTestsOverviewPage_TextBox_Search.Text.Equals("Zoek gebruiker/toetsnaam") || AllMyTestsOverviewPage_TextBox_Search.Text.Equals(""))
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
            if (AllMyTestsOverviewPage_TextBox_Search.Text.Equals("Zoek gebruiker/toetsnaam") || AllMyTestsOverviewPage_TextBox_Search.Text.Equals(""))
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
            if (AllMyTestsOverviewPage_TextBox_Search.Text.Equals("Zoek gebruiker/toetsnaam") || AllMyTestsOverviewPage_TextBox_Search.Text.Equals(""))
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
            if (AllMyTestsOverviewPage_TextBox_Search.Text.Equals("Zoek gebruiker/toetsnaam") || AllMyTestsOverviewPage_TextBox_Search.Text.Equals(""))
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
            if (AllMyTestsOverviewPage_TextBox_Search.Text.Equals("Zoek gebruiker/toetsnaam") || AllMyTestsOverviewPage_TextBox_Search.Text.Equals(""))
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
            if (AllMyTestsOverviewPage_TextBox_Search.Text.Equals("Zoek gebruiker/toetsnaam") || AllMyTestsOverviewPage_TextBox_Search.Text.Equals(""))
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
            if (AllMyTestsOverviewPage_TextBox_Search.Text.Equals(""))
            {
                CurrentContent = TableContent;
                Filter(FindFilter(ActiveFilter)[0], FindFilter(ActiveFilter)[1]);
            }

            if (!AllMyTestsOverviewPage_TextBox_Search.Text.Equals("Zoek toetsnaam") && !AllMyTestsOverviewPage_TextBox_Search.Text.Equals(""))
            {
                CurrentContent = TableContent;
                string searchterm = AllMyTestsOverviewPage_TextBox_Search.Text;
                SearchResult = (from t in CurrentContent
                                where t.WPFName.IndexOf(searchterm, StringComparison.OrdinalIgnoreCase) >= 0
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
            if (AllMyTestsOverviewPage_TextBox_Search.Text.Equals("Zoek toetsnaam"))
            {
                AllMyTestsOverviewPage_TextBox_Search.Text = "";
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
            AllMyTestsOverviewPage_DataGrid_AllTestsTable.ItemsSource = FilterList;
            AllMyTestsOverviewPage_DataGrid_AllTestsTable.Items.Refresh();
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
        private void AllMyTestsOverviewPage_Button_MakeOwnTest_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (MainWindow.Ingelogd == 0)
            {
                Console.WriteLine("User niet ingelogd");
            }
            else
            {
                MessageBox.Show("Gaat naar createTestPage");
                //MainWindow.ChangePage(new CreateTestPage(MainWindow));
            }
        }

        /// <summary>
        /// button that filters tests only made by the user himself
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllMyTestsOverviewPage_Button_ShowOwnTestOnly_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (MainWindow.Ingelogd == 0)
            {
                Console.WriteLine("User niet ingelogd");
            }
            else
            {
                AllMyTestsOverviewPage_TextBox_Search.Text = $"User: {Database.GetUserName(MainWindow.Ingelogd)}";
            }

        }

        /// <summary>
        /// If checked, checkbox that shows tests previously made by the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllMyTestsOverviewPage_CheckBox_MadeBefore_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            if (MainWindow.Ingelogd == 0)
            {
                Console.WriteLine("User niet ingelogd");
            }
            else
            {
                CurrentContent = Database.GetAllTestsAlreadyMade(MainWindow.Ingelogd);
                //TableCounter(CurrentContent);
                AllMyTestsOverviewPage_DataGrid_AllTestsTable.ItemsSource = CurrentContent;
                AllMyTestsOverviewPage_DataGrid_AllTestsTable.Items.Refresh();
            }
        }

        /// <summary>
        /// Unchecks the box that only shows tests previously made by the user, therefore showing all the tests
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllMyTestsOverviewPage_CheckBox_MadeBefore_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {

            AllMyTestsOverviewPage_DataGrid_AllTestsTable.ItemsSource = TableContent;

            AllMyTestsOverviewPage_DataGrid_AllTestsTable.Items.Refresh();
        }

        private void DG_AllMyTestsOverviewPage_Hyperlink_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            TextBlock textBlock = (TextBlock)sender;
            string id = textBlock.Tag.ToString();
            System.Windows.MessageBox.Show(id);
        }

        private void DG_ATO_Hyperlink_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            TextBlock textBlock = (TextBlock)sender;
            string id = textBlock.Tag.ToString();
            System.Windows.MessageBox.Show(id);

        }
        private void DG_ATO_Edit_Hyperlink_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            TextBlock textBlock = (TextBlock)sender;
            string id = textBlock.Tag.ToString();
            System.Windows.MessageBox.Show(id);
        }
        private void DG_ATO_Delete_Hyperlink_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            TextBlock textBlock = (TextBlock)sender;
            string id = textBlock.Tag.ToString();
            System.Windows.MessageBox.Show(id);
        }

        private void DG_Checkbox_Check(object sender, RoutedEventArgs e)
        {
            Database.UpdateToPublic(MainWindow.Ingelogd);
        }
    }
}
