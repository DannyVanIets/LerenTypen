using System;
using System.Collections.Generic;

namespace LerenTypen
{
    class TestResult
    {
        public int ID { get; private set; }
        public string Date { get; private set; }
        public int WordsPerMinute { get; private set; }

        public TestResult(int id, string date, int wordsPerMinute)
        {
            ID = id;
            Date = date;
            WordsPerMinute = wordsPerMinute;
        }

        public decimal CalculatePercentageRight()
        {
            List<string> rightAnswers = Database.GetTestResultsContentRight(ID);
            List<string> wrongAnswers = Database.GetTestResultsContentWrong(ID);
            decimal percentageRight;

            try
            {
                percentageRight = decimal.Divide(rightAnswers.Count, rightAnswers.Count + wrongAnswers.Count) * 100;
            }
            catch (DivideByZeroException)
            {
                percentageRight = 100;
            }
            return percentageRight;
        }

        public override string ToString()
        {
            return $"{Date}: {WordsPerMinute} woorden per minuut ({(int)CalculatePercentageRight()}% goed)";
        }
    }
}
