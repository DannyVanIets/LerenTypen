using LerenTypen.Models;
using NUnit.Framework;

namespace LerenTypen.UnitTests
{
    class TestResultTests
    {
        [Test]
        [TestCase(48, 100)]
        [TestCase(56, 75)]
        public void CalculatePercentageRight_TestResult_percentageRight(int testresultID, decimal result)
        {
            //Arrange
            decimal answer = 0;
            TestResult t = new TestResult(testresultID, "10/20/1990", 20);
            //Act
            answer = t.CalculatePercentageRight();
            //Assert
            Assert.AreEqual(result, answer);
        }
    }
}
