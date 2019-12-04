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
                            result.Add(reader.ToString());
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
    }
}
