using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LerenTypen
{
    class Test
    {
        public string Name { get; private set; }
        public int Type { get; private set; }
        public string AuthorUsername { get; private set; }
        public int WordCount { get; private set; }
        public int TimesMade { get; private set; }
        public double AverageScore { get; private set; }
        public double Highscore { get; private set; }
        public int Version { get; private set; }
        public int Difficulty { get; private set; }
        public bool IsPrivate { get; private set; }

        public Test(string name, int type, string authorUsername, int wordCount, int timesMade, double averageScore, 
            double highscore, int version, int difficulty, bool isPrivate)
        {
            Name = name;
            Type = type;
            AuthorUsername = authorUsername;
            WordCount = wordCount;
            TimesMade = timesMade;
            AverageScore = averageScore;
            Highscore = highscore;
            Version = version;
            Difficulty = difficulty;
            IsPrivate = isPrivate;
        }
    }
    }
}
