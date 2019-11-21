using System.Windows.Controls;

namespace LerenTypen
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>

        public AlleUsers(MainWindow main)
    {
        initi
    }


    class Users
    {

        public int accountid { get; set; }
        public int accountType { get; set; }
        public string username { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }


        public Users(int accountnum, int acctype, string usern, string fname, string lname)
        {
            this.accountid = accountnum;
            this.accountType = acctype;
            this.username = usern;
            this.firstname = fname;
            this.lastname = lname;
        }
    }
}

