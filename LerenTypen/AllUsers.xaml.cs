using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace LerenTypen
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class AllUsers : Page
    {
        private List<User> Usercontent;
        List<User> CurrentContent = new List<User>();
        public MainWindow Mainwindow { get; set; }
        public AllUsers(MainWindow mainwindow)
        {
            InitializeComponent();
            this.Mainwindow = mainwindow;
            Usercontent = new List<User>();
            //Info loaded in from database
            Usercontent = Database.GetUsers();
            DGV1.ItemsSource = Usercontent;
            DGV1.Items.Refresh();
            CurrentContent = Usercontent;
        }
        //This gives UserID when Edit is clicked
        private void DG_Hyperlink_click(object sender, System.Windows.RoutedEventArgs e)
        {
            TextBlock textBlock = (TextBlock)sender;
            string id = textBlock.Tag.ToString();
            Database.GetUserAccount(int.Parse(id));
            var newWindow = new AdminUserPanel(int.Parse(id));
            newWindow.Show();
        }
    }
    public class User
    {
        public int accountnumber { get; set; }
        public int usertype { get; set; }
        public string username { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string edit { get; set; }
        public User(int accountnum, string usern, int acctype, string fname, string lname, string edit)
        {
            this.accountnumber = accountnum;
            this.usertype = acctype;
            this.username = usern;
            this.firstname = fname;
            this.lastname = lname;
            this.edit = "Edit";
        }
    }
}