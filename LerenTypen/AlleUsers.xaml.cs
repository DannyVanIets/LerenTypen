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
    public partial class AlleUsers : Page
    {

        List<Users> Usercontent;

        List<Users> CurrentContent = new List<Users>();

        public MainWindow Mainwindow { get; set; }
        public AlleUsers(MainWindow mainwindow)
        {
            InitializeComponent();
            this.Mainwindow = mainwindow;
            Usercontent = new List<Users>();

            /*
            Usercontent.Add(new Users(1,0, "BramsToets", "makkelijk", "Bram"));
            Usercontent.Add(new Users(2,1, "Danny heeft dit gemaakt", "gemiddeld", "Bram"));
            Usercontent.Add(new Users(3,2, "Tristan opdracht 3","makkelijk", "Danny"));
            Usercontent.Add(new Users(4,0, "Mark oefententamen","moeilijk", "Tristan"));
            Usercontent.Add(new Users(5, 1,"Hugo opdracht 3", "makkelijk", "Bram"));
            */

            Usercontent = Database.GetUsers();
            // Usercontent = Database.GetUsers();
            DGV1.ItemsSource = Usercontent;
            DGV1.Items.Refresh();
            CurrentContent = Usercontent;

        }
        private void DG_Hyperlink_click(object sender , System.Windows.RoutedEventArgs e)
        {
            TextBlock textBlock = (TextBlock)sender;
            string id = textBlock.Tag.ToString();
            MessageBox.Show(id.ToString());
        }
    }
    class Users
    {

        public int accountnumber { get; set; }
        public int usertype { get; set; }
        public string username { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public  string edit { get; set; }


        public Users(int accountnum, string usern, int acctype, string fname, string lname, string edit)
        {
            this.accountnumber = accountnum;
            this.usertype = acctype;
            this.username = usern;
            this.firstname = fname;
            this.lastname = lname;
            this.edit = "edit";
        }
    }
}

