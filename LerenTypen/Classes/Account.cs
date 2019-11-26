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
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }

        public Account(string userName, DateTime birthdate, string firstName, string surname, string securityQuestion, string securityAnswer)
        {
            UserName = userName;
            Birthdate = birthdate;
            FirstName = firstName;
            Surname = surname;
            SecurityQuestion = securityQuestion;
            SecurityAnswer = securityAnswer;
        }

        public Account()
        {

        }
    }
}
