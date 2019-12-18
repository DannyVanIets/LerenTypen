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
        /// Gets the results using resultsID
        /// </summary>
        /// <param name="accountID"></param>
        /// <returns></returns>
        public static int GetWordsPerMinuteByPeriod(int accountID, DateTime firstDate, DateTime secondDate)
        {
            int result = 0;
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();
                string query = "select avg(wordsEachMinute) from testresults where testResultsDate BETWEEN @firstDate AND @secondDate and accountID = @accountID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@accountID", accountID);
                    command.Parameters.AddWithValue("@firstDate", firstDate.ToString());
                    command.Parameters.AddWithValue("@secondDate", secondDate.ToString());
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(0))
                            {
                                result = (reader.GetInt32(0));
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
            return result;
        }

        /// <summary>
        /// Gets the results date range (first date - last date)
        /// </summary>
        /// <param name="accountID"></param>
        /// <returns></returns>
        public static int GetDateRange(int accountID)
        {
            int result = 0;
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();
                string query = "Declare @start_date DATETIME;" +
                    "Declare @end_date DATETIME;" +
                    "set @start_date = (select min(testResultsDate) from testresults where accountID = @accountID);" +
                    "set @end_date = (select max(testResultsDate) from testresults where accountID = @accountID);" +
                    " select DATEDIFF(Hour, @start_date, @end_date);";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@accountID", accountID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(0))
                            {
                                result = (reader.GetInt32(0));
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
            return result;
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

        // Gets the wrong answers of a testResult using testResultID
        public static List<string> GetTestResultsContentWrong(int testResultID)
        {
            List<string> results = new List<string>();
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();
                string query = "Select answer from testResultContent Where testResultID = @testResultID and answerType = 1";

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

                            results.Add(new TestResult(id, date, wordsPerMin));
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
        /// <param name="testID"></param>
        /// <param name="accountID"></param>
        /// <param name="wordsEachMinute"></param>
        /// <param name="pauses"></param>
        /// <param name="rightAnswers"></param>
        /// <param name="wrongAnswers"></param>
        /// <param name="lines"></param>
        /// <returns></returns>
        public static int SaveResults(int testID, int accountID, int wordsEachMinute, int pauses, List<string> rightAnswers, Dictionary<int, string> wrongAnswers, List<string> lines, int score)
        {
            int testResultID = 0;
            SqlConnection connection = new SqlConnection(Database.connectionString);

            try
            {
                connection.Open();
                string query = "INSERT INTO testresults (testID, accountID, testResultsDate, wordsEachMinute, pauses, score) VALUES (@testID, @accountID, CURRENT_TIMESTAMP, @WordsEachMinute, @pauses, @score); Select SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@testID", testID);
                    command.Parameters.AddWithValue("@accountID", accountID);
                    command.Parameters.AddWithValue("@wordsEachMinute", wordsEachMinute);
                    command.Parameters.AddWithValue("@pauses", pauses);
                    command.Parameters.AddWithValue("@score", score);

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

            InsertResultsContent(testResultID, rightAnswers, wrongAnswers, lines);
            return testResultID;
        }

        private static void InsertResultsContent(int testResultID, List<string> rightAnswers, Dictionary<int, string> wrongAnswers, List<string> lines)
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
    }
}
