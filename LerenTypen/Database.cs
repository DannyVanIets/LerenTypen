using Microsoft.OData.Edm;
using MySql.Data.MySqlClient;
using System;
using System.Data.SqlClient;
using System.Text;

namespace LerenTypen
{
    static class Database
    {
        private static string connectionString = "Server=localhost;Database=quicklylearningtyping;Uid=root;";

        public static bool UserExists(string user)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    String query = "Select @username from accounts";
                    using (MySqlCommand username = new MySqlCommand(query, connection))
                    {
                        username.Parameters.AddWithValue("@username", user);
                        connection.Open();
                        using (MySqlDataReader reader = username.ExecuteReader())
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
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    String query = "INSERT INTO accounts(accountType , accountUsername , accountPassword , accountBirthdate , accountFirstname, accountSurname , AccountSecurityQuestion , AccountSecurityAnswer , archived) VALUES (0 , @username, @pwhash, @bday, @fname, @lname,  @secvraag, @secans, 0)";

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
    }
}
