using System;

namespace LerenTypen.Models
{
    //In this class we will put all the date related methods, properties and variables.
    public class Date
    {
        //Now is used to get the current date and time.
        public DateTime dateOfToday = DateTime.Now;
        public DateTime dateOfTodayHundredYearsAgo = DateTime.Now.AddYears(-100);

        public static DateTime GetDateXMinutesAgo(int x)
        {
            return DateTime.Now.AddMinutes(-x);
        }

        public static DateTime GetDateXMonthsAgo(int x)
        {
            return DateTime.Now.AddMonths(-x);
        }

        public static DateTime GetDateXWeeksAgo(int x)
        {
            return DateTime.Now.AddDays(-x * 7);
        }

        public static DateTime GetDateXDaysAgo(int x)
        {
            return DateTime.Now.AddDays(-x);
        }
    }
}
