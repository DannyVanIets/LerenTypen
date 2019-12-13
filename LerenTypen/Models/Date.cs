using System;

namespace LerenTypen.Models
{
    //In this class we will put all the date related methods, properties and variables.
    public class Date
    {
        //Now is used to get the current date and time.
        public DateTime dateOfToday = DateTime.Now;
        public DateTime dateOfTodayHundredYearsAgo = DateTime.Now.AddYears(-100);

        public DateTime getDateXyearsAgo(int x)
        {
            return DateTime.Now.AddMonths(-x);
        }
        public DateTime getDateXMonthsAgo(int x)
        {
            return DateTime.Now.AddMonths(-x);
        }

        public DateTime getDateXWeeksAgo(int x)
        {
            return DateTime.Now.AddMonths(-x);
        }

        public DateTime getDateXDaysAgo(int x)
        {
            return DateTime.Now.AddDays(-x);
        }
    }
}
