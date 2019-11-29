using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using LerenTypen.Models;

namespace LerenTypen
{
    static class Database
    {
        private static string connectionString = "Server=localhost;Database=quicklylearningtyping;Uid=root;";

        public static int GetAccountIDForLogin(string accountUsername, string password)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    // We only return the accountID, in case the logged in user's account information changes. That way nothing that may change is stored. ID's will always stay the same. We also check if the account is not archived.
                    sb.Append($"SELECT `accountID` FROM accounts WHERE accountUsername = @accountusername AND accountPassword = @accountpassword AND archived = 0;");
                    string MySql = sb.ToString();
                    using (MySqlCommand command = new MySqlCommand(MySql, connection))
                    {
                        command.Parameters.AddWithValue("@accountusername", accountUsername);
                        command.Parameters.AddWithValue("@accountpassword", password);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                return int.Parse(reader[0].ToString());
                            }
                        }
                    }
                }
            }
            catch (MySqlException e)
            {
                System.Console.WriteLine(e.Message);
            }
            return 0;
        }

        public static int GetAccountIDForUpdate(string accountUsername)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    // We only return the accountID, in case the logged in user's account information changes. That way nothing that may change is stored. ID's will always stay the same. We also check if the account is not archived.
                    sb.Append($"SELECT `accountID` FROM accounts WHERE accountUsername = @accountusername AND archived = 0;");
                    string MySql = sb.ToString();
                    using (MySqlCommand command = new MySqlCommand(MySql, connection))
                    {
                        command.Parameters.AddWithValue("@accountusername", accountUsername);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                return int.Parse(reader[0].ToString());
                            }
                        }
                    }
                }
            }
            catch (MySqlException e)
            {
                System.Console.WriteLine(e.Message);
            }
            return 0;
        }

        // Get the Users account
        public static Account GetUserAccount(int accountID)
        {
            Account account = new Account();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("SELECT accountFirstName, accountSurname, accountUsername, accountBirthdate FROM accounts WHERE accountID = @id AND archived = 0");
                    string MySql = sb.ToString();

                    using (MySqlCommand command = new MySqlCommand(MySql, connection))
                    {
                        command.Parameters.AddWithValue("@id", accountID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                account = new Account((string)reader[0], (string)reader[1], (string)reader[2], (DateTime)reader[3]);
                            }
                        }
                    }
                    connection.Close();
                    return account;
                }
            }
            catch (MySqlException e)
            {
                System.Console.WriteLine(e.Message);
            }
            return null;
        }

        // Let the admin update the account info
        public static bool AdminUpdateAccount(string userName, string firstName, string surname)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("UPDATE accounts SET accountFirstname = @firstname, accountSurname = @surname, accountUsername = @username WHERE accountID = @id AND archived = 0");
                    string MySql = sb.ToString();
                    using (MySqlCommand command = new MySqlCommand(MySql, connection))
                    {
                        command.Parameters.AddWithValue("@id", GetAccountIDForUpdate(userName));
                        command.Parameters.AddWithValue("@firstname", firstName);
                        command.Parameters.AddWithValue("@surname", surname);
                        command.Parameters.AddWithValue("@username", userName);
                        command.ExecuteReader();
                    }
                    connection.Close();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return false;
        }


        public static bool DeleteAcc(string userName)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("DELETE FROM accounts WHERE accountID = @id AND accountUsername = @username ");
                    string MySql = sb.ToString();
                    int counter = 1;

                    using (MySqlCommand command = new MySqlCommand(MySql, connection))
                    {
                        command.Parameters.AddWithValue("@id", GetAccountIDForUpdate(userName));
                        command.Parameters.AddWithValue("@username", userName);
                        command.ExecuteReader();
                    }
                    connection.Close();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return false;
        }
    




    // Make the user a student
    public static bool MakeStudent(string userName)
    {
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("UPDATE accounts SET accountType = 0 WHERE accountID = @id AND archived = 0");
                string MySql = sb.ToString();

                using (MySqlCommand command = new MySqlCommand(MySql, connection))
                {
                    command.Parameters.AddWithValue("@id", GetAccountIDForUpdate(userName));
                    command.ExecuteReader();
                }
                connection.Close();
                return true;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return false;
    }

    // Make the user teacher        
    public static bool MakeTeacher(string userName)
    {
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("UPDATE accounts SET accountType = 1 WHERE accountID = @id AND archived = 0");
                string MySql = sb.ToString();

                using (MySqlCommand command = new MySqlCommand(MySql, connection))
                {
                    command.Parameters.AddWithValue("@id", GetAccountIDForUpdate(userName));
                    command.ExecuteReader();
                }
                connection.Close();
                return true;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return false;
    }

    // Make the user Admin
    public static bool MakeAdmin(string userName)
    {
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("UPDATE accounts SET accountType = 2 WHERE accountID = @id AND archived = 0");
                string MySql = sb.ToString();

                using (MySqlCommand command = new MySqlCommand(MySql, connection))
                {
                    command.Parameters.AddWithValue("@id", GetAccountIDForUpdate(userName));
                    command.ExecuteReader();
                }
                connection.Close();
                return true;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return false;
    }

    // Check if person is admin
    public static bool IsAdmin(int accountnumber)
    {
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                String query = "Select accountUsername from accounts where accountType= 2 and accountID = @accountnumber ";
                using (MySqlCommand Isadmin = new MySqlCommand(query, connection))
                {
                    Isadmin.Parameters.AddWithValue("@accountnumber", accountnumber);
                    connection.Open();
                    using (MySqlDataReader reader = Isadmin.ExecuteReader())
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


    //All user info received
    public static List<User> GetUsers()
    {
        List<User> queryResult = new List<User>();
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
                                return int.Parse(reader[0].ToString());
                            }
                        }
                    }
                }
            }
            catch (MySqlException e)
            {
                System.Console.WriteLine(e.Message);
            }
            return 0;
        }
    }
}
