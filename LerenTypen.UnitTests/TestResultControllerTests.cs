using LerenTypen.Controllers;
using LerenTypen.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace LerenTypen.UnitTests
{
    class TestResultControllerTests
    {
        public TestResultControllerTests()
        {
            Database.Connect();
        }

        #region Select
        [Test]
        // Happy
        [TestCase(48, "60", "0", "100")]
        public void GetTestResults_testResultsID_TestResults(int testResultsID, string resultWordsEachMinute, string resultPauses, string resultScore)
        {
            //Arrange
            List<string> answer = new List<string>();
            //Act
            answer = TestResultController.GetTestResults(testResultsID);
            //Assert
            Assert.AreEqual(resultWordsEachMinute, answer[0]);
            Assert.AreEqual(resultPauses, answer[1]);
            Assert.AreEqual(resultScore, answer[2]);
        }

        [Test]
        // Happy
        [TestCase(58, 45)]
        // Unhappy
        [TestCase(int.MaxValue, 0)]
        public void GetTestResultsContentRight_testResultsID_TestResultsContentRight(int testResultsID, int resultCount)
        {
            //Arrange
            List<string> answer = new List<string>();
            //Act
            answer = TestResultController.GetTestResultsContentRight(testResultsID);
            //Assert
            Assert.AreEqual(resultCount, answer.Count);
        }

        [Test]
        // Happy
        [TestCase(58, 1)]
        // Unhappy
        [TestCase(int.MaxValue, 0)]
        public void GetTestResultsContentWrong_testResultsID_TestResultsContentWrong(int testResultsID, int resultCount)
        {
            //Arrange
            List<string> answer = new List<string>();
            //Act
            answer = TestResultController.GetTestResultsContentWrong(testResultsID);
            //Assert
            Assert.AreEqual(resultCount, answer.Count);
        }

        [Test]
        // Happy
        [TestCase(58, 1)]
        // Unhappy
        [TestCase(int.MaxValue, 0)]
        public void GetTestResultsContentHadToBe_testResultsID_TestResultsContentHadToBe(int testResultsID, int resultCount)
        {
            //Arrange
            List<string> answer = new List<string>();
            //Act
            answer = TestResultController.GetTestResultsContentHadToBe(testResultsID);
            //Assert
            Assert.AreEqual(resultCount, answer.Count);
        }

        [Test]
        // Happy
        [TestCase(2, 1, 0)]
        // Unhappy
        [TestCase(0, 1, 0)]
        public void GetAllTestResultsFromAccount_accountIDTestID_AllTestResultsFromAccount(int accountID, int testID, int resultCount)
        {
            //Arrange
            List<TestResult> answer = new List<TestResult>();
            //Act
            answer = TestResultController.GetAllTestResultsFromAccount(accountID, testID);
            //Assert
            Assert.AreEqual(resultCount, answer.Count);
        }
        #endregion

        #region Insert
        [Test]
        // Happy
        [TestCase(20)]
        // Unhappy
        [TestCase(0)]
        public void SaveResultsandInsertResultsContent_testResultsID_testResultID(int testID)
        {
            //Arrange
            int answer;
            List<string> rightAnswers = new List<string>() { "test", "test" };
            Dictionary<int, string> wrongAnswers = new Dictionary<int, string>();
            wrongAnswers.Add(2, "Test");
            List<string> lines = new List<string>() { "test", "test", "test", "test" };
            //Act
            answer = TestResultController.SaveResults(testID, 1, 1, 1, rightAnswers, wrongAnswers, lines, 5);
            //Assert
            Assert.IsNotNull(answer);
        }
        #endregion
    }
}
