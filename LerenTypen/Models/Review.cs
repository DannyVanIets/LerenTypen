using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LerenTypen.Models
{
    class Review
    {
        public int ReviewScore { get; set; }
        public Date ReviewDateAdded { get; set; }
        public string ReviewDescription { get; set; }

        public Review(int score, Date dateAdded, string description)
        {
            ReviewScore = score;
            ReviewDateAdded = dateAdded;
            ReviewDescription = description;
        }

        public Review(int score, Date dateAdded)
        {
            ReviewScore = score;
            ReviewDateAdded = dateAdded;
        }
    }
}
