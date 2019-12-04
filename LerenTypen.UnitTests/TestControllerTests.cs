using LerenTypen.Controllers;
using LerenTypen.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace LerenTypen.UnitTests
{
    class TestControllerTests
    {
        #region Select
        [Test]
        public void GetAllTests_ReturnsListTestTable(List<TestTable> result)
        {
            //Arrange
            List<TestTable> answer;
            //Act
            answer = TestController.GetAllTests();
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetAmountOfWordsFromTest_testID_int(int testID, int result)
        {
            //Arrange
            int answer = 0;
            //Act
            answer = TestController.GetAmountOfWordsFromTest(testID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetTest_testID_ListInt(int testID, Test result)
        {
            //Arrange
            Test answer;
            //Act
            answer = TestController.GetTest(testID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetTestInformation_testID_ListInt(int testID, List<int> result)
        {
            //Arrange
            List<int> answer;
            //Act
            answer = TestController.GetTestInformation(testID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetTestName_testID_string(int testID, string result)
        {
            //Arrange
            string answer = "";
            //Act
            answer = TestController.GetTestName(testID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetTestContent_testID_ListString(int testID, List<string> result)
        {
            //Arrange
            List<string> answer = new List<string>();
            //Act
            answer = TestController.GetTestContent(testID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetAllMyTestswithIsPrivate_accountID_ListTestTable(int accountID, List<TestTable> result)
        {
            //Arrange
            List<TestTable> answer = new List<TestTable>();
            //Act
            answer = TestController.GetAllMyTestswithIsPrivate(accountID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetAllMyTestsAlreadyMade_intIngelogd_ListTestTable(int ingelogd, List<TestTable> result)
        {
            //Arrange
            List<TestTable> answer = new List<TestTable>();
            //Act
            answer = TestController.GetAllMyTestsAlreadyMade(ingelogd);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetAllTestsAlreadyMade_intIngelogd_ListTestTable(int ingelogd, List<TestTable> result)
        {
            //Arrange
            List<TestTable> answer = new List<TestTable>();
            //Act
            answer = TestController.GetAllTestsAlreadyMade(ingelogd);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetTestHighscore_accountIDTestID_int(int testID, List<TestResult> result)
        {
            //Arrange
            int answer = 0;
            //Act
            answer = TestController.GetTestHighscore(testID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetTestAverageScore(int testID, int result)
        {
            //Arrange
            int answer = 0;
            //Act
            answer = TestController.GetTestAverageScore(testID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetFastestTyper(int testID, int result)
        {
            //Arrange
            int answer = 0;
            //Act
            answer = TestController.GetTestAverageScore(testID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetFastestTyper(int testID, Dictionary<int, int> result)
        {
            //Arrange
            Dictionary<int, int> answer;
            //Act
            answer = TestController.GetTop3FastestTypers(testID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetTop3Highscores(int testID, Dictionary<int, int> result)
        {
            //Arrange
            Dictionary<int, int> answer;
            //Act
            answer = TestController.GetTop3Highscores(testID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetTimesMade(int testID, int result)
        {
            //Arrange
            int answer;
            //Act
            answer = TestController.GetTimesMade(testID);
            //Assert
            Assert.AreEqual(result, answer);
        }
        #endregion
    }
}
