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
        List<User> usercontent;
        List<User> CurrentContent = new List<User>();
        private List<User> SearchResult = new List<User>();
        public bool IsAdmin = false;
        public MainWindow Mainwindow { get; set; }

        public AllUsersPage(MainWindow mainwindow)
        {
            InitializeComponent();
            this.Mainwindow = mainwindow;
            usercontent = new List<User>();
            //Info loaded in from database
            usercontent = Database.GetUsers();
            DGV1.ItemsSource = usercontent;
            DGV1.Items.Refresh();
            CurrentContent = usercontent;
        }

        // This gives UserID when Edit is clicked
        private void DG_Hyperlink_click(object sender, System.Windows.RoutedEventArgs e)
        {
            TextBlock textBlock = (TextBlock)sender;
            User user = (User)textBlock.Tag;
            string id = user.Accountnumber.ToString();
            string usertype = user.UserTypeID.ToString();
            Database.GetUserAccount(int.Parse(id));
            var newWindow = new AdminUserPanel(int.Parse(id), int.Parse(usertype));
            newWindow.ShowDialog();
            Mainwindow.ChangePage(new AllUsersPage(Mainwindow));
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
public class User
{
    public int Accountnumber { get; set; }
    public int UserTypeID { get; set; }
    public string Usertype { get; set; }
    public string Username { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Edit { get; set; }

    public User(int accountnum, string usern, int acctype, string fname, string lname)
    {
        this.Accountnumber = accountnum;
        this.UserTypeID = acctype;

        if (UserTypeID == 0)
        {
            this.Usertype = "Student";
        }
        else if (UserTypeID == 1)
        {
            this.Usertype = "Docent";
        }
        else
        {
            this.Usertype = "Admin";
        }
        this.Username = usern;
        this.Firstname = fname;
        this.Lastname = lname;
        this.Edit = "Bewerken";
    }
}
