using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace LerenTypen.UnitTests
{
    class Database
    {
        public static string connectionString = "Data Source=127.0.0.1,1433;User Id=qlt;Password=MvBg2T-{K[Vh;Database=quicklylearningtyping;";
        public static List<string> SelectQuery(string query)
        {
            List<string> result = new List<string>();
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(reader.GetString(0));
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
            return result;
        }

        public static void Connect()
        {
            SshClient client = new SshClient("145.44.233.184", "student", "toor2019");
            try
            {
                client.Connect();
                var port = new ForwardedPortLocal("127.0.0.1", 1433, "localhost", 1433);
                client.AddForwardedPort(port);
                port.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
