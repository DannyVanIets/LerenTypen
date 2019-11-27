using System;

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

        public override string ToString()
        {
            return $"{Date}: {WordsPerMinute} woorden per minuut";
        }
    }
}
