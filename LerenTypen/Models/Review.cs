using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LerenTypen.Models
{
    public class Review
    {
        public int TestID { get; set; }
        public int AccountID { get; set; }
        public int ReviewScore { get; set; }
        public DateTime ReviewDateAdded { get; set; }
        public string ReviewDescription { get; set; }

        public Review(int testID, int accountID, int score, string description)
        {
            TestID = testID;
            AccountID = accountID;
            ReviewScore = score;
            ReviewDateAdded = DateTime.Now;
            ReviewDescription = description;
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
