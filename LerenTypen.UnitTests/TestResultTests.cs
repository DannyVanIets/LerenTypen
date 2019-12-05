using LerenTypen.Models;
using NUnit.Framework;

namespace LerenTypen.UnitTests
{
    class TestResultTests
    {
        [Test]
        public void CalculatePercentageRight_TestResult_percentageRight(TestResult t, decimal result)
        {
            //Arrange
            decimal answer = 0;
            //Act
            answer = t.CalculatePercentageRight();
            //Assert
            Assert.AreEqual(result, answer);
        }
    }
}
