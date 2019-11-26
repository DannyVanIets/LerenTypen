using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace LerenTypen
{
    static class Database { 
        private static MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder()
        {
            Server = "localhost", UserID = "root", Password = "", Database = "quicklylearningtyping"
        };

        private static string connectionString = "Server=localhost;Database=quicklylearningtyping;Uid=root;";

        public static void TestQuery() {
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
        public static void InsertResults(int testID, int accountID, int wordsEachMinute, int pauses, List<string> rightAnswers, Dictionary<int, string> wrongAnswers, List<string> lines)
        {            
            Int32 testResultID = 0;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("INSERT INTO testresults (testID, accountID, testResultsDate, wordsEachMinute, pauses) VALUES (@testID, @accountID, NOW(), @WordsEachMinute, @pauses); Select LAST_INSERT_ID();");
                    string MySql = sb.ToString();

                    using (MySqlCommand command = new MySqlCommand(MySql, connection))
                    {
                        command.Parameters.AddWithValue("@testID", testID);
                        command.Parameters.AddWithValue("@accountID", accountID);
                        command.Parameters.AddWithValue("@wordsEachMinute", wordsEachMinute);
                        command.Parameters.AddWithValue("@pauses", pauses);

                       testResultID = Convert.ToInt32(command.ExecuteScalar());


                    }
                }
            }
            catch (MySqlException e)
            {
                System.Console.WriteLine(e.Message);
            }
            InsertResultsContent(testResultID, rightAnswers, wrongAnswers, lines);
        }

        public static void InsertResultsContent(Int32 testResultID, List<string> rightAnswers, Dictionary<int, string> wrongAnswers, List<string>lines )
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
                            command.Parameters.AddWithValue("@answerType", 0);
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

        public static List<string> GetTestResults(int accountID, int testID, int testResultsID)
        {
            List<string> results = new List<string>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    
                        sb.Append("Select wordsEachMinute, pauses from testresults where testID = @testID and accountID = @accountID and testResultsID = @testResultsID");
                    
                    string MySql = sb.ToString();

                    using (MySqlCommand command = new MySqlCommand(MySql, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                results.Add(reader["wordsEachMinute"].ToString()); 
                                results.Add( reader["pauses"].ToString());
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

        public static Tuple<List<string>, int> GetTestResults(int accountID, int testID)
        {
            List<string> results = new List<string>();
            int testResultID = 0;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();

                    sb.Append("Select wordsEachMinute, pauses from testresults where testID = @testID and accountID = @accountID and testResultsID = Max(testResultsID); Select LAST_INSERT_ID()");

                    string MySql = sb.ToString();

                    using (MySqlCommand command = new MySqlCommand(MySql, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                results.Add(reader["wordsEachMinute"].ToString());
                                results.Add(reader["pauses"].ToString());

                                testResultID = int.Parse(command.ExecuteScalar().ToString());
                            }
                        }
                    }
                }
            }
            catch (MySqlException e)
            {
                System.Console.WriteLine(e.Message);
            }
            return Tuple.Create(results, testResultID);
        }

        public static List<string> GetTestResultsContent(int testResultID)
        {
            List<string> results = new List<string>();
            try
            {

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Select answer, rightAnswer from testContent Where testResultID = @testResultID");
                    string MySql = sb.ToString();

                    using (MySqlCommand command = new MySqlCommand(MySql, connection))
                    {

                        command.Parameters.AddWithValue("@testResultID", testResultID);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {                                
                                results.Add(reader["answer"].ToString());                                
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
