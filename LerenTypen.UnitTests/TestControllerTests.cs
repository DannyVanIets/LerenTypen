using LerenTypen.Controllers;
using LerenTypen.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace LerenTypen.UnitTests
{
    class TestControllerTests
    {
        public TestControllerTests()
        {
            Database.Connect();
        }


        #region Select
        [Test]
        //new List<TestTable>
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
        // Happy
        [TestCase(9, 39)]
        [TestCase(21, 42)]
        // Unhappy
        [TestCase(1, null)]
        [TestCase(0, null)]
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
        //new Test
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
        // new list
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
        // Happy
        [TestCase(12, "Landen en steden")]
        [TestCase(15, "Doeizinnen")]
        // Unhappy
        [TestCase(1, null)]
        [TestCase(int.MaxValue, null)]
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
        // new List
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
        // new List
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
        // Happy
        [TestCase(15)]
        // Unhappy
        [TestCase(0)]
        public void GetAllMyTestsAlreadyMade_intIngelogd_AllUsersTestsAlreadyMade(int ingelogd)
        {
            //Arrange
            List<TestTable> answer = new List<TestTable>();
            //Act
            answer = TestController.GetAllMyTestsAlreadyMade(ingelogd);
            //Assert
            Assert.AreEqual(0, answer.Count);
        }

        [Test]
        // Happy
        [TestCase(15, 1)]
        // Unhappy
        [TestCase(0, 0)]
        public void GetAllTestsAlreadyMade_intIngelogd_AllTestsAlreadyMade(int ingelogd, int resultCount)
        {
            //Arrange
            List<TestTable> answer = new List<TestTable>();
            //Act
            answer = TestController.GetAllTestsAlreadyMade(ingelogd);
            //Assert
            Assert.AreEqual(resultCount, answer.Count);
        }

        [Test]
        // Happy
        [TestCase(10, 100)]
        [TestCase(15, 100)]
        [TestCase(20, 100)]
        // Unhappy
        [TestCase(1, null)]
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
        // Happy
        [TestCase(20, 93)]
        // Unhappy
        [TestCase(1, null)]
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
        // Happy
        [TestCase(20, 60)]
        [TestCase(14, 162)]
        // Unhappy
        [TestCase(1, null)]
        public void GetFastestTyper_TestID_WordsPerMinute(int testID, int result)
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
