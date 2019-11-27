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
                            command.ExecuteNonQuery();
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
        public static bool UserExists(string user)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    String query = "Select accountUsername from accounts where accountUsername = @username";
                    using (MySqlCommand usernamecheck = new MySqlCommand(query, connection))
                    {
                        usernamecheck.Parameters.AddWithValue("@username", user);
                        connection.Open();
                        using (MySqlDataReader reader = usernamecheck.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return false;
        }

        public static void Register(string username, string password, DateTime birthday, string firstname, string lastname, string securityvraag, string securityanswer)
        {
            DateTime res = birthday.Date;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = "INSERT INTO accounts(accountType, accountUsername, accountPassword, accountBirthdate, accountFirstname, accountSurname, AccountSecurityQuestion, " +
                        "AccountSecurityAnswer, archived) VALUES (0 , @username, @pwhash, @bday, @fname, @lname,  @secvraag, @secans, 0)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        //a shorter syntax to adding parameters
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@pwhash", password);
                        command.Parameters.AddWithValue("@bday", res);
                        command.Parameters.AddWithValue("@fname", firstname);
                        command.Parameters.AddWithValue("@lname", lastname);
                        command.Parameters.AddWithValue("@secvraag", securityvraag);
                        command.Parameters.AddWithValue("@secans", securityanswer);
                        //make sure you open and close(after executing) the connection
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
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

        //In this query we will get all the information from one account. This is used in EditAccountPage to fill in the textboxes with the existing information.
        //Password, type and archived is not selected.
        public static Account GetAllAccountInformationExceptPassword(int accountID)
        {
            //Everything will be stored in the class Account so that it can be easily used later in the EditAccountPage.
            Account account = new Account();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    StringBuilder sb = new StringBuilder();
                    sb.Append("SELECT accountUsername, accountBirthdate, accountFirstName, accountSurname, accountSecurityQuestion, accountSecurityAnswer FROM accounts WHERE accountID = @id AND archived = 0");
                    string MySql = sb.ToString();

                    using (MySqlCommand command = new MySqlCommand(MySql, connection))
                    {
                        command.Parameters.AddWithValue("@id", accountID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //We convert everything to strings, except for the birthdate so we can easily store it in a textbox.
                                account = new Account((string)reader[0], (DateTime)reader[1], (string)reader[2], (string)reader[3], (string)reader[4], (string)reader[5]);
                            }
                        }
                    }
                    connection.Close();
                    return account;
                }
            }
            catch (MySqlException e)
            {
                System.Console.WriteLine(e.Message);
            }
            return null;
        }

        //In this query we will get the hashed password from an account. This is used in EditAccountPage to check if the old password is correctly filled in before
        //we update the password with a new one.
        public static string GetPasswordFromAccount(int accountID)
        {
            string results = "";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    StringBuilder sb = new StringBuilder();
                    sb.Append("SELECT accountPassword FROM accounts WHERE accountID = @id AND archived = 0");
                    string MySql = sb.ToString();

                    using (MySqlCommand command = new MySqlCommand(MySql, connection))
                    {
                        command.Parameters.AddWithValue("@id", accountID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                results = reader[0].ToString();
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return results;
        }

        //In this query we will update an account with everything from EditAccountPage, except the password.
        //We also return a bool to check if the account is succesfully updated or not.
        public static bool UpdateAccountWithoutPassword(int accountID, string userName, DateTime birthday, string firstName, string surname, string securityQuestion, string securityAnswer)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    StringBuilder sb = new StringBuilder();
                    sb.Append("UPDATE accounts SET accountUsername = @username, accountBirthdate = @birthday, accountFirstname = @firstname, accountSurname = @surname, " +
                        "AccountSecurityQuestion = @securityquestion, AccountSecurityAnswer = @securityanswer WHERE accountID = @id AND archived = 0");
                    string MySql = sb.ToString();

                    using (MySqlCommand command = new MySqlCommand(MySql, connection))
                    {
                        command.Parameters.AddWithValue("@id", accountID);
                        command.Parameters.AddWithValue("@username", userName);
                        command.Parameters.AddWithValue("@birthday", birthday);

                        command.Parameters.AddWithValue("@firstname", firstName);
                        command.Parameters.AddWithValue("@surname", surname);

                        command.Parameters.AddWithValue("@securityquestion", securityQuestion);
                        command.Parameters.AddWithValue("@securityanswer", securityAnswer);

                        //command.executeReader() is used to launch the query to the database.
                        command.ExecuteReader();
                    }
                    connection.Close();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return false;
        }

        //Same query as UpdateAccountWithoutPassword, except this one also updated the password. Also used in EditAccountPage.
        //We also return a bool to check if the account is succesfully updated or not.
        public static bool UpdateAccountWithPassword(int accountID, string userName, string password, DateTime birthday, string firstName, string surname, string securityQuestion, string securityAnswer)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    StringBuilder sb = new StringBuilder();
                    sb.Append("UPDATE accounts SET accountUsername = @username, accountPassword = @password, accountBirthdate = @birthday, accountFirstname = @firstname, " +
                        "accountSurname = @surname, AccountSecurityQuestion = @securityquestion, AccountSecurityAnswer = @securityanswer WHERE accountID = @id AND archived = 0");
                    string MySql = sb.ToString();

                    using (MySqlCommand command = new MySqlCommand(MySql, connection))
                    {
                        command.Parameters.AddWithValue("@id", accountID);
                        command.Parameters.AddWithValue("@username", userName);
                        command.Parameters.AddWithValue("@password", password);

                        command.Parameters.AddWithValue("@birthday", birthday);
                        command.Parameters.AddWithValue("@firstname", firstName);
                        command.Parameters.AddWithValue("@surname", surname);

                        command.Parameters.AddWithValue("@securityquestion", securityQuestion);
                        command.Parameters.AddWithValue("@securityanswer", securityAnswer);

                        command.ExecuteReader();
                    }
                    connection.Close();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return false;
        }
    }
}