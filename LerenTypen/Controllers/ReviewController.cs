using LerenTypen.Models;
using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace LerenTypen.Controllers
{
    public static class ReviewController
    {
        public static bool CheckIfUserHasMadeAReview(int testID, int accountID)
        {
            bool result = false;
            SqlConnection connection = new SqlConnection(Database.connectionString);

            try
            {
                string query = "SELECT * FROM testReviews WHERE testID = @testid AND accountID = @accountid";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@testid", testID);
                    command.Parameters.AddWithValue("@accountid", accountID);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader.HasRows)
                            {
                                result = true;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return result;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
            return result;
        }

        public static bool AddReviewWithDescription(Review review)
        {
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                string query = "INSERT INTO testReviews(testID, accountID, testReviewScore, testReviewDateAdded, testReviewDescription)" +
                    " VALUES (@testid, @accountid, @score, @dateadded, @description)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@testid", review.TestID);
                    command.Parameters.AddWithValue("@accountid", review.AccountID);
                    command.Parameters.AddWithValue("@score", review.ReviewScore);
                    command.Parameters.AddWithValue("@dateadded", review.ReviewDateAdded);
                    command.Parameters.AddWithValue("@description", review.ReviewDescription);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
            return true;
        }

        public static bool AddReviewWithoutDescription(Review review)
        {
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                string query = "INSERT INTO testReviews(testID, accountID, testReviewScore, testReviewDateAdded)" +
                    " VALUES (@testid, @accountid, @score, @dateadded)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@testid", review.TestID);
                    command.Parameters.AddWithValue("@accountid", review.AccountID);
                    command.Parameters.AddWithValue("@score", review.ReviewScore);
                    command.Parameters.AddWithValue("@dateadded", review.ReviewDateAdded);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
            return true;
        }
    }
}
