using System;

namespace LerenTypen.Controllers
{
    class DateController
    {
        public static DateTime GetDateXMinutesAgo(int x)
        {
            return DateTime.Now.AddMinutes(-x);
        }

        public static DateTime GetDateXYearsAgo(int x)
        {
            DateTime date = DateTime.Now.AddYears(-x);
            return new DateTime(date.Year, 1, 1);
        }

        public static DateTime GetDateXMonthsAgo(int x)
        {
            DateTime date = DateTime.Now.AddMonths(-x);
            return new DateTime(date.Year, date.Month, 1);
        }

        public static DateTime GetDateXWeeksAgo(int x)
        {
            DateTime date = DateTime.Now.Date.AddDays(-x * 7);
            return StartOfWeek(date, DayOfWeek.Monday);
        }

        public static DateTime GetDateXDaysAgo(int x)
        {
            return DateTime.Now.Date.AddDays(-x);
        }

        //Gets the first day of dt's week
        private static DateTime StartOfWeek(DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }
    }
}
