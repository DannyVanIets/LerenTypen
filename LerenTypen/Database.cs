using Microsoft.OData.Edm;
using System;
using System.Data.SqlClient;
using System.Text;

namespace LerenTypen
{
    static class Database
    {
        private static string connectionString = "Data Source=127.0.0.1,1433;User Id=qlt;Password=MvBg2T-{K[Vh;Database=quicklylearningtyping;";
      
        public static bool UserExists(string user)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    String query = "Select accountUsername from accounts where accountUsername = @username";
                    using (SqlCommand usernamecheck = new SqlCommand(query, connection))
                    {
                        usernamecheck.Parameters.AddWithValue("@username", user);
                        connection.Open();
                        using (SqlDataReader reader = usernamecheck.ExecuteReader())
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

        public static void Registrer(string username, string password, DateTime birthday, string firstname, string lastname, string securityvraag, string securityanswer)
        {
            Date res = birthday.Date;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO accounts(accountType, accountUsername, accountPassword, accountBirthdate, accountFirstname, accountSurname, AccountSecurityQuestion, " +
                        "AccountSecurityAnswer, archived) VALUES (0 , @username, @pwhash, @bday, @fname, @lname,  @secvraag, @secans, 0)";

                    using (SqlCommand command = new SqlCommand(query, connection))
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
        
        //Database query used by login. We're gonna check if the username and hashedpassword match any existing data. If it does, we will return the accountID.
        public static int GetAccountIDForLogin(string accountUsername, string password)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();
                    StringBuilder sb = new StringBuilder();

                    // We only return the accountID, in case the logged in user's account information changes. That way nothing that may change is stored. ID's will always stay the same. We also check if the account is not archived.
                    sb.Append($"SELECT accountID FROM accounts WHERE accountUsername = @accountusername AND accountPassword = @accountpassword AND archived = 0;");

                    string Sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(Sql, connection))
                    {
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
                }
            }
            catch (SqlException e)
            {
                System.Console.WriteLine(e.Message);
            }
            return 0;
        }
      
        public static string GetAccountUsername(int accountID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append($"SELECT `accountUsername` FROM accounts WHERE accountID = @id;");

                    string Sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(Sql, connection))
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
            }
            catch (SqlException e)
            {
                System.Console.WriteLine(e.Message);
            }
            return "";
        }

    }
}
