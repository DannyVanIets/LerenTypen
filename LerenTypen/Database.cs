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
        /// <summary>
        /// Method for adding tests to database. 
        /// </summary>        
        public static void AddTest(string testName, int testType, int testDifficulty, int isPrivate, List<string> content, int uploadedBy)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();

                    // Select last insert id is used to insert the tests content into a seperate table with the same id
                    sb.Append($"INSERT INTO tests (testName, testType, archived, testDifficulty, createDate, isPrivate, accountID) " +
                        $"VALUES (@testName, @testType, 0, @testDifficulty, NOW(), @isPrivate , @accountID); SELECT LAST_INSERT_ID()");
                    
                    string MySql = sb.ToString();

                    using (MySqlCommand command = new MySqlCommand(MySql, connection))
                    {
                        command.Parameters.AddWithValue("@testName", testName);
                        command.Parameters.AddWithValue("@testType", testType);
                        command.Parameters.AddWithValue("@testDifficulty", testDifficulty);
                        command.Parameters.AddWithValue("@isPrivate", isPrivate);                       
                        command.Parameters.AddWithValue("@accountID", uploadedBy);

                        object testID = command.ExecuteScalar();
                        int intTestID = int.Parse(testID.ToString());

                        AddTestContent(intTestID, content);                      
                    }
                }
            }
            catch (MySqlException e)
            {
                System.Console.WriteLine(e.Message);
            }            
        }

        private static void AddTestContent(int testID, List<string> content)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    
                    foreach (string s in content)
                    {
                        string MySql = $"INSERT INTO testContent (testID, content) VALUES (@testID,@s);";

                        using (MySqlCommand command = new MySqlCommand(MySql, connection))
                        {
                            command.Parameters.AddWithValue("@testID", testID);
                            command.Parameters.AddWithValue("@s", s);
                            
                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                           
                            }
                        }
                    }
                }
            }
            catch (MySqlException e)
            {
                System.Console.WriteLine(e.Message);
            }
        }

        public static List<string> TestQuery() {
            List<string> resultset = new List<string>();
            try

            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("select testID, t.accountID, testName, testDifficulty, timesMade, highscore, a.accountUsername from tests t Inner join accounts a on t.accountID=a.accountID");
                    string MySql = sb.ToString();

                    using (MySqlCommand command = new MySqlCommand(MySql, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    resultset.Add(reader.GetString(2));

                                }
                                reader.NextResult();
                            }
                        }
                    }
                }
                return resultset;
            }
            catch (MySqlException e)
            {
                System.Console.WriteLine(e.Message);
                return null;
            }
        }

        public static List<TestTable> GetAllTests()
        {
            List<TestTable> queryResult = new List<TestTable>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("select testID, t.accountID, testName, testDifficulty, timesMade, highscore, a.accountUsername from tests t Inner join accounts a on t.accountID=a.accountID");
                    string MySql = sb.ToString();

                    using (MySqlCommand command = new MySqlCommand(MySql, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    queryResult.Add(new TestTable(0, reader.GetString(2), reader.GetInt32(4), 0, GetAmountOfWordsFromTest(reader.GetInt32(0)), reader.GetInt32(3), reader.GetString(6)));
                                }
                                reader.NextResult();
                            }
                        }
                    }
                }
                return queryResult;
            }
            catch (MySqlException e)
            {
                System.Console.WriteLine(e.Message);
                return null;
            }
        }
        public static int GetAmountOfWordsFromTest(int testId)
        {
            int amountOfWords = 0;
            string fullResult = "";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("SELECT content FROM testcontent WHERE testID=" + testId);
                    string MySql = sb.ToString();

                    using (MySqlCommand command = new MySqlCommand(MySql, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                               fullResult = reader.GetString(0);
                               amountOfWords += fullResult.Trim().Split().Length;

                            }
                        }
                    }
                }
                return amountOfWords;
            }
            catch (MySqlException e)
            {
                System.Console.WriteLine(e.Message);
                return 0;
            }
        }
    }
}
