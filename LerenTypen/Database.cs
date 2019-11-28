using System.Collections.Generic;
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

        //Hier worden alle users opgehaald.
        public static List<Users> GetUsers()
        {
            List<Users> queryResult = new List<Users>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("select accountID,accountUsername, accountType, accountFirstname , accountSurname from accounts");
                    string MySql = sb.ToString();

                    using (MySqlCommand command = new MySqlCommand(MySql, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    queryResult.Add(new Users(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3), reader.GetString(4), "Edit"));
                                }
                                reader.NextResult();
                            }
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

            public static void Registrer(string username, string password, DateTime birthday, string firstname, string lastname, string securityvraag, string securityanswer)
            {
                Date res = birthday.Date;

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
        }
    }
