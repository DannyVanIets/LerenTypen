using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LerenTypen.Models
{
    //In this class we will put all the date related methods, properties and variables.
    class Date
    {
        //Now is used to get the current date and time.
        public DateTime dateOfToday = DateTime.Now;
        public DateTime dateOfTodayHundredYearsAgo = DateTime.Now.AddYears(-100);
    }
}
