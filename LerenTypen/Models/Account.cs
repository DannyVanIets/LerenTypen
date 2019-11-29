using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LerenTypen.Models
{
    //In this class we will store all the information from an account that we can later one use in EditAccountPage to fill in the textboxes with all the correct information.
    public class Account
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

        public Account(string userName, DateTime birthdate, string firstName, string surname)
        {
            UserName = userName;
            Birthdate = birthdate;
            FirstName = firstName;
            Surname = surname;
        }

        public Account()
        {

        }
    }
}
