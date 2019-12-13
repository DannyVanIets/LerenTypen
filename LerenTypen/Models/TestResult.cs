using LerenTypen.Controllers;
using System;
using System.Collections.Generic;

namespace LerenTypen
{
    public class TestResult
    {
        public int ID { get; private set; }
        public int TestID { get; private set; }
        public string Date { get; private set; }
        public int WordsPerMinute { get; private set; }

        public TestResult(int id, int testID, string date, int wordsPerMinute)
        {
            ID = id;
            Date = date;
            WordsPerMinute = wordsPerMinute;
        }

        public override string ToString()
        {
            int score = (int)TestResultController.CalculatePercentageRight(TestID, ID);
            return $"{Date}: {WordsPerMinute} woorden per minuut ({score}% goed)";
        }
    }
}
