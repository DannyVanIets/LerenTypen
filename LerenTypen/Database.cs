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
        public static void AddTest(string testName, int testType, int testDifficulty, int isPrivate, int amountOfWords, List<string> content, int isTeacher)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();

                    // Select last insert id is used to insert the tests content into a seperate table with the same id
                    sb.Append($"INSERT INTO tests (testName, testType, archived, testDifficulty, uploadDate, isPrivate, amountOfWords, isTeacher) " +
                        $"VALUES (@testName, {testType}, 0, {testDifficulty},NOW(), {isPrivate}, {amountOfWords}, {isTeacher}); SELECT LAST_INSERT_ID()");
                    
                    string MySql = sb.ToString();

                    using (MySqlCommand command = new MySqlCommand(MySql, connection))
                    {
                        command.Parameters.AddWithValue("@testName", testName);
                        command.Parameters.AddWithValue("@testType", testType);
                        command.Parameters.AddWithValue("@testDifficulty", testDifficulty);
                        command.Parameters.AddWithValue("@isPrivate", isPrivate);
                        command.Parameters.AddWithValue("@amountOfWords", amountOfWords);
                        command.Parameters.AddWithValue("@isTeacher", isTeacher);

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
                        string MySql = $"INSERT INTO testContent (testID, line) VALUES (@testID,'@s');";

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
    }
}
