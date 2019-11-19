using Microsoft.OData.Edm;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace LerenTypen
{
    static class Database
    {
        private static string connectionn = "Server=localhost;Database=quicklylearningtyping;Uid=root;";

       
        public static void Registreer(string username, string password, DateTime birthday, string firstname, string lastname, string securityvraag, string securityanswer)
        {
            try
            {
                Date res = birthday.Date;
                String query = "INSERT INTO accounts(accountType , accountUsername , accountPassword , accountBirthdate , accountFirstname, accountSurname , AccountSecurityQuestion , AccountSecurityAnswer , archived) VALUES (0 , @username, @pwhash, @bday, @fname, @lname,  @secvraag, @secans, 0)";

                using (SqlConnection connection = new SqlConnection(connectionn))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    //a shorter syntax to adding parameters
                    command.Parameters.AddWithValue("@username", username.ToString());
                    command.Parameters.AddWithValue("@pwhash", password.ToString());
                    command.Parameters.AddWithValue("@bday", res.ToString());
                    command.Parameters.AddWithValue("@fname", firstname.ToString());
                    command.Parameters.AddWithValue("@lname", lastname.ToString());
                    command.Parameters.AddWithValue("@secvraag", securityvraag.ToString());
                    command.Parameters.AddWithValue("@secans", securityanswer.ToString());
                    

                    //make sure you open and close(after executing) the connection
                    connection.Open();
                    command.ExecuteNonQuery();
                    }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
