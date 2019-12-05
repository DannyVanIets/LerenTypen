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
        public void GetAllTests_ReturnsAllTests(List<TestTable> result)
        {
            //Arrange
            List<TestTable> answer;
            //Act
            answer = TestController.GetAllTests();
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetAmountOfWordsFromTest_testID_AmountOfWordsOfTest(int testID, int result)
        {
            //Arrange
            int answer = 0;
            //Act
            answer = TestController.GetAmountOfWordsFromTest(testID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetTest_testID_Test(int testID, Test result)
        {
            //Arrange
            Test answer;
            //Act
            answer = TestController.GetTest(testID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetTestInformation_testID_TestInformation(int testID, List<int> result)
        {
            //Arrange
            List<int> answer;
            //Act
            answer = TestController.GetTestInformation(testID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetTestName_testID_TestName(int testID, string result)
        {
            //Arrange
            string answer = "";
            //Act
            answer = TestController.GetTestName(testID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetTestContent_testID_TestContent(int testID, List<string> result)
        {
            //Arrange
            List<string> answer = new List<string>();
            //Act
            answer = TestController.GetTestContent(testID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetAllMyTestswithIsPrivate_accountID_AllUsersTestsWithIsPrivate(int accountID, List<TestTable> result)
        {
            //Arrange
            List<TestTable> answer = new List<TestTable>();
            //Act
            answer = TestController.GetAllMyTestswithIsPrivate(accountID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetAllMyTestsAlreadyMade_intIngelogd_AllUsersTestsAlreadyMade(int ingelogd, List<TestTable> result)
        {
            //Arrange
            List<TestTable> answer = new List<TestTable>();
            //Act
            answer = TestController.GetAllMyTestsAlreadyMade(ingelogd);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetAllTestsAlreadyMade_intIngelogd_AllTestsAlreadyMade(int ingelogd, List<TestTable> result)
        {
            //Arrange
            List<TestTable> answer = new List<TestTable>();
            //Act
            answer = TestController.GetAllTestsAlreadyMade(ingelogd);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetTestHighscore_accountIDTestID_TestHighscore(int testID, int result)
        {
            //Arrange
            int answer = 0;
            //Act
            answer = TestController.GetTestHighscore(testID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetTestAverageScore_TestID_TestAverageScore(int testID, int result)
        {
            //Arrange
            int answer = 0;
            //Act
            answer = TestController.GetTestAverageScore(testID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetFastestTyper_TestID_FastestTyper(int testID, int result)
        {
            //Arrange
            int answer = 0;
            //Act
            answer = TestController.GetFastestTyper(testID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetTop3FastestTypers_TestID_Top3FastestTypers(int testID, Dictionary<int, int> result)
        {
            //Arrange
            Dictionary<int, int> answer;
            //Act
            answer = TestController.GetTop3FastestTypers(testID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetTop3Highscores_TestID_Top3Highscores(int testID, Dictionary<int, int> result)
        {
            //Arrange
            Dictionary<int, int> answer;
            //Act
            answer = TestController.GetTop3Highscores(testID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetTimesMade_TestID_TimesMade(int testID, int result)
        {
            //Arrange
            int answer;
            //Act
            answer = TestController.GetTimesMade(testID);
            //Assert
            Assert.AreEqual(result, answer);
        }
        #endregion

        #region Insert        
        public void AddTestandContent_TestContent_ReturnNoException(string testName, int testType, int testDifficulty, int isPrivate, List<string> content, int uploadedBy, bool result)
        {
            //Arrange
            bool answer;
            //Act
            answer = TestController.AddTest(testName, testType, testDifficulty, isPrivate, content, uploadedBy);
            //Assert
            Assert.AreEqual(result, answer);
        }

        #endregion

        #region Update
        public void UpdateTestToPublic_testID_NoException(int testID, bool result)
        {
            //Arrange
            bool answer;
            //Act
            answer = TestController.UpdateTestToPublic(testID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        public void UpdateTestToPrivate_testID_NoException(int testID, bool result)
        {
            //Arrange
            bool answer;
            //Act
            answer = TestController.UpdateTestToPrivate(testID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        #endregion
    }
}
