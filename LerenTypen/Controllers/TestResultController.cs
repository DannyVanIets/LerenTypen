using LerenTypen.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace LerenTypen.Controllers
{
    public class TestResultController
    {
        /// <summary>
        /// Gets the results using resultsID
        /// </summary>
        /// <param name="accountID"></param>
        /// <param name="testID"></param>
        /// <param name="testResultsID"></param>
        /// <returns></returns>
        public static List<string> GetTestResults(int testResultsID)
        {
            List<string> results = new List<string>();
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();
                string query = "Select wordsEachMinute, pauses, score from testresults where testResultID = @testResultID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@testResultID", testResultsID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(reader["wordsEachMinute"].ToString());
                            results.Add(reader["pauses"].ToString());
                            results.Add(reader["score"].ToString());
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
        /// Gets the right answers of a testResult using testResultID
        /// </summary>
        /// <param name="testResultID"></param>
        /// <returns></returns>
        public static List<string> GetTestResultsContentRight(int testResultID)
        {
            List<string> results = new List<string>();
            SqlConnection connection = new SqlConnection(Database.connectionString);

            try
            {
                connection.Open();
                string query = "Select answer from testResultContent Where testResultID = @testResultID and answerType = 0";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@testResultID", testResultID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(reader["answer"].ToString());
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

        // Gets the wrong answers of a testResult using testID and testResultID
        // Dictionary key = line index and value = answer
        public static Dictionary<int, string> GetTestResultsContentWrong(int testID, int testResultID)
        {
            Dictionary<int, string> results = new Dictionary<int, string>();
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();
                string query = "Select rightAnswer, answerType, answer from testResultContent Where testResultID = @testResultID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@testResultID", testResultID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        int i = 0;
                        while (reader.Read())
                        {
                            if (Convert.ToInt32(reader["answerType"]) == 1)
                            {
                                results.Add(i, reader["answer"].ToString());
                            }
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
            return results;
        }

        // Gets the answers the wrong answers had to be
        public static List<string> GetTestResultsContentHadToBe(int testResultID)
        {
            List<string> results = new List<string>();
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();
                string query = "Select rightAnswer from testResultContent Where testResultID = @testResultID and answerType = 1";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@testResultID", testResultID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(reader["rightAnswer"].ToString());
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

        public static List<TestResult> GetAllTestResultsFromAccount(int accountID, int testID)
        {
            List<TestResult> results = new List<TestResult>();
            SqlConnection connection = new SqlConnection(Database.connectionString);

            try
            {
                connection.Open();
                string query = "SELECT testResultID, testResultsDate, wordsEachMinute FROM testresults WHERE testID=@testID AND accountID=@accID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@testID", testID);
                    command.Parameters.AddWithValue("@accID", accountID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader[0]);
                            DateTime dateTime = (DateTime)reader[1];
                            string date = dateTime.Date.ToString("dd/MM/yyyy");
                            int wordsPerMin = Convert.ToInt32(reader[2]);

                            results.Add(new TestResult(id, testID, date, wordsPerMin));
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
        /// inserts results of the test after the test has been made and adds the users input to testresultcontent right after, using the same testresult id
        /// </summary>
        public static int SaveResults(int testID, int accountID, int wordsEachMinute, int pauses, List<string> rightAnswers, Dictionary<int, string> wrongAnswers, List<string> lines, int score, int time, bool finished)
        {
            int testResultID = 0;
            SqlConnection connection = new SqlConnection(Database.connectionString);

            try
            {
                connection.Open();
                string query;
                if (finished)
                {
                    query = "INSERT INTO testresults (testID, accountID, testResultsDate, wordsEachMinute, pauses, score, time) VALUES (@testID, @accountID, CURRENT_TIMESTAMP, @WordsEachMinute, @pauses, @score, @time); Select SCOPE_IDENTITY();";
                }
                else
                {
                    query = "INSERT INTO testresults (testID, accountID, testResultsDate, wordsEachMinute, pauses, score, time, finished) VALUES (@testID, @accountID, CURRENT_TIMESTAMP, @WordsEachMinute, @pauses, @score, @time, 0); Select SCOPE_IDENTITY();";
                }

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@testID", testID);
                    command.Parameters.AddWithValue("@accountID", accountID);
                    command.Parameters.AddWithValue("@wordsEachMinute", wordsEachMinute);
                    command.Parameters.AddWithValue("@pauses", pauses);
                    command.Parameters.AddWithValue("@score", score);
                    command.Parameters.AddWithValue("@time", time);

                    testResultID = Convert.ToInt32(command.ExecuteScalar());
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

            InsertResultsContent(testResultID, rightAnswers, wrongAnswers, lines, finished);
            return testResultID;
        }

        private static void InsertResultsContent(int testResultID, List<string> rightAnswers, Dictionary<int, string> wrongAnswers, List<string> lines, bool finished)
        {
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();

                foreach (string rightAnswer in rightAnswers)
                {

                    string query = "INSERT INTO testresultcontent (testResultID, answer, answerType) VALUES (@testResultID, @answer, @answerType)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@testResultID", testResultID);
                        command.Parameters.AddWithValue("@answer", rightAnswer);
                        command.Parameters.AddWithValue("@answerType", 0);
                        command.ExecuteNonQuery();
                    }
                }

                // Position of right answer in lines is stored in keyvaluepairs int
                foreach (KeyValuePair<int, string> wrongAnswer in wrongAnswers)
                {
                    string query = "INSERT INTO testresultcontent (testResultID, answer, answerType, rightAnswer) VALUES (@testResultID, @answer, @answerType, @rightAnswer)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@testResultID", testResultID);
                        command.Parameters.AddWithValue("@answer", wrongAnswer.Value);
                        command.Parameters.AddWithValue("@answerType", 1);
                        command.Parameters.AddWithValue("@rightAnswer", lines[wrongAnswer.Key]);
                        command.ExecuteNonQuery();
                    }
                }
                if (!finished)
                {
                    List<string> unfinishedLines = TestController.GetAllLinesNotInResult(rightAnswers, wrongAnswers, lines);
                    foreach (string line in unfinishedLines)
                    {
                        string query = "INSERT INTO testresultcontent (testResultID, answer, answerType) VALUES (@testResultID, @answer, @answerType)";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@testResultID", testResultID);
                            command.Parameters.AddWithValue("@answer", line);
                            command.Parameters.AddWithValue("@answerType", 2);
                            command.ExecuteNonQuery();
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
        }

        /// <summary>
        /// Gets the unfinished testresultID from the specified account and test
        /// </summary>
        public static int GetUnfinishedTestResultID(int accountID, int testID)
        {
            int id = 0;
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();
                string query = "SELECT testResultID FROM testresults WHERE testID = @testID AND accountID = @accID AND finished = 0";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@accID", accountID);
                    command.Parameters.AddWithValue("@testID", testID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            id = Convert.ToInt32(reader[0]);
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
            return id;
        }

        public static void DeleteTestResult(int testResultID)
        {
            DeleteTestResultContent(testResultID);

            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();
                string query = "DELETE FROM testresults WHERE testResultID = @testresID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@testresID", testResultID);
                    command.ExecuteNonQuery();
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
        }

        /// <summary>
        /// Deletes the content of the specified test result
        /// </summary>
        private static void DeleteTestResultContent(int testResultID)
        {
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();
                string query = "DELETE FROM testresultcontent WHERE testResultID = @testresID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@testresID", testResultID);
                    int rowsAffected = command.ExecuteNonQuery();
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
        }

        public static int GetAmountOfPauses(int testResultID)
        {
            int amount = 0;
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();
                string query = "SELECT pauses FROM testresults WHERE testResultID = @testResultID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@testResultID", testResultID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            amount = Convert.ToInt32(reader[0]);
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
            return amount;
        }

        public static int GetTime(int testResultID)
        {
            int time = 0;
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();
                string query = "SELECT time FROM testresults WHERE testResultID = @testResultID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@testResultID", testResultID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            time = Convert.ToInt32(reader[0]);
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
            return time;
        }

        /// <summary>
        /// Calculates the percentage of right answers of a test result
        /// </summary>
        public static decimal CalculatePercentageRight(int testID, int testResultID)
        {
            List<string> rightAnswers = TestResultController.GetTestResultsContentRight(testResultID);
            Dictionary<int, string> wrongAnswers = TestResultController.GetTestResultsContentWrong(testID, testResultID);
            decimal percentageRight;

            try
            {
                percentageRight = decimal.Divide(rightAnswers.Count, rightAnswers.Count + wrongAnswers.Count) * 100;
            }
            catch (DivideByZeroException)
            {
                percentageRight = 100;
            }
            return percentageRight;
        }
    }
}
