using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace LerenTypen
{
    static class Database
    {
        private static MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder()
        {
            Server = "localhost",
            UserID = "root",
            Password = "",
            Database = "quicklylearningtyping"
        };

        private static string connectionString = "Server=localhost;Database=quicklylearningtyping;Uid=root;";

        public static void TestQuery()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("INSERT INTO tests VALUES (2, 'Test', 'words', 1, 0)");
                    string MySql = sb.ToString();

                    using (MySqlCommand command = new MySqlCommand(MySql, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            //while (reader.Read())
                            //{

                            //}
                        }
                    }
                }
            }
            catch (MySqlException e)
            {
                System.Console.WriteLine(e.Message);
            }
        }

        public static string GetUserName(int accountID)
        {
            string result = null;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("select accountUsername from accounts where accountID = @accountID");
                    string MySql = sb.ToString();

                    using (MySqlCommand command = new MySqlCommand(MySql, connection))
                    {
                        command.Parameters.AddWithValue("@accountID", accountID);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result = reader.GetString(0);
                            }
                        }
                    }
                }
            }
            catch (MySqlException e)
            {
                System.Console.WriteLine(e.Message);
            }
            return result;
        }

        /// <summary>
        /// Gets the accountID of the testcreater and the testDifficulty
        /// </summary>
        /// <param name="testID"></param>
        /// <returns></returns>
        public static List<int> GetTestInformation(int testID)
        {
            List<int> results = new List<int>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Select accountID, testDifficulty from tests where testID = @testID");
                    string MySql = sb.ToString();

                    using (MySqlCommand command = new MySqlCommand(MySql, connection))
                    {
                        command.Parameters.AddWithValue("@testID", testID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var accountID = reader["accountID"];
                                var testDifficulty = reader["testDifficulty"];

                                results.Add((int)accountID);
                                results.Add((int)testDifficulty);
                            }
                        }
                    }
                }
            }
            catch (MySqlException e)
            {
                System.Console.WriteLine(e.Message);
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
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Select testName from tests where testID = @testID");
                    string MySql = sb.ToString();

                    using (MySqlCommand command = new MySqlCommand(MySql, connection))
                    {
                        command.Parameters.AddWithValue("@testID", testID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                title = reader.GetString(0);
                            }
                        }
                    }
                }
            }
            catch (MySqlException e)
            {
                System.Console.WriteLine(e.Message);
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
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Select content from testContent Where testID = @testID");
                    string MySql = sb.ToString();

                    using (MySqlCommand command = new MySqlCommand(MySql, connection))
                    {
                        command.Parameters.AddWithValue("@testID", testID);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                results.Add(reader.GetString(0));
                            }
                        }
                    }
                }
            }
            catch (MySqlException e)
            {
                System.Console.WriteLine(e.Message);
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
        public static Int32 InsertResults(int testID, int accountID, int wordsEachMinute, int pauses, List<string> rightAnswers, Dictionary<int, string> wrongAnswers, List<string> lines, int score)
        {
            Int32 testResultID = 0;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("INSERT INTO testresults (testID, accountID, testResultsDate, wordsEachMinute, pauses, score) VALUES (@testID, @accountID, NOW(), @WordsEachMinute, @pauses, @score); Select LAST_INSERT_ID();");
                    string MySql = sb.ToString();

                    using (MySqlCommand command = new MySqlCommand(MySql, connection))
                    {
                        command.Parameters.AddWithValue("@testID", testID);
                        command.Parameters.AddWithValue("@accountID", accountID);
                        command.Parameters.AddWithValue("@wordsEachMinute", wordsEachMinute);
                        command.Parameters.AddWithValue("@pauses", pauses);
                        command.Parameters.AddWithValue("@score", score);

                        testResultID = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
            }
            catch (MySqlException e)
            {
                System.Console.WriteLine(e.Message);
            }
            InsertResultsContent(testResultID, rightAnswers, wrongAnswers, lines);
            return testResultID;
        }

        public static void InsertResultsContent(Int32 testResultID, List<string> rightAnswers, Dictionary<int, string> wrongAnswers, List<string> lines)
        {
            foreach (string rightAnswer in rightAnswers)
            {
                try
                {
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();
                        StringBuilder sb = new StringBuilder();
                        sb.Append("INSERT INTO testresultcontent (testResultID, answer, answerType) VALUES (@testResultID, @answer, @answerType)");
                        string MySql = sb.ToString();

                        using (MySqlCommand command = new MySqlCommand(MySql, connection))
                        {
                            command.Parameters.AddWithValue("@testResultID", testResultID);
                            command.Parameters.AddWithValue("@answer", rightAnswer);
                            command.Parameters.AddWithValue("@answerType", 0);
                            command.ExecuteNonQuery();
                        }
                    }

                }
                catch (MySqlException e)
                {
                    System.Console.WriteLine(e.Message);
                }
            }
            // Position of right answer in lines is stored in keyvaluepairs int
            foreach (KeyValuePair<int, string> wrongAnswer in wrongAnswers)
            {
                try
                {
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();
                        StringBuilder sb = new StringBuilder();
                        sb.Append("INSERT INTO testresultcontent (testResultID, answer, answerType, rightAnswer) VALUES (@testResultID, @answer, @answerType, @rightAnswer)");
                        string MySql = sb.ToString();

                        using (MySqlCommand command = new MySqlCommand(MySql, connection))
                        {
                            command.Parameters.AddWithValue("@testResultID", testResultID);
                            command.Parameters.AddWithValue("@answer", wrongAnswer.Value);
                            command.Parameters.AddWithValue("@answerType", 1);
                            command.Parameters.AddWithValue("@rightAnswer", lines[wrongAnswer.Key]);
                            command.ExecuteNonQuery();
                        }
                    }
                }
                catch (MySqlException e)
                {
                    System.Console.WriteLine(e.Message);
                }
            }
        }

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
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();

                    sb.Append("Select wordsEachMinute, pauses, score from testresults where testResultID = @testResultID");

                    string MySql = sb.ToString();

                    using (MySqlCommand command = new MySqlCommand(MySql, connection))
                    {
                        command.Parameters.AddWithValue("@testResultID", testResultsID);
                        using (MySqlDataReader reader = command.ExecuteReader())
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
            }
            catch (MySqlException e)
            {
                System.Console.WriteLine(e.Message);
            }
            return results;
        }

        public static void UpdateTimesMade(int testID)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    Console.WriteLine(testID);
                    sb.Append("UPDATE tests SET timesMade = timesMade + 1 WHERE testID = @testID");
                    string MySql = sb.ToString();

                    using (MySqlCommand command = new MySqlCommand(MySql, connection))
                    {
                        command.Parameters.AddWithValue("@testID", testID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException e)
            {
                System.Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Gets the right answers of a testResult using testResultID
        /// </summary>
        /// <param name="testResultID"></param>
        /// <returns></returns>
        public static List<string> GetTestResultsContentRight(int testResultID)
        {
            List<string> results = new List<string>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Select answer from testResultContent Where testResultID = @testResultID and answerType = 0");
                    string MySql = sb.ToString();

                    using (MySqlCommand command = new MySqlCommand(MySql, connection))
                    {
                        command.Parameters.AddWithValue("@testResultID", testResultID);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                results.Add(reader["answer"].ToString());
                            }
                        }
                    }
                }
            }
            catch (MySqlException e)
            {
                System.Console.WriteLine(e.Message);
            }
            return results;
        }

        // Gets the wrong answers of a testResult using testResultID
        public static List<string> GetTestResultsContentWrong(int testResultID)
        {
            List<string> results = new List<string>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Select answer from testResultContent Where testResultID = @testResultID and answerType = 1");
                    string MySql = sb.ToString();

                    using (MySqlCommand command = new MySqlCommand(MySql, connection))
                    {
                        command.Parameters.AddWithValue("@testResultID", testResultID);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                results.Add(reader["answer"].ToString());
                            }
                        }
                    }
                }
            }
            catch (MySqlException e)
            {
                System.Console.WriteLine(e.Message);
            }
            return results;
        }

        // Gets the answers the wrong answers had to be
        public static List<string> GetTestResultsContentHadToBe(int testResultID)
        {
            List<string> results = new List<string>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Select rightAnswer from testResultContent Where testResultID = @testResultID and answerType = 1");
                    string MySql = sb.ToString();

                    using (MySqlCommand command = new MySqlCommand(MySql, connection))
                    {
                        command.Parameters.AddWithValue("@testResultID", testResultID);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                results.Add(reader["rightAnswer"].ToString());
                            }
                        }
                    }
                }
            }
            catch (MySqlException e)
            {
                System.Console.WriteLine(e.Message);
            }
            return results;
        }
    }
}
