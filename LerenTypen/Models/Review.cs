using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LerenTypen.Models
{
    //This class is used to store all the information from reviews. This way it can be used for inserting and selecting reviews
    public class Review
    {
        public int TestID { get; set; }
        public string AccountUsername { get; set; }
        public int AccountID { get; set; }
        public int ReviewScore { get; set; }
        public DateTime ReviewDateAdded { get; set; }
        public string ReviewDescription { get; set; }

        //Two different constructors one is used if the description is filled in and the other if it isn't.
        public Review(int testID, int accountID, int score, string description)
        {
            TestID = testID;
            AccountID = accountID;
            ReviewScore = score;
            ReviewDateAdded = DateTime.Now;
            ReviewDescription = description;
        }

        public Review(string accusername , int reviewscoregiven , string reviewdescription, DateTime date)
        {
            AccountUsername = accusername;
            ReviewScore = reviewscoregiven;
            ReviewDescription = reviewdescription;
            ReviewDateAdded = date;

        }
        public Review(int testID, int accountID, int score)
        {
            TestID = testID;
            AccountID = accountID;
            ReviewScore = score;
            ReviewDateAdded = DateTime.Now;
        }
    }
}