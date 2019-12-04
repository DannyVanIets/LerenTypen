namespace LerenTypen.Models
{
    class UserTable
    {
        public int Accountnumber { get; set; }
        public int UserTypeID { get; set; }
        public string Usertype { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Edit { get; set; }

        public UserTable(int accountnum, string usern, int acctype, string fname, string lname)
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
}
