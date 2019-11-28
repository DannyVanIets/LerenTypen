using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        List<Users> Usercontent;

        List<Users> CurrentContent = new List<Users>();
        private List<Users> SearchResult = new List<Users>();
        public bool IsAdmin = false;
        public MainWindow Mainwindow { get; set; }
        public AllUsers(MainWindow mainwindow)
        {
            InitializeComponent();
            this.Mainwindow = mainwindow;
            Usercontent = new List<Users>();

            //de info word uit de database gehaald
            Usercontent = Database.GetUsers();
            DGV1.ItemsSource = Usercontent;
            DGV1.Items.Refresh();
            CurrentContent = Usercontent;

        }
        //hier word de userid meegegven wanneer op edit word geklikt
        private void DG_Hyperlink_click(object sender, System.Windows.RoutedEventArgs e)
        {
            TextBlock textBlock = (TextBlock)sender;
            string id = textBlock.Tag.ToString();
            MessageBox.Show(id.ToString());
        }

        private void Search_Event(object sender, TextChangedEventArgs e)
        {
            if (Search_Username_Account.Text.Equals(""))
            {
                CurrentContent = Usercontent;
                DGV1.ItemsSource = CurrentContent;
                DGV1.Items.Refresh();
            }
            if (!Search_Username_Account.Text.Equals(""))
            {
                CurrentContent = Usercontent;
                string searchterm = Search_Username_Account.Text;
                SearchResult = (from t in CurrentContent
                                where t.firstname.IndexOf(searchterm, StringComparison.OrdinalIgnoreCase) >= 0 || t.lastname.IndexOf(searchterm, StringComparison.OrdinalIgnoreCase) >= 0 || t.username.IndexOf(searchterm, StringComparison.OrdinalIgnoreCase) >= 0
                                select t).ToList();

                CurrentContent = SearchResult;
                DGV1.ItemsSource = CurrentContent;
                DGV1.Items.Refresh();
            }
        }
    }
}

class Users
{

    public int accountnumber { get; set; }
    public int usertype { get; set; }
    public string username { get; set; }
    public string firstname { get; set; }
    public string lastname { get; set; }
    public string edit { get; set; }


    public Users(int accountnum, string usern, int acctype, string fname, string lname, string edit)
    {
        this.accountnumber = accountnum;
        this.usertype = acctype;
        this.username = usern;
        this.firstname = fname;
        this.lastname = lname;
        this.edit = "Edit";
    }
}

