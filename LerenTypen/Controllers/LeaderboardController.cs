using LerenTypen.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LerenTypen.Controllers
{
    class LeaderboardController
    {
        //Fill in all the info for the hard words leaderboard
        public static List<Test> LeaderboardHardTestsWords(int pick)
        {
            List<Test> queryResult = new List<Test>();
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();
                //start a new query which can be filled
                string query = "";
                //check which box is being checked (weekly, monthly, yearly)
                if (pick == 0)
                {
                    query = "select tr.accountID  , t.testName , max(wordsEachMinute) ,t.testDifficulty from testresults tr JOIN tests t on t.testID = tr.testID JOIN accounts a ON t.accountID = a.accountID  where a.archived = 0 and t.archived = 0 and t.accountID in (select a.accountID from accounts a where a.accountType=1) and t.isPrivate = 0 and t.testDifficulty = 2 and tr.testResultsDate BETWEEN @lastweek AND @now group by a.accountUsername, tr.accountID, t.testName ,t.testDifficulty order by max(wordsEachMinute) desc";
                }
                else if (pick == 1)
                {
                    query = "select tr.accountID  , t.testName , max(wordsEachMinute) ,t.testDifficulty from testresults tr JOIN tests t on t.testID = tr.testID JOIN accounts a ON t.accountID = a.accountID  where a.archived = 0 and t.archived = 0 and t.accountID in (select a.accountID from accounts a where a.accountType=1) and t.isPrivate = 0 and t.testDifficulty = 2 and tr.testResultsDate BETWEEN @lastmonth AND @now group by a.accountUsername, tr.accountID, t.testName ,t.testDifficulty order by max(wordsEachMinute) desc";
                }
                else if (pick == 2)
                {
                    query = "select tr.accountID  , t.testName , max(wordsEachMinute) ,t.testDifficulty from testresults tr JOIN tests t on t.testID = tr.testID JOIN accounts a ON t.accountID = a.accountID  where a.archived = 0 and t.archived = 0 and t.accountID in (select a.accountID from accounts a where a.accountType=1) and t.isPrivate = 0 and t.testDifficulty = 2 and tr.testResultsDate BETWEEN @lastyear AND @now group by a.accountUsername, tr.accountID, t.testName ,t.testDifficulty order by max(wordsEachMinute) desc";
                }
                DateTime todayWeekAgo = DateTime.Now.AddDays(-7);
                DateTime todayMonthAgo = DateTime.Now.AddMonths(-1);
                DateTime todayYearAgo = DateTime.Now.AddDays(-365);

                //add the times to the query's to filter them.
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@now", DateTime.Now);
                    command.Parameters.AddWithValue("@lastweek", todayWeekAgo);
                    command.Parameters.AddWithValue("lastmonth", todayMonthAgo);
                    command.Parameters.AddWithValue("lastyear", todayYearAgo);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        //use a counter to give the leaderboard a ranking
                        int counter = 0;
                        while (reader.Read())
                        {
                            counter++;
                            queryResult.Add(new Test(counter, AccountController.GetUsername(Convert.ToInt32(reader[0])), reader.GetString(1), Convert.ToInt32(reader[2])));

                        }
                    }
                }
                return queryResult;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
            return queryResult;
        }
        //fill the info the hard percentage board
        public static List<Test> LeaderboardHardTestsWordsPerc(int pick)
        {
            List<Test> queryResult = new List<Test>();
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();
                //start a new query which can be filled
                string query = "";
                //check which box is being checked (weekly, monthly, yearly)
                if (pick == 0)
                {
                    query = "select tr.accountID  , t.testName , max(score) ,t.testDifficulty from testresults tr JOIN tests t on t.testID = tr.testID JOIN accounts a ON t.accountID = a.accountID  where a.archived = 0 and t.archived = 0 and t.accountID in (select a.accountID from accounts a where a.accountType=1) and t.isPrivate = 0 and t.testDifficulty = 2 and tr.testResultsDate BETWEEN @lastweek AND @now group by a.accountUsername, tr.accountID, t.testName ,t.testDifficulty order by max(score) desc";
                }
                else if (pick == 1)
                {
                    query = "select tr.accountID  , t.testName , max(score) ,t.testDifficulty from testresults tr JOIN tests t on t.testID = tr.testID JOIN accounts a ON t.accountID = a.accountID  where a.archived = 0 and t.archived = 0 and t.accountID in (select a.accountID from accounts a where a.accountType=1) and t.isPrivate = 0 and t.testDifficulty = 2 and tr.testResultsDate BETWEEN @lastmonth AND @now group by a.accountUsername, tr.accountID, t.testName ,t.testDifficulty order by max(score) desc";
                }
                else if (pick == 2)
                {
                    query = "select tr.accountID  , t.testName , max(score) ,t.testDifficulty from testresults tr JOIN tests t on t.testID = tr.testID JOIN accounts a ON t.accountID = a.accountID  where a.archived = 0 and t.archived = 0 and t.accountID in (select a.accountID from accounts a where a.accountType=1) and t.isPrivate = 0 and t.testDifficulty = 2 and tr.testResultsDate BETWEEN @lastyear AND @now group by a.accountUsername, tr.accountID, t.testName ,t.testDifficulty order by max(score) desc";
                }
                DateTime todayWeekAgo = DateTime.Now.AddDays(-7);
                DateTime todayMonthAgo = DateTime.Now.AddMonths(-1);
                DateTime todayYearAgo = DateTime.Now.AddDays(-365);

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@now", DateTime.Now);
                    command.Parameters.AddWithValue("@lastweek", todayWeekAgo);
                    command.Parameters.AddWithValue("lastmonth", todayMonthAgo);
                    command.Parameters.AddWithValue("lastyear", todayYearAgo);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        int counter = 0;
                        while (reader.Read())
                        {
                            counter++;
                            queryResult.Add(new Test(counter, AccountController.GetUsername(Convert.ToInt32(reader[0])), reader.GetString(1), Convert.ToInt32(reader[2])));
                        }
                    }
                }
                return queryResult;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
            return queryResult;
        }

        //Fill the list for the medium words board
        public static List<Test> LeaderboardMediumTestsWords(int pick)
        {
            List<Test> queryResult = new List<Test>();
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();
                //start a new query which can be filled
                string query = "";
                //check which box is being checked (weekly, monthly, yearly)
                if (pick == 0)
                {
                    query = "select tr.accountID  , t.testName , max(wordsEachMinute) ,t.testDifficulty from testresults tr JOIN tests t on t.testID = tr.testID JOIN accounts a ON t.accountID = a.accountID  where a.archived = 0 and t.archived = 0 and t.accountID in (select a.accountID from accounts a where a.accountType=1) and t.isPrivate = 0 and t.testDifficulty = 1 and tr.testResultsDate BETWEEN @lastweek AND @now group by a.accountUsername, tr.accountID, t.testName ,t.testDifficulty order by max(wordsEachMinute) desc";
                }
                else if (pick == 1)
                {
                    query = "select tr.accountID  , t.testName , max(wordsEachMinute) ,t.testDifficulty from testresults tr JOIN tests t on t.testID = tr.testID JOIN accounts a ON t.accountID = a.accountID  where a.archived = 0 and t.archived = 0 and t.accountID in (select a.accountID from accounts a where a.accountType=1) and t.isPrivate = 0 and t.testDifficulty = 1 and tr.testResultsDate BETWEEN @lastmonth AND @now group by a.accountUsername, tr.accountID, t.testName ,t.testDifficulty order by max(wordsEachMinute) desc";
                }
                else if (pick == 2)
                {
                    query = "select tr.accountID  , t.testName , max(wordsEachMinute) ,t.testDifficulty from testresults tr JOIN tests t on t.testID = tr.testID JOIN accounts a ON t.accountID = a.accountID  where a.archived = 0 and t.archived = 0 and t.accountID in (select a.accountID from accounts a where a.accountType=1) and t.isPrivate = 0 and t.testDifficulty = 1 and tr.testResultsDate BETWEEN @lastyear AND @now group by a.accountUsername, tr.accountID, t.testName ,t.testDifficulty order by max(wordsEachMinute) desc";
                }
                DateTime todayWeekAgo = DateTime.Now.AddDays(-7);
                DateTime todayMonthAgo = DateTime.Now.AddMonths(-1);
                DateTime todayYearAgo = DateTime.Now.AddDays(-365);

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@now", DateTime.Now);
                    command.Parameters.AddWithValue("@lastweek", todayWeekAgo);
                    command.Parameters.AddWithValue("lastmonth", todayMonthAgo);
                    command.Parameters.AddWithValue("lastyear", todayYearAgo);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        int counter = 0;
                        while (reader.Read())
                        {
                            counter++;
                            queryResult.Add(new Test(counter, AccountController.GetUsername(Convert.ToInt32(reader[0])), reader.GetString(1), Convert.ToInt32(reader[2])));
                        }
                    }
                }
                return queryResult;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
            return queryResult;
        }
        //Fill in all the into for the medium board
        public static List<Test> LeaderboardMediumTestsPerc(int pick)
        {
            List<Test> queryResult = new List<Test>();
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();
                //new query started
                string query = "";
                //check which box is picked and run that query.
                if (pick == 0)
                {
                    query = "select tr.accountID  , t.testName , max(score) ,t.testDifficulty from testresults tr JOIN tests t on t.testID = tr.testID JOIN accounts a ON t.accountID = a.accountID  where a.archived = 0 and t.archived = 0 and t.accountID in (select a.accountID from accounts a where a.accountType=1) and t.isPrivate = 0 and t.testDifficulty = 1 and tr.testResultsDate BETWEEN @lastweek AND @now group by a.accountUsername, tr.accountID, t.testName ,t.testDifficulty order by max(score) desc";
                }
                else if (pick == 1)
                {
                    query = "select tr.accountID  , t.testName , max(score) ,t.testDifficulty from testresults tr JOIN tests t on t.testID = tr.testID JOIN accounts a ON t.accountID = a.accountID  where a.archived = 0 and t.archived = 0 and t.accountID in (select a.accountID from accounts a where a.accountType=1) and t.isPrivate = 0 and t.testDifficulty =1 and tr.testResultsDate BETWEEN @lastmonth AND @now group by a.accountUsername, tr.accountID, t.testName ,t.testDifficulty order by max(score) desc";
                }
                else if (pick == 2)
                {
                    query = "select tr.accountID  , t.testName , max(score) ,t.testDifficulty from testresults tr JOIN tests t on t.testID = tr.testID JOIN accounts a ON t.accountID = a.accountID  where a.archived = 0 and t.archived = 0 and t.accountID in (select a.accountID from accounts a where a.accountType=1) and t.isPrivate = 0 and t.testDifficulty = 1 and tr.testResultsDate BETWEEN @lastyear AND @now group by a.accountUsername, tr.accountID, t.testName ,t.testDifficulty order by max(score) desc";
                }
                //see which days etc.
                DateTime todayWeekAgo = DateTime.Now.AddDays(-7);
                DateTime todayMonthAgo = DateTime.Now.AddMonths(-1);
                DateTime todayYearAgo = DateTime.Now.AddDays(-365);
                //Fill in the dates in the code
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@now", DateTime.Now);
                    command.Parameters.AddWithValue("@lastweek", todayWeekAgo);
                    command.Parameters.AddWithValue("lastmonth", todayMonthAgo);
                    command.Parameters.AddWithValue("lastyear", todayYearAgo);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        //counter to show the rank on the leaderboard
                        int counter = 0;
                        while (reader.Read())
                        {
                            counter++;
                            queryResult.Add(new Test(counter, AccountController.GetUsername(Convert.ToInt32(reader[0])), reader.GetString(1), Convert.ToInt32(reader[2])));

                        }
                    }
                }
                return queryResult;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
            return queryResult;
        }

        //fill in the into for the leaderboard
        public static List<Test> LeaderboardEasyTestsWords(int pick)
        {
            List<Test> queryResult = new List<Test>();
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();
                //new query start
                string query = "";
                //see which box is being picked
                if (pick == 0)
                {
                    query = "select tr.accountID  , t.testName , max(wordsEachMinute) ,t.testDifficulty from testresults tr JOIN tests t on t.testID = tr.testID JOIN accounts a ON t.accountID = a.accountID  where a.archived = 0 and t.archived = 0 and t.accountID in (select a.accountID from accounts a where a.accountType=1) and t.isPrivate = 0 and t.testDifficulty = 0 and tr.testResultsDate BETWEEN @lastweek AND @now group by a.accountUsername, tr.accountID, t.testName ,t.testDifficulty order by max(wordsEachMinute) desc";
                }
                else if (pick == 1)
                {
                    query = "select tr.accountID  , t.testName , max(wordsEachMinute) ,t.testDifficulty from testresults tr JOIN tests t on t.testID = tr.testID JOIN accounts a ON t.accountID = a.accountID  where a.archived = 0 and t.archived = 0 and t.accountID in (select a.accountID from accounts a where a.accountType=1) and t.isPrivate = 0 and t.testDifficulty = 0 and tr.testResultsDate BETWEEN @lastmonth AND @now group by a.accountUsername, tr.accountID, t.testName ,t.testDifficulty order by max(wordsEachMinute) desc";
                }
                else if (pick == 2)
                {
                    query = "select tr.accountID  , t.testName , max(wordsEachMinute) ,t.testDifficulty from testresults tr JOIN tests t on t.testID = tr.testID JOIN accounts a ON t.accountID = a.accountID  where a.archived = 0 and t.archived = 0 and t.accountID in (select a.accountID from accounts a where a.accountType=1) and t.isPrivate = 0 and t.testDifficulty = 0 and tr.testResultsDate BETWEEN @lastyear AND @now group by a.accountUsername, tr.accountID, t.testName ,t.testDifficulty order by max(wordsEachMinute) desc";
                }
                DateTime todayWeekAgo = DateTime.Now.AddDays(-7);
                DateTime todayMonthAgo = DateTime.Now.AddMonths(-1);
                DateTime todayYearAgo = DateTime.Now.AddDays(-365);

                //fill in the date in the code
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@now", DateTime.Now);
                    command.Parameters.AddWithValue("@lastweek", todayWeekAgo);
                    command.Parameters.AddWithValue("lastmonth", todayMonthAgo);
                    command.Parameters.AddWithValue("lastyear", todayYearAgo);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        int counter = 0;
                        while (reader.Read())
                        {
                            counter++;
                            queryResult.Add(new Test(counter, AccountController.GetUsername(Convert.ToInt32(reader[0])), reader.GetString(1), Convert.ToInt32(reader[2])));
                        }
                    }
                }
                return queryResult;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
            return queryResult;
        }

        //fill in the info for the leaderboard percentage
        public static List<Test> LeaderboardEasyTestsPerc(int pick)
        {
            List<Test> queryResult = new List<Test>();
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();
                //start a new quert
                string query = "";
                //check which value in combobox is being picked.
                if (pick == 0)
                {
                    query = "select tr.accountID  , t.testName , max(score) ,t.testDifficulty from testresults tr JOIN tests t on t.testID = tr.testID JOIN accounts a ON t.accountID = a.accountID  where a.archived = 0 and t.archived = 0 and t.accountID in (select a.accountID from accounts a where a.accountType=1) and t.isPrivate = 0 and t.testDifficulty = 0 and tr.testResultsDate BETWEEN @lastweek AND @now group by a.accountUsername, tr.accountID, t.testName ,t.testDifficulty order by max(score) desc";
                }
                else if (pick == 1)
                {
                    query = "select tr.accountID  , t.testName , max(score) ,t.testDifficulty from testresults tr JOIN tests t on t.testID = tr.testID JOIN accounts a ON t.accountID = a.accountID  where a.archived = 0 and t.archived = 0 and t.accountID in (select a.accountID from accounts a where a.accountType=1) and t.isPrivate = 0 and t.testDifficulty = 0 and tr.testResultsDate BETWEEN @lastmonth AND @now group by a.accountUsername, tr.accountID, t.testName ,t.testDifficulty order by max(score) desc";
                }
                else if (pick == 2)
                {
                    query = "select tr.accountID  , t.testName , max(score) ,t.testDifficulty from testresults tr JOIN tests t on t.testID = tr.testID JOIN accounts a ON t.accountID = a.accountID  where a.archived = 0 and t.archived = 0 and t.accountID in (select a.accountID from accounts a where a.accountType=1) and t.isPrivate = 0 and t.testDifficulty = 0 and tr.testResultsDate BETWEEN @lastyear AND @now group by a.accountUsername, tr.accountID, t.testName ,t.testDifficulty order by max(score) desc";
                }
                DateTime todayWeekAgo = DateTime.Now.AddDays(-7);
                DateTime todayMonthAgo = DateTime.Now.AddMonths(-1);
                DateTime todayYearAgo = DateTime.Now.AddDays(-365);
                //fill the query with the needed dates.
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@now", DateTime.Now);
                    command.Parameters.AddWithValue("@lastweek", todayWeekAgo);
                    command.Parameters.AddWithValue("lastmonth", todayMonthAgo);
                    command.Parameters.AddWithValue("lastyear", todayYearAgo);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        //counter to show the rank for the leaderboard.
                        int counter = 0;
                        while (reader.Read())
                        {
                            counter++;
                            queryResult.Add(new Test(counter, AccountController.GetUsername(Convert.ToInt32(reader[0])), reader.GetString(1), Convert.ToInt32(reader[2])));
                        }
                    }
                }

                return queryResult;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
            return queryResult;
        }


    }
}