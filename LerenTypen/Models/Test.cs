using LerenTypen.Controllers;

namespace LerenTypen.Models
{
    public class Test
    {
        public int ID { get; private set; }
        public int Number { get; set; }
        public string Name { get; private set; }
        public int Type { get; private set; }
        public int AuthorID { get; private set; }
        public string AuthorUsername { get; private set; }
        public int WordCount { get; private set; }
        public int TimesMade { get; private set; }
        public int AverageScore { get; private set; }
        public int Highscore { get; private set; }
        public int Version { get; private set; }
        public int Difficulty { get; private set; }
        public bool IsPrivate { get; private set; }
        public string CreatedDateTime { get; private set; }

        public Test(int id, string name, int type, int authorID, string authorUsername, int wordCount,
            int version, int difficulty, bool isPrivate, string createdDateTime)
        {
            ID = id;
            Name = name;
            Type = type;
            AuthorID = authorID;
            AuthorUsername = authorUsername;
            WordCount = wordCount;
            Version = version;
            Difficulty = difficulty;
            IsPrivate = isPrivate;
            CreatedDateTime = createdDateTime;

            TimesMade = TestController.GetTimesMade(ID);
            Highscore = TestController.GetTestHighscore(ID);
            AverageScore = TestController.GetTestAverageScore(ID);
        }

        public Test(int id, string name, string authorName)
        {
            ID = id;
            Name = name;
            AuthorUsername = authorName;
        }
    }
}
