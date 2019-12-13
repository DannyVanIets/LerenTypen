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
        // Happy
        [TestCase(10)]
        //new List<TestTable>
        public void GetAllTests_ReturnsAllTests(int resultCount)
        {
            //Arrange
            List<TestTable> answer;
            //Act
            answer = TestController.GetAllTests();
            //Assert
            Assert.AreEqual(resultCount, answer.Count);
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
        // Happy
        [TestCase(12, 12, "Landen en steden", 1, 1, "HenkerDenker", 72, 2, 1, false, "02-12-2019", false)]
        // Unhappy
        [TestCase(0, null, null, null, null, null, null, null, null, null, null, true)]
        public void GetTest_testID_Test(int testID, int resultId, string resultName, int resultType, int resultAuthorID, string resultAuthorUsername, int resultWordCount, int resultVersion, int resultDifficulty, bool resultIsPrivate, string resultCreatedDateTime, bool expectNull)
        {
            //Arrange
            Test answer;
            Test result = new Test(resultId, resultName, resultType, resultAuthorID, resultAuthorUsername, resultWordCount, resultVersion, resultDifficulty, resultIsPrivate, resultCreatedDateTime);
            //Act
            answer = TestController.GetTest(testID);
            //Assert
            if (answer == null && expectNull)
            {
                Assert.Pass();
            }
            else
            {
                Assert.AreEqual(result.AuthorID, answer.AuthorID);
                Assert.AreEqual(result.AuthorUsername, answer.AuthorUsername);
                Assert.AreEqual(result.CreatedDateTime, answer.CreatedDateTime);
                Assert.AreEqual(result.Difficulty, answer.Difficulty);
                Assert.AreEqual(result.ID, answer.ID);
                Assert.AreEqual(result.IsPrivate, answer.IsPrivate);
                Assert.AreEqual(result.Name, answer.Name);
                Assert.AreEqual(result.Type, answer.Type);
                Assert.AreEqual(result.WordCount, answer.WordCount);
            }
        }

        [Test]
        // Happy
        [TestCase(12, 1, 1)]
        public void GetTestInformation_testID_TestInformation(int testID, int resultAccountID, int resultTestDifficulty)
        {
            //Arrange
            List<int> answer;
            //Act
            answer = TestController.GetTestInformation(testID);
            //Assert
            Assert.AreEqual(resultAccountID, answer[0]);
            Assert.AreEqual(resultTestDifficulty, answer[1]);
        }

        [Test]
        // Happy
        [TestCase(12, "Landen en steden")]
        [TestCase(15, "Doeizinnen")]
        // Unhappy
        [TestCase(1, "")]
        [TestCase(int.MaxValue, "")]
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
        // Happy
        [TestCase(9, 39)]
        // Unhappy
        [TestCase(0, 0)]
        public void GetTestContent_testID_TestContent(int testID, int resultCount)
        {
            //Arrange
            List<string> answer = new List<string>();
            //Act
            answer = TestController.GetTestContent(testID);
            //Assert
            Assert.AreEqual(resultCount, answer.Count);
        }

        [Test]
        // Happy
        [TestCase(15, 0)]
        // Unhappy
        [TestCase(0, 0)]
        public void GetAllMyTestswithIsPrivate_accountID_AllUsersTestsWithIsPrivate(int accountID, int resultCount)
        {
            //Arrange
            List<TestTable> answer = new List<TestTable>();
            //Act
            answer = TestController.GetAllMyTestswithIsPrivate(accountID);
            //Assert
            Assert.AreEqual(resultCount, answer.Count);
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
        public void GetWordHighscore_TestID_WordsPerMinute(int testID, int result)
        {
            //Arrange
            int answer = 0;
            //Act
            answer = TestController.GetWordHighscore(testID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        // Happy
        [TestCase(12)]
        // Unhappy
        [TestCase(0)]
        public void GetTop3FastestTypers_TestID_Top3FastestTypers(int testID)
        {
            //Arrange
            Dictionary<int, int> answer;
            //Act
            answer = TestController.GetTop3FastestTypers(testID);
            //Assert
            Assert.IsTrue(answer.Count < 4);
        }

        [Test]
        // Happy
        [TestCase(12)]
        // Unhappy
        [TestCase(0)]
        public void GetTop3Highscores_TestID_Top3Highscores(int testID)
        {
            //Arrange
            Dictionary<int, int> answer;
            //Act
            answer = TestController.GetTop3Highscores(testID);
            //Assert
            Assert.IsTrue(answer.Count < 4);
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
