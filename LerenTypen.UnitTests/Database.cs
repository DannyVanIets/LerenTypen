using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace LerenTypen.UnitTests
{
    class Database
    { 
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

        public static int GetFirstTestID()
        {
            SqlConnection connection = new SqlConnection(Models.Database.connectionString);
            try
            {
                connection.Open();

                // this query returns all the content from a given testId
                string mySql = "SELECT top 1 testID FROM tests";

                using (SqlCommand command = new SqlCommand(mySql, connection))
                {
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

            return 0;
        }

        public static int GetFirstAccountID()
        {
            SqlConnection connection = new SqlConnection(Models.Database.connectionString);
            try
            {
                connection.Open();

                // this query returns all the content from a given testId
                string mySql = "SELECT top 1 accountID FROM accounts";

                using (SqlCommand command = new SqlCommand(mySql, connection))
                {
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

            return 0;
        }

        public static int GetFirstAdminAccount()
        {
            SqlConnection connection = new SqlConnection(Models.Database.connectionString);
            try
            {
                connection.Open();

                // this query returns all the content from a given testId
                string mySql = "SELECT top 1 accountID FROM accounts WHERE accountType = 2";

                using (SqlCommand command = new SqlCommand(mySql, connection))
                {
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

            return 0;
        }

        public static int GetFirstTestResultID()
        {
            SqlConnection connection = new SqlConnection(Models.Database.connectionString);
            try
            {
                connection.Open();

                // this query returns all the content from a given testId
                string mySql = "SELECT top 1 testResultID FROM testresults";

                using (SqlCommand command = new SqlCommand(mySql, connection))
                {
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

            return 0;
        }

        public static int GetFirstReviewID()
        {
            SqlConnection connection = new SqlConnection(Models.Database.connectionString);
            try
            {
                connection.Open();

                // this query returns all the content from a given testId
                string mySql = "SELECT top 1 reviewID FROM testReviews";

                using (SqlCommand command = new SqlCommand(mySql, connection))
                {
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
            return 0;
        }
    }
}
