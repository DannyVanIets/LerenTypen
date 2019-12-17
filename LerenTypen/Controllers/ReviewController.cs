using LerenTypen.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace LerenTypen.Controllers
{
    //In this controller we have all the testReviews queries we use in this applicaiton.
    public static class ReviewController
    {
        //Regex is used to check if filled in text has only number. Is used in the OnlyNumberic function.
        private static readonly Regex _regex = new Regex("[^0-9]+");

        //In this query we will check if the user has already made a review on a certain test.
        //If that's true, then we return true and we won't show textboxes and button to add a review on the testinfopage.
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
                            //reader.HasRows checks if the query has returned any rows.
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

        public static string DisplayUsersReviewInfo(int testID)
        {
            List<Review> queryResult = new List<Review>();
            SqlConnection connection = new SqlConnection(Database.connectionString);
            try
            {
                string query = "select a.accountUsername , trs.testReviewScore , trs.testReviewDescription , trs.testReviewDateAdded from accounts a inner join testReviews trs on a.accountID = trs.accountID where trs.testID=@ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", testID);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            queryResult.Add(new Review(reader.GetString(0), reader.GetInt16(1) , reader.GetString(2) , reader.GetDateTime(3)));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
            return queryResult.ToString();
        }

        //In this query we will insert a review with a description added to it. Other than that, same as "AddReviewWithoutDescription".
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


        //Inserts a review into the database without a desciption.
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

        //Used to check if the the text is only numberic. If true, the text contains only numbers.
        //Uses the _regex property. Regex checks if the text matches with the property.
        public static bool OnlyNumberic(string text)
        {
            return !_regex.IsMatch(text);
        }
    }
}
