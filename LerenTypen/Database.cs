using MySql.Data.MySqlClient;
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
        public static string GetAccountUsername(int accountID)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append($"SELECT `accountUsername` FROM accounts WHERE accountID = @id;");

                    string MySql = sb.ToString();

                    using (MySqlCommand command = new MySqlCommand(MySql, connection))
                    {
                        command.Parameters.AddWithValue("@id", accountID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                return reader[0].ToString();
                            }
                        }
                    }
                }
            }
            catch (MySqlException e)
            {
                System.Console.WriteLine(e.Message);
            }
            return "";
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

                    // this query returns all the content from a given testId
                    sb.Append("SELECT content FROM testcontent WHERE testID=" + testId);

                    string MySql = sb.ToString();

                    using (MySqlCommand command = new MySqlCommand(MySql, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                fullResult = reader.GetString(0);

                                // checks the string for any excess spaces and deletes them
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

        public static Test GetTest(int testID)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("SELECT testName, testType, accountID, timesMade, highscore, version, testDifficulty, isPrivate, createDate");
                    string MySql = sb.ToString();

                    using (MySqlCommand command = new MySqlCommand(MySql, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string testName = reader[0].ToString();
                                int testType = (int)reader[1];
                                string authorUsername = GetAccountUsername((int)reader[2]);
                                int wordCount = GetAmountOfWordsFromTest(testID);
                                int timesMade = (int)reader[3];
                                double highscore = (double)reader[4];
                                int version = (int)reader[5];
                                int testDifficulty = (int)reader[5];
                                bool isPrivate = (bool)reader[6];
                                string createdDateTime = (string)reader[7];

                                return new Test(testName, testType, authorUsername, wordCount, timesMade, 0, highscore, version, testDifficulty, isPrivate, createdDateTime);
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
    }
}
