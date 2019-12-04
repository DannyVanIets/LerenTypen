namespace LerenTypen
{
    public class TestTable
    {
        public int WPFNumber { get; set; }
        public string WPFName { get; set; }
        public int WPFTimesMade { get; set; }
        public int Highscore { get; set; }
        public int AmountOfWords { get; set; }
        public string Difficulty { get; set; }
        public string Uploader { get; set; }
        public int DifficultyBinder { get; set; }
        public bool IsPrivate { get; set; }
        public int TestId { get; set; }

        public TestTable(int number, string name, int timesMade, int highscore, int amountOfWords, int testDifficulty, string uploader)
        {
            this.WPFNumber = number;
            this.WPFName = name;
            this.WPFTimesMade = timesMade;
            this.Highscore = highscore;
            this.AmountOfWords = amountOfWords;
            this.Uploader = uploader;

            if (testDifficulty == 0)
            {
                DifficultyBinder = 0;
                Difficulty = "makkelijk";
            }
            else if (testDifficulty == 1)
            {
                DifficultyBinder = 1;
                Difficulty = "gemiddeld";
            }
            else if (testDifficulty == 2)
            {
                DifficultyBinder = 2;
                Difficulty = "moeilijk";
            }

        }
        public TestTable(int number, string name, int timesMade, int highscore, int amountOfWords, int testDifficulty, string uploader, int isPrivate, int testId)
        {
            this.WPFNumber = number;
            this.WPFName = name;
            this.WPFTimesMade = timesMade;
            this.Highscore = highscore;
            this.AmountOfWords = amountOfWords;
            this.Uploader = uploader;

            if (testDifficulty == 0)
            {
                DifficultyBinder = 0;
                Difficulty = "makkelijk";
            }
            else if (testDifficulty == 1)
            {
                DifficultyBinder = 1;
                Difficulty = "gemiddeld";
            }
            else if (testDifficulty == 2)
            {
                DifficultyBinder = 2;
                Difficulty = "moeilijk";
            }

            if (isPrivate == 0)
            {
                this.IsPrivate = false;
            }
            else
            {
                this.IsPrivate = true;
            }

            this.TestId = testId;
        }

    }
}
