using LerenTypen.Controllers;
using LerenTypen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace LerenTypen
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class AllUsersPage : Page
    {
        List<UserTable> usercontent;
        List<UserTable> CurrentContent = new List<UserTable>();
        private List<UserTable> SearchResult = new List<UserTable>();
        public bool IsAdmin = false;
        public MainWindow Mainwindow { get; set; }

        public AllUsersPage(MainWindow mainwindow)
        {
            InitializeComponent();
            this.Mainwindow = mainwindow;
            usercontent = new List<UserTable>();
            //Info loaded in from database
            usercontent = AccountController.GetAllUsers();
            DGV1.ItemsSource = usercontent;
            DGV1.Items.Refresh();
            CurrentContent = usercontent;
        }

        // This gives UserID when Edit is clicked
        private void DG_Hyperlink_click(object sender, System.Windows.RoutedEventArgs e)
        {
            TextBlock textBlock = (TextBlock)sender;
            UserTable UserTable = (UserTable)textBlock.Tag;
            string id = UserTable.Accountnumber.ToString();
            string usertype = UserTable.UserTypeID.ToString();
            AccountController.GetAccountNamesAndBirthdate(int.Parse(id));
            var newWindow = new AdminEditAccountWindow(int.Parse(id), int.Parse(usertype));
            newWindow.ShowDialog();
            Mainwindow.frame.Navigate(new AllUsersPage(Mainwindow));
        }

        private void Search_Event(object sender, TextChangedEventArgs e)
        {
            if (Search_Username_Account.Text.Equals(""))
            {
                CurrentContent = usercontent;
                DGV1.ItemsSource = CurrentContent;
                DGV1.Items.Refresh();
            }
            if (!Search_Username_Account.Text.Equals(""))
            {
                CurrentContent = usercontent;
                string searchterm = Search_Username_Account.Text;
                SearchResult = (from t in CurrentContent
                                where t.Firstname.IndexOf(searchterm, StringComparison.OrdinalIgnoreCase) >= 0 || t.Lastname.IndexOf(searchterm, StringComparison.OrdinalIgnoreCase) >= 0 || t.Username.IndexOf(searchterm, StringComparison.OrdinalIgnoreCase) >= 0
                                select t).ToList();
                CurrentContent = SearchResult;
                DGV1.ItemsSource = CurrentContent;
                DGV1.Items.Refresh();
            }
        }
    }
}