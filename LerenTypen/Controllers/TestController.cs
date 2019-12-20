using LerenTypen.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace LerenTypen.Controllers
{
    public class TestController
    {
        public static int GetTestHighscore(int testID)
        {
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();

                // this query returns all the content from a given testId
                string mySql = "SELECT ISNULL(MAX(score), 0) FROM testresults WHERE testID=@id";

                using (SqlCommand command = new SqlCommand(mySql, connection))
                {
                    command.Parameters.AddWithValue("@id", testID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return Convert.ToInt32(reader[0]);
                        }
                    }
                }
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

            return 0;
        }

        public static Review GetUserRating(int testID, int userID)
        {
            Review queryResult = null;
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();
                string query = "select a.accountUsername, testReviewScore , trs.testReviewDescription, trs.testReviewDateAdded ,a.accountID from testReviews trs inner join accounts a on a.accountID = trs.accountID where a.archived=0 and testID=@testid and a.accountID = @accountid order by testReviewID desc;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@testid", testID);
                    command.Parameters.AddWithValue("@accountid", userID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            queryResult = new Review(reader.GetString(0), Convert.ToInt32(reader[1]), reader[2].ToString(), reader.GetDateTime(3));

                        }
                    }
                }
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

        public static int GetTestAverageScore(int testID)
        {
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();
                // this query returns all the content from a given testId
                string query = "SELECT ISNULL(AVG(score), 0) FROM testresults WHERE testID=@id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", testID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return Convert.ToInt32(reader[0]);
                        }
                    }
                }
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

            return 0;
        }

        public static bool GetTestByName(string testName)
        {
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();
                string query = "SELECT Count(*) FROM tests WHERE testName=@testName";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@testName", testName);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader.GetInt32(0) >= 1)
                            {
                                return true;
                            };
                        }
                    }
                }
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

            return false;
        }

        public static Test GetTest(int testID)
        {
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();
                string query = "SELECT testName, testType, t.accountID, timesMade, highscore, version, testDifficulty, isnull(Round(AVG(tr.testReviewScore),1),0), isPrivate, createDate FROM tests t left join testReviews tr on tr.testID=t.testID WHERE t.testID = @testID group by testName, testType, t.accountID, timesMade, highscore, version, testDifficulty, isPrivate, createDate ORDER BY AVG(tr.testReviewScore) desc";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@testID", testID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string testName = reader[0].ToString();
                            int testType = Convert.ToInt32(reader[1]);
                            int authorID = Convert.ToInt32(reader[2]);
                            string authorUsername = AccountController.GetUsername(authorID);
                            int wordCount = GetAmountOfWordsFromTest(testID);
                            double highscore = Convert.ToDouble(reader[4]);
                            int version = Convert.ToInt32(reader[5]);
                            int testDifficulty = Convert.ToInt32(reader[6]);
                            double rating = Convert.ToInt32(reader[7]);
                            bool isPrivate = Convert.ToBoolean(reader[8]);
                            DateTime createdDateTime = (DateTime)reader[9];
                            string createdDateString = createdDateTime.Date.ToString("dd/MM/yyyy");

                            return new Test(testID, testName, testType, authorID, authorUsername, wordCount, version, testDifficulty, rating, isPrivate, createdDateString);
                        }
                    }
                }
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

            return null;
        }

        /// <summary>
        /// Returns every test in the database
        /// </summary>
        /// <returns></returns>
        public static List<TestTable> GetAllTests()
        {
            List<TestTable> queryResult = new List<TestTable>();
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();
                string query = "select t.testID, t.accountID, testName, t.testDifficulty, a.accountUsername , isnull(Round(AVG(tr.testReviewScore),1),0)  from tests t left join testReviews tr on tr.testID = t.testID join accounts a on t.accountID = a.accountID where t.archived = 0 and a.archived = 0 and t.isPrivate = 0 group by t.testID, t.accountID, testName, t.testDifficulty , a.accountUsername ORDER BY AVG(tr.testReviewScore) desc";
                int counter = 1;

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Adds all the found data to a list
                            queryResult.Add(new TestTable(counter, reader.GetString(2), GetTimesMade(reader.GetInt32(0)), GetWordHighscore(reader.GetInt32(0)), GetAmountOfWordsFromTest(reader.GetInt32(0)), reader.GetInt16(3), reader.GetString(4), Convert.ToDouble(Math.Round(reader.GetDecimal(5), 1)), GetTestAverageScore(reader.GetInt32(0)), reader.GetInt32(0)));
                            counter++;
                        }
                    }
                }
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

        /// <summary>
        /// Returns the ids of the trending tests from the last week
        /// </summary>
        /// <returns></returns>
        public static List<int> GetTrendingTestIDs(int limit)
        {
            List<int> trendingTestIDs = new List<int>();
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();
                string query = "";
                if (limit == 0)
                {
                    query = "select tr.testID from testresults tr JOIN tests t on tr.testID = t.testID JOIN accounts a ON t.accountID = a.accountID where t.archived=0 and a.archived=0 and t.isPrivate=0 and tr.testResultsDate BETWEEN @weekAgo AND @now GROUP BY t.testID, tr.testID ORDER BY count(tr.testID) DESC";
                }
                else
                {
                    query = $"select TOP {limit} tr.testID from testresults tr JOIN tests t on tr.testID = t.testID JOIN accounts a ON t.accountID = a.accountID where t.archived=0 and a.archived=0 and t.isPrivate=0 and tr.testResultsDate BETWEEN @weekAgo AND @now GROUP BY t.testID, tr.testID ORDER BY count(tr.testID) DESC";
                }
                DateTime todayWeekAgo = DateTime.Now.AddDays(-7);

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@now", DateTime.Now);
                    command.Parameters.AddWithValue("@weekAgo", todayWeekAgo);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            trendingTestIDs.Add(Convert.ToInt32(reader[0]));
                        }
                    }
                }
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
            return trendingTestIDs;
        }

        /// <summary>
        /// Returns the trending tests from the last week
        /// </summary>
        /// <returns></returns>
        public static List<Test> GetTrendingTests(int limit = 0)
        {
            List<Test> trendingTests = new List<Test>();
            SqlConnection connection = new SqlConnection(Database.connectionString);
            List<int> ids = GetTrendingTestIDs(limit);

            try
            {
                string query;
                connection.Open();

                foreach (int id in ids)
                {
                    query = $"SELECT testName,testType, t.accountID, timesMade, version, testDifficulty, isPrivate, createDate FROM tests t WHERE t.testID = {id} group by t.testID, testName, testType, t.accountID, timesMade, highscore, version, testDifficulty, isPrivate, createDate";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string name = reader.GetString(0);
                                int type = Convert.ToInt32(reader[1]);
                                int authorID = Convert.ToInt32(reader[2]);
                                string authorName = AccountController.GetUsername(authorID);
                                int wordCount = TestController.GetAmountOfWordsFromTest(id);
                                int timesMade = TestController.GetTimesMade(id);
                                int version = Convert.ToInt32(reader[3]);
                                int difficulty = Convert.ToInt32(reader[4]);
                                double rating = Convert.ToInt32(ReviewController.GetRatingScore(id));
                                int isPrivate = Convert.ToInt32(reader[6]);
                                DateTime createDateTime = (DateTime)reader[7];

                                trendingTests.Add(new Test(id, name, type, authorID, authorName, wordCount, version, difficulty, rating, Convert.ToBoolean(isPrivate), createDateTime.Date.ToString("dd-MM-yyyy")));
                            }
                        }
                    }
                }
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
            return trendingTests;
        }

        /// <summary>
        /// Returns the id and name of the trending tests from the last week
        /// </summary>
        /// <returns></returns>
        public static List<Test> GetTrendingTestsNameAndID(int limit = 0)
        {
            List<Test> trendingTests = new List<Test>();
            SqlConnection connection = new SqlConnection(Database.connectionString);
            List<int> ids = GetTrendingTestIDs(limit);

            try
            {
                string query;
                connection.Open();

                foreach (int id in ids)
                {
                    query = $"SELECT testName, accountID from tests WHERE testID = {id}";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string name = reader.GetString(0);
                                int authorID = Convert.ToInt32(reader[1]);
                                string authorName = AccountController.GetUsername(authorID);

                                trendingTests.Add(new Test(id, name, authorName));
                            }
                        }
                    }
                }
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
            return trendingTests;
        }


        public static int GetWordHighscore(int testID)
        {
            int wordsPerMin = 0;
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();

                // this query returns all the content from a given testId
                string query = "SELECT MAX(wordsEachMinute), accountID FROM testresults WHERE testID=@testID GROUP BY accountID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@testID", testID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            wordsPerMin = Convert.ToInt32(reader[0]);
                        }
                    }
                }
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

            return wordsPerMin;
        }

        /// <summary>
        /// Gets the accountID of the testcreater and the testDifficulty
        /// </summary>
        /// <param name="testID"></param>
        /// <returns></returns>
        public static List<int> GetTestInformation(int testID)
        {
            List<int> results = new List<int>();
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();
                string query = "Select accountID, testDifficulty from tests where testID = @testID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@testID", testID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var accountID = reader["accountID"];
                            var testDifficulty = reader["testDifficulty"];

                            results.Add(Convert.ToInt32(accountID));
                            results.Add(Convert.ToInt32(testDifficulty));
                        }
                    }
                }
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
            return results;
        }

        public static Dictionary<int, int> GetTop3FastestTypers(int testID)
        {
            Dictionary<int, int> results = new Dictionary<int, int>();
            SqlConnection connection = new SqlConnection(Database.connectionString);

            try
            {
                connection.Open();

                // this query returns all the content from a given testId
                string query = ("SELECT TOP 3 MAX(wordsEachMinute) AS wordsEachMinute, accountID FROM testresults WHERE testID=@testID GROUP BY accountID ORDER BY wordsEachMinute DESC");

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@testID", testID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int wordsPerMin = Convert.ToInt32(reader[0]);
                            int accountID = Convert.ToInt32(reader[1]);

                            results.Add(accountID, wordsPerMin);
                        }
                    }
                }
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

            return results;
        }

        public static Dictionary<int, int> GetTop3Highscores(int testID)
        {
            Dictionary<int, int> results = new Dictionary<int, int>();
            SqlConnection connection = new SqlConnection(Database.connectionString);

            try
            {
                connection.Open();

                // this query returns all the content from a given testId
                string query = "SELECT TOP 3 MAX(score) AS score, accountID FROM testresults WHERE testID = @testID GROUP BY accountID  ORDER BY score DESC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@testID", testID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int score = Convert.ToInt32(reader[0]);
                            int accountID = Convert.ToInt32(reader[1]);

                            results.Add(accountID, score);
                        }
                    }
                }
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

            return results;
        }

        /// <summary>
        /// Get the tests name using testID
        /// </summary>
        /// <param name="testID"></param>
        /// <returns></returns>
        public static string GetTestName(int testID)
        {
            string title = "";
            SqlConnection connection = new SqlConnection(Database.connectionString);

            try
            {
                connection.Open();
                string query = "Select testName from tests where testID = @testID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@testID", testID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            title = reader.GetString(0);
                        }
                    }
                }
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

            return title;
        }

        /// <summary>
        /// Gets all of the content having this testID
        /// </summary>
        /// <param name="testID"></param>
        /// <returns></returns>
        public static List<string> GetTestContent(int testID)
        {
            List<string> results = new List<string>();
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();
                string query = "Select content from testContent Where testID = @testID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@testID", testID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(reader.GetString(0));
                        }
                    }
                }
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
            return results;
        }

        // Functions for US#11
        public static bool UpdateTestToPublic(int testId)
        {
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();
                string query = "update tests set isPrivate=0 where testId=@test;";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@test", testId);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
            return true;
        }

        public static bool UpdateTestToPrivate(int testId)
        {
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();
                string query = "update tests set isPrivate=1 where testId=@test";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@test", testId);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
            return true;
        }


        public static bool UpdateTestToArchived(int testId)
        {
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();
                string query = "UPDATE tests SET archived=1 WHERE testId=@test;";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@test", testId);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
            return true;
        }

        public static List<TestTable> GetPrivateTestMyAccount(int accountId)
        {
            List<TestTable> queryResult = new List<TestTable>();
            SqlConnection connection = new SqlConnection(Database.connectionString);

            try
            {
                connection.Open();
                string query = "select testID, testName, t.isPrivate , t.testID from tests t Inner join accounts a on t.accountID=@id where t.archived= 0 and a.archived= 0 and a.accountId= @id;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", accountId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        int i = 1;
                        while (reader.Read())
                        {
                            //adds all the found data to a list
                            queryResult.Add(new TestTable(i, reader.GetString(1), Convert.ToInt32(reader[2]), Convert.ToInt32(reader[3])));
                            i++;
                        }
                    }
                }
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

        public static List<TestTable> GetAllMyTestswithIsPrivate(int accountId)
        {
            List<TestTable> queryResult = new List<TestTable>();
            SqlConnection connection = new SqlConnection(Database.connectionString);

            try
            {
                connection.Open();
                string query = "select t.testID, t.accountID, testName, t.testDifficulty, a.accountUsername, t.isPrivate from tests t join accounts a on t.accountID = a.accountID where t.archived = 0 and a.archived = 0 group by t.testID, t.accountID, testName, t.testDifficulty , a.accountUsername, t.isPrivate";
                int bCounter = 1;

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", accountId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //adds all the found data to a list
                            int id = Convert.ToInt32(reader[0]);
                            queryResult.Add(new TestTable(bCounter, reader.GetString(2), GetTimesMade(id), GetWordHighscore(id), GetAmountOfWordsFromTest(id), Convert.ToInt32(reader[3]), reader.GetString(4), ReviewController.GetRatingScore(id), Convert.ToInt32(reader[6]), id));
                            bCounter++;
                        }
                    }
                }
            }
            catch (Exception e)
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

        public static List<TestTable> GetAllMyTestsAlreadyMade(int ingelogd)
        {
            List<TestTable> queryResult = new List<TestTable>();
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();
                // this query joins the info needed for the testtable with accounts to find the corresponding username and with testresults to find out if a test has been made before by the user
                string query = "select t.testID, t.accountID, testName, t.testDifficulty, a.accountUsername, t.isPrivate, AVG(trev.testReviewScore) from tests t Inner join accounts a on t.accountID = a.accountID inner join testresults tr on tr.testID = t.testID inner join testReviews trev on t.accountID = a.accountID where tr.accountID = @accountID and t.accountID = @accountID and t.archived = 0 and a.archived = 0 group by t.testID, t.accountID, testName, t.testDifficulty, a.accountUsername, t.isPrivate";
                int counter = 1;

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@accountID", ingelogd);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // add all the found data to a list
                            queryResult.Add(new TestTable(counter, reader.GetString(2), GetTimesMade(Convert.ToInt32(reader[0])), GetWordHighscore(Convert.ToInt32(reader[0])), GetAmountOfWordsFromTest(Convert.ToInt32(reader[0])), Convert.ToInt32(reader[3]), reader.GetString(4), Convert.ToInt32(reader[6]), Convert.ToInt32(reader[5]), reader.GetInt32(0)));
                            counter++;
                        }
                    }
                }

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

        /// <summary>
        /// Get the amount of words from the contentTable for each test
        /// </summary>
        /// <param name="testId"></param>
        /// <returns></returns>
        public static int GetAmountOfWordsFromTest(int testId)
        {
            int amountOfWords = 0;
            string fullResult = "";
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();

                // This query returns all the content from a given testId
                string query = "SELECT content FROM testcontent WHERE testID=" + testId;

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            fullResult = reader.GetString(0);

                            // Checks the string for any excess spaces and deletes them
                            string[] words = fullResult.Trim().Split();
                            foreach (var word in words)
                            {
                                if (!word.Equals(""))
                                {
                                    amountOfWords++;
                                }
                            }
                        }
                    }
                }
                return amountOfWords;
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
            return 0;
        }

        /// <summary>
        /// Method for adding tests to database. 
        /// </summary>        
        public static bool AddTest(string testName, int testType, int testDifficulty, int isPrivate, List<string> content, int uploadedBy, int version)
        {
            bool result;
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();

                // Select SCOPE_IDENTITY is used to insert the tests content into a seperate table with the same id
                // DateTime.Now is being used to get the current date and time.
                string query = "INSERT INTO tests (testName, testType, archived, testDifficulty, createDate, isPrivate, accountID, version) " +
                    $"VALUES (@testName, @testType, 0, @testDifficulty, @now, @isPrivate, @uploadedBy, @version); SELECT SCOPE_IDENTITY()";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@testName", testName);
                    command.Parameters.AddWithValue("@testType", testType);
                    command.Parameters.AddWithValue("@testDifficulty", testDifficulty);
                    command.Parameters.AddWithValue("@now", DateTime.Now);
                    command.Parameters.AddWithValue("@isPrivate", isPrivate);
                    command.Parameters.AddWithValue("@uploadedBy", uploadedBy);
                    command.Parameters.AddWithValue("@version", version);

                    object testID = command.ExecuteScalar();
                    int intTestID = int.Parse(testID.ToString());
                    result = AddTestContent(intTestID, content);

                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                connection.Close();
                connection.Dispose();

            }
            return result;

        }

        /// <summary>
        /// Transaction for editing tests, checks if person is editing the test
        /// </summary>
        /// <param name="testID"></param>
        /// <returns></returns>
        public static int EditingTest(int testID)
        {
            int result = 2;
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();

                string query = "BEGIN TRANSACTION[Tran1] " +
                    "Select beingEdited from tests where testID = @testID " +
                    "BEGIN TRY " +
                    "DECLARE " +
                    "@beingEdited TinyINT;  " +
                    "SELECT @beingEdited = beingEdited from tests where testID = @testID; " +
                    "SELECT @beingEdited " +
                    "IF @beingEdited = 0 " +
                    "BEGIN  " +
                    "Update tests SET beingEdited = 1 Where testID = @testID " +
                    "END  " +
                    "END TRY " +
                    "BEGIN CATCH " +
                    "ROLLBACK TRANSACTION[Tran1] " +
                    "END CATCH " +
                    "COMMIT TRANSACTION[Tran1]";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@testID", testID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = Convert.ToInt32(reader[0]);
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                return 2;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
            return result;
        }

        /// <summary>
        /// Called hitting save new test version
        /// </summary>        
        /// <returns></returns>
        public static bool NotBeingEdited(int testID)
        {
            bool result;
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();
                string query = "UPDATE tests SET beingEdited = 0 WHERE testID = @testID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@testID", testID);
                    command.ExecuteNonQuery();
                    result = true;
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                connection.Close();
                connection.Dispose();

            }
            return result;

        }

        /// <summary>
        /// Method adds each line of content of a test to database using its tests ID. testcontent is stored in a separate db.
        /// </summary>        
        private static bool AddTestContent(int testID, List<string> content)
        {
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();

                foreach (string contentLine in content)
                {
                    string query = "INSERT INTO testContent (testID, content) VALUES (@testID, @contentLine)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@testID", testID);
                        command.Parameters.AddWithValue("@contentLine", contentLine);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
            return true;
        }


        /// <summary>
        /// Returns all the tests that have been previously made by the user
        /// </summary>
        /// <param name="ingelogd"></param>
        /// <returns></returns>
        public static List<TestTable> GetAllTestsAlreadyMade(int ingelogd)
        {
            List<TestTable> tests = new List<TestTable>();
            SqlConnection connection = new SqlConnection(Database.connectionString);

            try
            {
                connection.Open();
                // this query joins the info needed for the testtable with accounts to find the corresponding username and with testresults to find out if a test has been made before by the user
                string query = "select t.testID, t.accountID, testName, t.testDifficulty, a.accountUsername from tests t inner join accounts a on t.accountID=a.accountID inner join testresults tr on tr.testID=t.testID where tr.accountID = @accountID and t.archived=0 and a.archived=0 and t.isPrivate=0 group by t.testID, t.accountID, testName, t.testDifficulty, a.accountUsername";
                int counter = 1;

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@accountID", ingelogd);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //add all the found data to a list
                            int testID = Convert.ToInt32(reader[0]);
                            tests.Add(new TestTable(counter, reader.GetString(2), GetTimesMade(testID), GetWordHighscore(reader.GetInt32(0)), GetAmountOfWordsFromTest(testID), Convert.ToInt32(reader[3]), reader.GetString(4), ReviewController.GetRatingScore(testID), testID));
                            counter++;
                        }
                    }
                }
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
            return tests;
        }

        public static int GetTimesMade(int testId)
        {
            SqlConnection connection = new SqlConnection(Database.connectionString);
            int result = 0;
            try
            {
                connection.Open();
                string query = "select count(*) from testresults where testId=@test;";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@test", testId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = Convert.ToInt32(reader[0]);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
            return result;
        }


        public static List<TestTable> GetAllMyTestsAlreadyMadeTop3(int accountId)
        {

            List<TestTable> queryResult = new List<TestTable>();
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();
                // this query joins the info needed for the testtable with accounts to find the corresponding username and with testresults to find out if a test has been made before by the user
                string query = "select tr.testID, testname, tr.testID ,tr.testResultsDate,t.isPrivate from tests t Inner join accounts a on t.accountID=a.accountID inner join testresults tr on tr.testID=t.testID where tr.accountID = @id and t.archived=0 and a.archived=0 and t.isPrivate=0 group by tr.testID, testname, t.isPrivate, tr.testID ,tr.testResultsDate order by tr.testResultsDate desc;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", accountId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Create Lists for making top 3 distinct
                        List<int> testIDList = new List<int>();
                        List<string> testNameList = new List<string>();
                        List<int> top3TestID = new List<int>();
                        List<string> top3TestName = new List<string>();

                        while (reader.Read())
                        {
                            testIDList.Add(Convert.ToInt32(reader[2]));
                            testNameList.Add(reader.GetString(1));

                            //Making lists top 3
                            top3TestID = testIDList.Distinct().Take(3).ToList();
                            top3TestName = testNameList.Distinct().Take(3).ToList();
                        }
                        int i = 0;
                        // putting top3 lists in one testtable list
                        foreach (int testID in top3TestID)
                        {
                            queryResult.Add(new TestTable(i + 1, top3TestName[i], testID));
                            i++;
                        }
                    }
                }
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

        /// <summary>
        /// Gets the ids of all unfinished tests from the specified account
        /// </summary>
        public static List<int> GetUnfinishedTestIDsFromAccount(int accountId)
        {
            List<int> ids = new List<int>();
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();
                string query = "select tr.testID from testresults tr join tests t on tr.testID = t.testID where tr.accountID = @id and tr.finished = 0 and t.archived = 0";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", accountId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ids.Add(Convert.ToInt32(reader[0]));
                        }
                    }
                }
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
            return ids;
        }

        /// <summary>
        /// Gets a dictionary containing the answers as the key and the answerType as the value
        /// (0 = right, 1 = wrong)
        /// </summary>
        public static List<string> GetAllLinesFromResult(int testResultID, int type = -1)
        {
            List<string> lines = new List<string>();
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();
                string query = "Select answer, rightAnswer, answerType from testResultContent Where testResultID = @testResultID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    int i = 0;
                    command.Parameters.AddWithValue("@testResultID", testResultID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (Convert.ToInt32(reader["answerType"]) == 1)
                            {
                                lines.Add(reader["rightAnswer"].ToString());
                            }
                            else
                            {
                                lines.Add(reader["answer"].ToString());
                            }

                        }
                    }

                }
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
            return lines;
        }

        /// <summary>
        /// Returns all lines that are not in the list of lines from the result 
        /// </summary>
        public static List<string> GetAllLinesNotInResult(List<string> linesInResult, Dictionary<int, string> wrongAnswers, List<string> allLines)
        {
            List<string> linesNotInResult = new List<string>(allLines);

            foreach (string line in linesInResult)
            {
                linesNotInResult.Remove(line);
            }
            foreach (KeyValuePair<int, string> kvp in wrongAnswers)
            {
                linesNotInResult.Remove(allLines[kvp.Key]);
            }

            return linesNotInResult;
        }
    }
}
