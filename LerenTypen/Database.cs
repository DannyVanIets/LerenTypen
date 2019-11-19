using Microsoft.OData.Edm;
using MySql.Data.MySqlClient;
using System;
using System.Text;

namespace LerenTypen
{
    static class Database
    {
        private static string connectionString = "Server=localhost;Database=quicklylearningtyping;Uid=root;";

        public static void Registreer(string username, string password, DateTime birthday, string firstname, string lastname, string securityvraag, string securityanswer)
        {
            Date res = birthday.Date;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append($"INSERT INTO accounts(accountType , accountUsername , accountPassword , accountBirthdate , accountFirstname, accountSurname , AccountSecurityQuestion , AccountSecurityAnswer , archived) VALUES (0 , '{username}','{password}', '{res}', '{firstname}','{lastname}',  '{securityvraag}', '{securityanswer}', 0)");
                    string MySql = sb.ToString();
                    using (MySqlCommand command = new MySqlCommand(MySql, connection))
                    {

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                        }

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
