using System.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LerenTypen
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class TestOverviewPage : Page
    {
        List<TestTable> TableContent;
        List<TestTable> CurrentContent = new List<TestTable>();
        bool isInitialized = false;
        public TestOverviewPage()
        {
            InitializeComponent();



            TableContent = new List<TestTable>();
            TableContent.Add(new TestTable(1, "BramsToets", 50, 0, 100, "makkelijk", "Bram"));
            TableContent.Add(new TestTable(2, "Danny heeft dit gemaakt", 2, 0, 151, "gemiddeld", "Bram"));
            TableContent.Add(new TestTable(3, "Tristan opdracht 3", 49, 0, 10, "makkelijk", "Danny"));
            TableContent.Add(new TestTable(4, "Mark oefententamen", 8, 0, 400, "moeilijk", "Tristan"));
            TableContent.Add(new TestTable(5, "Hugo opdracht 3", 99, 0, 94, "makkelijk", "Bram"));

            //ToonAlles_Clicker(this, new System.Windows.RoutedEventArgs() );
            AllTestsOverview_DataGrid_AllTestsTable.ItemsSource = TableContent;
            isInitialized = true;
            CurrentContent = TableContent;


        }

        private void LessThan50_Clicker(object sender, System.Windows.RoutedEventArgs e)
        {

            List<TestTable> ItemsLessThan50 = new List<TestTable>();
            foreach (var item in TableContent)
            {
                if (item.AmountOfWords < 50)
                {
                    ItemsLessThan50.Add(item);
                }
            }
            AllTestsOverview_DataGrid_AllTestsTable.ItemsSource = ItemsLessThan50;
            AllTestsOverview_DataGrid_AllTestsTable.Items.Refresh();

            CurrentContent = ItemsLessThan50;

        }

        private void ToonAlles_Clicker(object sender, System.Windows.RoutedEventArgs e)
        {
            if (isInitialized)
            {

                AllTestsOverview_DataGrid_AllTestsTable.ItemsSource = TableContent;
                AllTestsOverview_DataGrid_AllTestsTable.Items.Refresh();
                CurrentContent = TableContent;

            }
        }

        private void Between50And100_Clicker(object sender, System.Windows.RoutedEventArgs e)
        {
            List<TestTable> ItemsBetween50And100 = new List<TestTable>();
            foreach (var item in TableContent)
            {
                if (item.AmountOfWords > 49 && item.AmountOfWords < 100)
                {
                    ItemsBetween50And100.Add(item);
                }
            }
            AllTestsOverview_DataGrid_AllTestsTable.ItemsSource = ItemsBetween50And100;
            AllTestsOverview_DataGrid_AllTestsTable.Items.Refresh();
            CurrentContent = ItemsBetween50And100;

        }

        private void Between100And150_Clicker(object sender, System.Windows.RoutedEventArgs e)
        {
            List<TestTable> ItemsBetween100And150 = new List<TestTable>();
            foreach (var item in TableContent)
            {
                if (item.AmountOfWords > 99 && item.AmountOfWords < 150)
                {
                    ItemsBetween100And150.Add(item);
                }
            }
            AllTestsOverview_DataGrid_AllTestsTable.ItemsSource = ItemsBetween100And150;
            AllTestsOverview_DataGrid_AllTestsTable.Items.Refresh();
            CurrentContent = ItemsBetween100And150;

        }

        private void Between150And200_Clicker(object sender, System.Windows.RoutedEventArgs e)
        {
            List<TestTable> ItemsBetween150And200 = new List<TestTable>();
            foreach (var item in TableContent)
            {
                if (item.AmountOfWords > 149 && item.AmountOfWords < 200)
                {
                    ItemsBetween150And200.Add(item);
                }
            }

            AllTestsOverview_DataGrid_AllTestsTable.ItemsSource = ItemsBetween150And200;
            AllTestsOverview_DataGrid_AllTestsTable.Items.Refresh();
            CurrentContent = ItemsBetween150And200;

        }

        private void MoreThan200_Clicker(object sender, System.Windows.RoutedEventArgs e)
        {
            List<TestTable> ItemsMoreThan200 = new List<TestTable>();
            foreach (var item in TableContent)
            {
                if (item.AmountOfWords > 200)
                {
                    ItemsMoreThan200.Add(item);
                }
            }
            AllTestsOverview_DataGrid_AllTestsTable.ItemsSource = ItemsMoreThan200;
            AllTestsOverview_DataGrid_AllTestsTable.Items.Refresh();
            CurrentContent = ItemsMoreThan200;

        }

        private void Search_Event(object sender, TextChangedEventArgs e)
        {

            if (!AllTestsOverview_TextBox_Search.Text.Equals("Zoek gebruiker/toetsnaam"))
            {
                string searchterm = AllTestsOverview_TextBox_Search.Text;
                var SearchResult = from t in CurrentContent
                                   where t.WPFName.IndexOf(searchterm, StringComparison.OrdinalIgnoreCase) >= 0 || t.Uploader.IndexOf(searchterm, StringComparison.OrdinalIgnoreCase) >= 0
                                   select t;
                AllTestsOverview_DataGrid_AllTestsTable.ItemsSource = SearchResult;
                AllTestsOverview_DataGrid_AllTestsTable.Items.Refresh();


            }
        }


        private void Search_Remove_Event(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (AllTestsOverview_TextBox_Search.Text.Equals("Zoek gebruiker/toetsnaam"))
            {
                AllTestsOverview_TextBox_Search.Text = "";
            }
        }
    }
}
