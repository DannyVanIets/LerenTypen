using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using LerenTypen.Models;

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
        public static void TestQuery()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("INSERT INTO `testcontent` (`testContentID`, `testID`, `content`) VALUES (NULL, '1', 'Klaas-jan');");
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
                    connection.Close();
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static bool UserExists(string user)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = "Select accountUsername from accounts where accountUsername = @username";
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
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
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
                Console.WriteLine(e.Message);
                return 0;
            }
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
                Console.WriteLine(e.Message);
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
                    string mySql = sb.ToString();

                    using (MySqlCommand command = new MySqlCommand(mySql, connection))
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
                Console.WriteLine(e.Message);
            }

            return title;
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

                    sb.Append("Select wordsEachMinute, pauses from testresults where testResultID = @testResultID");

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
                            }
                        }
                    }
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
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
                Console.WriteLine(e.Message);
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
                Console.WriteLine(e.Message);
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
                Console.WriteLine(e.Message);
            }
            return results;
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
                Console.WriteLine(e.Message);
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
                Console.WriteLine(e.Message);
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
                    Console.WriteLine(e.Message);
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
                    Console.WriteLine(e.Message);
                }
            }
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
                Console.WriteLine(e.Message);
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
                Console.WriteLine(e.Message);
            }
            return result;
        }

        //Database query used by login. We're gonna check if the username and hashedpassword match any existing data. If it does, we will return the accountID.
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
                    connection.Close();
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
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
                    connection.Close();
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return "";
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
                Console.WriteLine(e.Message);
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
