using System.Windows.Controls;

namespace LerenTypen
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class AlleUsers : Page
    {
        public AlleUsers()
        {
            InitializeComponent();
        }


        class Users {

            public int accountnumber{ get; set; }
            public  string username{ get; set; }
            public string firstname { get; set; }
            public string lastname{ get; set;  }

            public Users(int accountnum , string usern , string fname, string lname)
            {
                this.accountnumber = accountnum;
                this.username = usern;
                this.firstname = fname;
                this.lastname = lname;
            }

        }
        public void GetUsers()
        {

        }
    }
}
