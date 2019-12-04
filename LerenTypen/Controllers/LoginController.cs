using LerenTypen.Models;
using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace LerenTypen.Controllers
{
    public static class LoginController
    {
        public static string ComputeSha256Hash(string plainData)
        {
            // Create a SHA256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(plainData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static void RegisterUser(string username, string password, DateTime birthday, string firstname, string lastname, string securityvraag, string securityanswer)
        {
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                string query = "INSERT INTO accounts(accountType, accountUsername, accountPassword, accountBirthdate, accountFirstname, accountSurname, AccountSecurityQuestion, " +
                    "AccountSecurityAnswer, archived) VALUES (0 , @username, @pwhash, @bday, @fname, @lname,  @secvraag, @secans, 0)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@pwhash", password);
                    command.Parameters.AddWithValue("@bday", birthday.Date);
                    command.Parameters.AddWithValue("@fname", firstname);
                    command.Parameters.AddWithValue("@lname", lastname);
                    command.Parameters.AddWithValue("@secvraag", securityvraag);
                    command.Parameters.AddWithValue("@secans", securityanswer);

                    connection.Open();
                    command.ExecuteNonQuery();
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
        }

        public static int GetAccountIDForLogin(string accountUsername, string password)
        {
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();
                // We only return the accountID, in case the logged in user's account information changes. That way nothing that may change is stored. ID's will always stay the same. We also check if the account is not archived.
                string query = $"SELECT accountID FROM accounts WHERE accountUsername = @accountusername AND accountPassword = @accountpassword AND archived = 0";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@accountusername", accountUsername);
                command.Parameters.AddWithValue("@accountpassword", password);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return int.Parse(reader[0].ToString());
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

        public static bool UserExists(string user)
        {
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                string query = "Select accountUsername from accounts where accountUsername = @username";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", user);
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
                Console.WriteLine(e.ToString());
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
            return false;
        }
    }
}
