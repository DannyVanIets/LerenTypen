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
        private List<User> usercontent;
        List<User> CurrentContent = new List<User>();
        public MainWindow Mainwindow { get; set; }
        public AllUsers(MainWindow mainwindow)
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
        public int Accountnumber { get; set; }
        public string Usertype { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Edit { get; set; }
        public User(int accountnum, string usern, string acctype, string fname, string lname, string edit)
        {
            this.Accountnumber = accountnum;
            if (int.Parse(acctype) == 0)
            {
                this.Usertype = "Student";
            }
            else if (int.Parse(acctype) == 1)
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
}