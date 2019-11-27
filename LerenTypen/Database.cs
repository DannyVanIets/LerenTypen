using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

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
                    // NOW() is being used to get the local date.
                    sb.Append($"INSERT INTO tests (testName, testType, archived, testDifficulty, createDate, isPrivate, accountID) " +
                        $"VALUES (@testName, @testType, 0, @testDifficulty, NOW(), @isPrivate , @uploadedBy); SELECT LAST_INSERT_ID()");
                    
                    string MySql = sb.ToString();

                    using (MySqlCommand command = new MySqlCommand(MySql, connection))
                    {
                        command.Parameters.AddWithValue("@testName", testName);
                        command.Parameters.AddWithValue("@testType", testType);
                        command.Parameters.AddWithValue("@testDifficulty", testDifficulty);
                        command.Parameters.AddWithValue("@isPrivate", isPrivate);                       
                        command.Parameters.AddWithValue("@uploadedBy", uploadedBy);

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
        /// <summary>
        /// Method adds each line of content of a test to database using its tests ID. testcontent is stored in a separate db.
        /// </summary>        
        private static void AddTestContent(int testID, List<string> content)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    
                    foreach (string contentLine in content)
                    {
                        string MySql = $"INSERT INTO testContent (testID, content) VALUES (@testID,@contentLine);";

                        using (MySqlCommand command = new MySqlCommand(MySql, connection))
                        {
                            command.Parameters.AddWithValue("@testID", testID);
                            command.Parameters.AddWithValue("@contentLine", contentLine);                           
                           
                        }
                    }
                }
            }
            catch (MySqlException e)
            {
                System.Console.WriteLine(e.Message);
            }
        }

        public static List<string> GetAccountType(int accountID)
        {
            List<string> resultset = new List<string>();
            try

            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("select accountType from accounts Where accountID = @accountID");
                    string MySql = sb.ToString();

                    using (MySqlCommand command = new MySqlCommand(MySql, connection))
                    {
                        command.Parameters.AddWithValue("@accountID", accountID);

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
        /// <summary>
        /// Returns every test in the database
        /// </summary>
        /// <returns></returns>
        public static List<TestTable> GetAllTests()
        {
            List<TestTable> queryResult = new List<TestTable>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("select testID, t.accountID, testName, t.testDifficulty, timesMade, highscore, a.accountUsername from tests t Inner join accounts a on t.accountID=a.accountID where t.archived=0 and a.archived=0 and t.isPrivate=0;");
                    string MySql = sb.ToString();
                    int counter = 1;

                    using (MySqlCommand command = new MySqlCommand(MySql, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    // Adds all the found data to a list
                                    queryResult.Add(new TestTable(counter, reader.GetString(2), reader.GetInt32(4), reader.GetInt32(5), GetAmountOfWordsFromTest(reader.GetInt32(0)), reader.GetInt32(3), reader.GetString(6)));
                                    counter++;
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

        /// <summary>
        /// Get the amount of words from the contentTable for each test
        /// </summary>
        /// <param name="testId"></param>
        /// <returns></returns>
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

                    // This query returns all the content from a given testId
                    sb.Append("SELECT content FROM testcontent WHERE testID=" + testId);

                    string MySql = sb.ToString();

                    using (MySqlCommand command = new MySqlCommand(MySql, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
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
                }
                return amountOfWords;
            }
            catch (MySqlException e)
            {
                System.Console.WriteLine(e.Message);
                return 0;
            }
        }

        // Database query used by login. We're gonna check if the username and hashedpassword match any existing data. If it does, we will return the accountID.
        public static int GetAccountIDForLogin(string accountUsername, string password)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();

                    // We only return the accountID, in case the logged in user's account information changes. That way nothing that may change is stored. ID's will always stay the same. We also check if the account is not archived.
                    sb.Append($"SELECT `accountID` FROM accounts WHERE accountUsername = @accountusername AND accountPassword = @accountpassword AND archived = 0;");

                    string MySql = sb.ToString();

                    using (MySqlCommand command = new MySqlCommand(MySql, connection))
                    {
                        command.Parameters.AddWithValue("@accountusername", accountUsername);
                        command.Parameters.AddWithValue("@accountpassword", password);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                return int.Parse(reader[0].ToString());
                            }
                        }
                    }
                }
            }
            catch (MySqlException e)
            {
                System.Console.WriteLine(e.Message);
            }
            return 0;
        }

        /// <summary>
        /// function that returns a username based on the accountID
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public static string GetUserName(int accountId)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();

                    sb.Append($"SELECT `accountUsername` FROM accounts WHERE accountID = @accountID AND archived = 0;");

                    string MySql = sb.ToString();

                    using (MySqlCommand command = new MySqlCommand(MySql, connection))
                    {
                        command.Parameters.AddWithValue("@accountID", accountId);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                return reader.GetString(0);
                            }
                        }
                    }
                }
            }
            catch (MySqlException e)
            {
                System.Console.WriteLine(e.Message);
            }
            return null;
        }

        /// <summary>
        /// Returns all the tests that have been previously made by the user
        /// </summary>
        /// <param name="ingelogd"></param>
        /// <returns></returns>
        public static List<TestTable> GetAllTestsAlreadyMade(int ingelogd)
        {
            List<TestTable> queryResult = new List<TestTable>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    // This query joins the info needed for the testtable with accounts to find the corresponding username and with testresults to find out if a test has been made before by the user
                    sb.Append("select t.testID, t.accountID, testName, t.testDifficulty, timesMade, highscore, a.accountUsername from tests t Inner join accounts a on t.accountID=a.accountID inner join testresults tr on tr.testID=t.testID where tr.accountID = @accountID and t.archived=0 and a.archived=0 and t.isPrivate=0;");
                    
                    string MySql = sb.ToString();
                    int counter = 1;

                    using (MySqlCommand command = new MySqlCommand(MySql, connection))
                    {

                       command.Parameters.AddWithValue("@accountID", ingelogd);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    // Add all the found data to a list
                                    queryResult.Add(new TestTable(counter, reader.GetString(2), reader.GetInt32(4), reader.GetInt32(5), GetAmountOfWordsFromTest(reader.GetInt32(0)), reader.GetInt32(3), reader.GetString(6)));
                                    counter++;
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

    }
}
