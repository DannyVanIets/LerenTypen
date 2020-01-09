using LerenTypen.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LerenTypen.Controllers
{
    public static class LeaderboardController
    {
        //Fill in all the info for the hard words leaderboard
        public static List<Test> GetHardTests(int timespan)
        {
            List<Test> queryResult = new List<Test>();
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();
                string query = "";

                //check which box is being checked (weekly, monthly, yearly)
                if (timespan == 0)
                {
                    query = "select tr.accountID , AVG(wordsEachMinute), AVG(score) from testresults tr JOIN tests t on t.testID = tr.testID JOIN accounts a ON t.accountID = a.accountID  where a.archived = 0 and t.archived = 0 and t.accountID in (select a.accountID from accounts a where a.accountType = 1) and tr.finished=1  and t.isPrivate = 0 and t.testDifficulty = 2 and tr.testResultsDate BETWEEN @lastweek AND @now group by tr.accountID order by(AVG(wordsEachMinute) * AVG(score)) desc";
                }
                else if (timespan == 1)
                {
                    query = "select tr.accountID , AVG(wordsEachMinute), AVG(score) from testresults tr JOIN tests t on t.testID = tr.testID JOIN accounts a ON t.accountID = a.accountID  where a.archived = 0 and t.archived = 0 and t.accountID in (select a.accountID from accounts a where a.accountType = 1) and tr.finished=1  and t.isPrivate = 0 and t.testDifficulty = 2 and tr.testResultsDate BETWEEN @lastmonth AND @now group by tr.accountID order by(AVG(wordsEachMinute) * AVG(score)) desc";
                }
                else if (timespan == 2)
                {
                    query = "select tr.accountID , AVG(wordsEachMinute), AVG(score) from testresults tr JOIN tests t on t.testID = tr.testID JOIN accounts a ON t.accountID = a.accountID  where a.archived = 0 and t.archived = 0 and t.accountID in (select a.accountID from accounts a where a.accountType = 1) and tr.finished=1  and t.isPrivate = 0 and t.testDifficulty = 2 and tr.testResultsDate BETWEEN @lastyear AND @now group by tr.accountID order by(AVG(wordsEachMinute) * AVG(score)) desc";
                }

                DateTime todayWeekAgo = DateTime.Now.AddDays(-7);
                DateTime todayMonthAgo = DateTime.Now.AddMonths(-1);
                DateTime todayYearAgo = DateTime.Now.AddDays(-365);

                //add the times to the query's to filter them.
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@now", DateTime.Now);
                    command.Parameters.AddWithValue("@lastweek", todayWeekAgo);
                    command.Parameters.AddWithValue("@lastmonth", todayMonthAgo);
                    command.Parameters.AddWithValue("@lastyear", todayYearAgo);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        //use a counter to give the leaderboard a ranking
                        int counter = 0;
                        while (reader.Read())
                        {
                            counter++;
                            queryResult.Add(new Test(counter, AccountController.GetUsername(Convert.ToInt32(reader[0])), Convert.ToInt32(reader[1]), Convert.ToInt32(reader[2])));

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
        public static List<Test> GetMediumTests(int timespan)
        {
            List<Test> queryResult = new List<Test>();
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();
                string query = "";

                //check which box is picked and run that query.
                if (timespan == 0)
                {
                    query = "select tr.accountID , AVG(wordsEachMinute), AVG(score) from testresults tr JOIN tests t on t.testID = tr.testID JOIN accounts a ON t.accountID = a.accountID  where a.archived = 0 and t.archived = 0 and t.accountID in (select a.accountID from accounts a where a.accountType = 1) and tr.finished=1  and t.isPrivate = 0 and t.testDifficulty = 1 and tr.testResultsDate BETWEEN @lastweek AND @now group by tr.accountID order by(AVG(wordsEachMinute) * AVG(score)) desc";
                }
                else if (timespan == 1)
                {
                    query = "select tr.accountID , AVG(wordsEachMinute), AVG(score) from testresults tr JOIN tests t on t.testID = tr.testID JOIN accounts a ON t.accountID = a.accountID  where a.archived = 0 and t.archived = 0 and t.accountID in (select a.accountID from accounts a where a.accountType = 1) and tr.finished=1  and t.isPrivate = 0 and t.testDifficulty = 1 and tr.testResultsDate BETWEEN @lastmonth AND @now group by tr.accountID order by(AVG(wordsEachMinute) * AVG(score)) desc";
                }
                else if (timespan == 2)
                {
                    query = "select tr.accountID , AVG(wordsEachMinute), AVG(score) from testresults tr JOIN tests t on t.testID = tr.testID JOIN accounts a ON t.accountID = a.accountID  where a.archived = 0 and t.archived = 0 and t.accountID in (select a.accountID from accounts a where a.accountType = 1) and tr.finished=1  and t.isPrivate = 0 and t.testDifficulty = 1 and tr.testResultsDate BETWEEN @lastyear AND @now group by tr.accountID order by(AVG(wordsEachMinute) * AVG(score)) desc";
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
                    command.Parameters.AddWithValue("@lastmonth", todayMonthAgo);
                    command.Parameters.AddWithValue("@lastyear", todayYearAgo);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        //counter to show the rank on the leaderboard
                        int counter = 0;
                        while (reader.Read())
                        {
                            counter++;
                            queryResult.Add(new Test(counter, AccountController.GetUsername(Convert.ToInt32(reader[0])), Convert.ToInt32(reader[1]), Convert.ToInt32(reader[2])));

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
        public static List<Test> GetEasyTests(int timespan)
        {
            List<Test> queryResult = new List<Test>();
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();
                string query = "";

                //check which value in combobox is being picked.
                if (timespan == 0)
                {
                    query = "select tr.accountID , AVG(wordsEachMinute), AVG(score) from testresults tr JOIN tests t on t.testID = tr.testID JOIN accounts a ON t.accountID = a.accountID  where a.archived = 0 and t.archived = 0 and t.accountID in (select a.accountID from accounts a where a.accountType = 1) and tr.finished=1  and t.isPrivate = 0 and t.testDifficulty = 0 and tr.testResultsDate BETWEEN @lastweek AND @now group by tr.accountID order by(AVG(wordsEachMinute) * AVG(score)) desc";
                }
                else if (timespan == 1)
                {
                    query = "select tr.accountID , AVG(wordsEachMinute), AVG(score) from testresults tr JOIN tests t on t.testID = tr.testID JOIN accounts a ON t.accountID = a.accountID  where a.archived = 0 and t.archived = 0 and t.accountID in (select a.accountID from accounts a where a.accountType = 1) and tr.finished=1  and t.isPrivate = 0 and t.testDifficulty = 0 and tr.testResultsDate BETWEEN @lastmonth AND @now group by tr.accountID order by(AVG(wordsEachMinute) * AVG(score)) desc";
                }
                else if (timespan == 2)
                {
                    query = "select tr.accountID , AVG(wordsEachMinute), AVG(score) from testresults tr JOIN tests t on t.testID = tr.testID JOIN accounts a ON t.accountID = a.accountID  where a.archived = 0 and t.archived = 0 and t.accountID in (select a.accountID from accounts a where a.accountType = 1) and tr.finished=1  and t.isPrivate = 0 and t.testDifficulty = 0 and tr.testResultsDate BETWEEN @lastyear AND @now group by tr.accountID order by(AVG(wordsEachMinute) * AVG(score)) desc";
                }

                DateTime todayWeekAgo = DateTime.Now.AddDays(-7);
                DateTime todayMonthAgo = DateTime.Now.AddMonths(-1);
                DateTime todayYearAgo = DateTime.Now.AddDays(-365);
                //fill the query with the needed dates.
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@now", DateTime.Now);
                    command.Parameters.AddWithValue("@lastweek", todayWeekAgo);
                    command.Parameters.AddWithValue("@lastmonth", todayMonthAgo);
                    command.Parameters.AddWithValue("@lastyear", todayYearAgo);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        //counter to show the rank for the leaderboard.
                        int counter = 0;
                        while (reader.Read())
                        {
                            counter++;
                            queryResult.Add(new Test(counter, AccountController.GetUsername(Convert.ToInt32(reader[0])), Convert.ToInt32(reader[1]), Convert.ToInt32(reader[2])));
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