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
        public List<string> Content { get; private set; }

        public int WordCount { get; private set; }

        public string Difficulty { get; private set; }

        public bool isPrivate { get; private set; }

        public Test(int wordCount, string difficulty)
        {
            this.WordCount = wordCount;
            this.Difficulty = difficulty;
        }
        //public int GetTimesTaken(Account account)
        //{
        //    return null;
        //}

            


    }
    class TestTable
    {
        public int WPFNumber { get; set; }
        public string WPFName { get; set; }
        public int WPFTimesMade { get; set; }
        public int Highscore { get; set; }
        public int AmountOfWords { get; set; }

        public string Difficulty { get; set; }
        public string Uploader { get; set; }

        public int DifficultyBinder { get; set; }
        public TestTable(int number, string name, int timesMade, int highscore, int amountOfWords, string difficulty, string uploader)
        {
            this.WPFNumber = number;
            this.WPFName = name;
            this.WPFTimesMade = timesMade;
            this.Highscore = highscore;
            this.AmountOfWords = amountOfWords;
            this.Difficulty = difficulty;
            this.Uploader = uploader;
            if(difficulty == "makkelijk")
            {
                DifficultyBinder = 1;
            }
            else if(difficulty == "gemiddeld")
            {
                DifficultyBinder = 2;
            }
            else
            {
                DifficultyBinder = 3;
            }
        }
        
    }
}
