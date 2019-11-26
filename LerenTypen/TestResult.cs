using System;

namespace LerenTypen
{
    class TestResult
    {
        public int ID { get; private set; }
        public DateTime DateTime { get; private set; }
        public int WordsPerMinute { get; private set; }

        public TestResult(int id, DateTime dateTime, int wordsPerMinute)
        {
            ID = id;
            DateTime = dateTime;
            WordsPerMinute = wordsPerMinute;
        }

        public override string ToString()
        {
            return $"{DateTime.ToString()}: {WordsPerMinute} woorden per minuut";
        }
    }
}
