using MySql.Data.MySqlClient;
using System;
using System.Text;

namespace LerenTypen
{
    static class Database
    {
        private static string connectionString = "Server=localhost;Database=quicklylearningtyping;Uid=root;";


        public static void GetUsers(int userid, string username, int usertype, string firstname, string lastname)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = "select @accountid , @username, @accounttype, @fname, @lname from accounts";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        //a shorter syntax to adding parameters

                        command.Parameters.AddWithValue("@accountid", userid);
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@accounttype", usertype);
                        command.Parameters.AddWithValue("@fname", firstname);
                        command.Parameters.AddWithValue("@lname", lastname);
                        //make sure you open and close(after executing) the connection
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception e)
            {
                System.Console.WriteLine(e);
            }
        }
    }
}
