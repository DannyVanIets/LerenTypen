using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LerenTypen
{
    class Account
    {
        public string UserName { get; set; }
        public DateTime Birthdate { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }

        public Account(string firstName, string surname, string userName, DateTime birthdate)
        {
            FirstName = firstName;
            Surname = surname;
            UserName = userName;
            Birthdate = birthdate;
        }

        public Account()
        {

        }
    }
}