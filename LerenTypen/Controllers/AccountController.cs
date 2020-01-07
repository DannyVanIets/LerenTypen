using LerenTypen.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace LerenTypen.Controllers
{
    //In this controller we have all the Account queries we use in this appliciaton.
    public class AccountController
    {
        //Get all the account information, except for password.
        public static Account GetAllAccountInformation(int accountID)
        {
            Account account = new Account();
            SqlConnection connection = new SqlConnection(Database.connectionString);

            try
            {
                connection.Open();
                string query = "SELECT accountUsername, accountBirthdate, accountFirstName, accountSurname, accountSecurityQuestion, accountSecurityAnswer FROM accounts WHERE accountID = @id AND archived = 0";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", accountID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            account = new Account((string)reader[0], (DateTime)reader[1], (string)reader[2], (string)reader[3], (string)reader[4], (string)reader[5]);
                        }
                    }
                }
                return account;
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
            return null;
        }


        public static int GetAmountOfAdmins(int accountID)
        {
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();
                string query = "Select count(accountID) from accounts where accountType = @id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", accountID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return Convert.ToInt32(reader[0]);
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
            return 1;
        }


        /// <summary>
        /// Get an user by its id
        /// </summary>
        public static Account GetUserAccount(int accountID)
        {
            Account account = new Account();
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();
                string query = "SELECT accountUsername, accountBirthdate, accountFirstName, accountSurname FROM accounts WHERE accountID = @id AND archived = 0";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", accountID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            account = new Account((string)reader[0], (DateTime)reader[1], (string)reader[2], (string)reader[3]);
                        }
                    }
                }
                return account;
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
            return null;
        }

        /// <summary>
        /// Get an account's username, birthdate, firstname and surname.
        /// </summary>
        public static Account GetAccountNamesAndBirthdate(int accountID)
        {
            Account account = new Account();
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();
                string query = "SELECT accountUsername, accountBirthdate, accountFirstName, accountSurname FROM accounts WHERE accountID = @id AND archived = 0";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", accountID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            account = new Account((string)reader[0], (DateTime)reader[1], (string)reader[2], (string)reader[3]);
                        }
                    }
                }
                return account;
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
            return null;
        }

        /// <summary>
        /// function that returns a username based on the accountID
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public static string GetUsername(int accountId)
        {
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();
                string query = "SELECT accountUsername FROM accounts WHERE accountID = @accountID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@accountID", accountId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return reader.GetString(0);
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
            return null;
        }


        //In this query we will get all the information from one account. This is used in EditAccountPage to fill in the textboxes with the existing information.
        //Password, type and archived is not selected.
        public static Account GetAllAccountInformationExceptPassword(int accountID)
        {
            // Everything will be stored in the class Account so that it can be easily used later in the EditAccountPage.
            Account account = new Account();

            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();
                string query = "SELECT accountUsername, accountBirthdate, accountFirstName, accountSurname, accountSecurityQuestion, accountSecurityAnswer FROM accounts WHERE accountID = @id AND archived = 0";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", accountID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //We convert everything to strings, except for the birthdate so we can easily store it in a textbox.
                            return new Account((string)reader[0], (DateTime)reader[1], (string)reader[2], (string)reader[3], (string)reader[4], (string)reader[5]);
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
            return null;
        }

        //In this query we will get the hashed password from an account. This is used in EditAccountPage to check if the old password is correctly filled in before
        //we update the password with a new one.
        public static string GetPasswordFromAccount(int accountID)
        {
            SqlConnection connection = new SqlConnection(Database.connectionString);

            try
            {
                connection.Open();
                string query = "SELECT accountPassword FROM accounts WHERE accountID = @id AND archived = 0";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", accountID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return reader[0].ToString();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
            return null;
        }

        //In this query we will update an account with everything from EditAccountPage, except the password.
        //We also return a bool to check if the account is succesfully updated or not.
        public static bool UpdateAccount(int accountID, string userName, DateTime birthday, string firstName, string surname, string securityQuestion, string securityAnswer)
        {
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();
                string query = "UPDATE accounts SET accountUsername = @username, accountBirthdate = @birthday, accountFirstname = @firstname, accountSurname = @surname, " +
                    "AccountSecurityQuestion = @securityquestion, AccountSecurityAnswer = @securityanswer WHERE accountID = @id AND archived = 0";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", accountID);
                    command.Parameters.AddWithValue("@username", userName);
                    command.Parameters.AddWithValue("@birthday", birthday);

                    command.Parameters.AddWithValue("@firstname", firstName);
                    command.Parameters.AddWithValue("@surname", surname);

                    command.Parameters.AddWithValue("@securityquestion", securityQuestion);
                    command.Parameters.AddWithValue("@securityanswer", securityAnswer);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
            return true;
        }

        public static bool UpdateAccount(string userName, string firstName, string surname)
        {
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();
                string query = "UPDATE accounts SET accountFirstname = @firstname, accountSurname = @surname, accountUsername = @username WHERE accountID = @id AND archived = 0";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", GetAccountIDFromUsername(userName));
                    command.Parameters.AddWithValue("@firstname", firstName);
                    command.Parameters.AddWithValue("@surname", surname);
                    command.Parameters.AddWithValue("@username", userName);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
            return true;
        }

        //Same query as UpdateAccountWithoutPassword, except this one also updated the password. Also used in EditAccountPage.
        //We also return a bool to check if the account is succesfully updated or not.
        public static bool UpdateAccountWithPassword(int accountID, string userName, string password, DateTime birthday, string firstName, string surname, string securityQuestion, string securityAnswer)
        {
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();
                string query = "UPDATE accounts SET accountUsername = @username, accountPassword = @password, accountBirthdate = @birthday, accountFirstname = @firstname, " +
                    "accountSurname = @surname, AccountSecurityQuestion = @securityquestion, AccountSecurityAnswer = @securityanswer WHERE accountID = @id AND archived = 0";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", accountID);
                    command.Parameters.AddWithValue("@username", userName);
                    command.Parameters.AddWithValue("@password", password);

                    command.Parameters.AddWithValue("@birthday", birthday);
                    command.Parameters.AddWithValue("@firstname", firstName);
                    command.Parameters.AddWithValue("@surname", surname);

                    command.Parameters.AddWithValue("@securityquestion", securityQuestion);
                    command.Parameters.AddWithValue("@securityanswer", securityAnswer);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
            return true;
        }

        public static int GetAccountIDFromUsername(string accountUsername)
        {
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();
                // We only return the accountID, in case the logged in user's account information changes. That way nothing that may change is stored. ID's will always stay the same. We also check if the account is not archived.
                string query = "SELECT accountID FROM accounts WHERE accountUsername = @accountusername AND archived = 0";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@accountusername", accountUsername);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return int.Parse(reader[0].ToString());
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
            return 0;
        }

        public static List<UserTable> GetLast3TestsMade()
        {
            List<UserTable> queryResult = new List<UserTable>();
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();
                string query = "select accountID, accountUsername, accountType, accountFirstname, accountSurname from accounts where archived = 0";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            queryResult.Add(new UserTable(reader.GetInt32(0), reader.GetString(1), reader.GetInt16(2), reader.GetString(3), reader.GetString(4)));
                        }
                    }
                }
                return queryResult;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }

        public static List<UserTable> GetAllUsers()
        {
            List<UserTable> queryResult = new List<UserTable>();
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();
                string query = "select accountID, accountUsername, accountType, accountFirstname, accountSurname from accounts where archived = 0";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            queryResult.Add(new UserTable(reader.GetInt32(0), reader.GetString(1), reader.GetInt16(2), reader.GetString(3), reader.GetString(4)));
                        }
                    }
                }
                return queryResult;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }

        // Check if person is admin
        public static bool IsAdmin(int accountnumber)
        {
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                string query = "Select accountUsername from accounts where accountType = 2 and accountID = @accountnumber and archived =0";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@accountnumber", accountnumber);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
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
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
            return false;
        }

        // Check if person is teacher
        public static bool IsTeacher(int accountnumber)
        {
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                string query = "Select accountUsername from accounts where accountType = 1 and accountID = @accountnumber and archived =0";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@accountnumber", accountnumber);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
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
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
            return false;
        }

        // Make the user Admin
        public static bool MakeAdmin(string userName)
        {
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();
                string query = "UPDATE accounts SET accountType = 2 WHERE accountID = @id AND archived = 0";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", GetAccountIDFromUsername(userName));
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
            return true;
        }

        // Make the user a student
        public static bool MakeStudent(string userName)
        {
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();
                string query = "UPDATE accounts SET accountType = 0 WHERE accountID = @id AND archived = 0";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", GetAccountIDFromUsername(userName));
                    command.ExecuteNonQuery();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
            return true;
        }

        // Make the user teacher        
        public static bool MakeTeacher(string userName)
        {
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();
                string MySql = "UPDATE accounts SET accountType = 1 WHERE accountID = @id AND archived = 0";

                using (SqlCommand command = new SqlCommand(MySql, connection))
                {
                    command.Parameters.AddWithValue("@id", GetAccountIDFromUsername(userName));
                    command.ExecuteNonQuery();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
            return true;
        }

        public static string GetAverageTestResultpercentage(string userName)
        {
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();
                string query = "select avg(testresults.score) from testresults where accountID = @id and finished = 1";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", AccountController.GetAccountIDFromUsername(userName));

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return reader[0].ToString();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
            return null;
        }


        public static int GetAverageWordsMinute(string userName)
        {
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();
                string query = "select avg(wordsEachMinute) from testresults r inner join tests t on r.testID = t.testID where r.accountID = @id and r.finished = 1 and t.archived=0";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", AccountController.GetAccountIDFromUsername(userName));
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(0))
                            {
                                return Convert.ToInt32(reader[0]);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
            return 0;
        }

        public static bool DeleteAccount(string userName)
        {
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();
                string query = "UPDATE accounts set archived=1 WHERE accountID = @id AND archived = 0";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", AccountController.GetAccountIDFromUsername(userName));
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                return false;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
            return true;
        }
    }
}
