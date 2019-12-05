using LerenTypen.Controllers;
using LerenTypen.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace LerenTypen.UnitTests
{
    class TestResultControllerTests
    {
        #region Select
        [Test]
        public void GetTestResults_testResultsID_TestResults(int testResultsID, List<string> result)
        {
            //Arrange
            List<string> answer = new List<string>();
            //Act
            answer = TestResultController.GetTestResults(testResultsID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetTestResultsContentRight_testResultsID_TestResultsContentRight(int testResultsID, List<string> result)
        {
            //Arrange
            List<string> answer = new List<string>();
            //Act
            answer = TestResultController.GetTestResultsContentRight(testResultsID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetTestResultsContentWrong_testResultsID_TestResultsContentWrong(int testResultsID, List<string> result)
        {
            //Arrange
            List<string> answer = new List<string>();
            //Act
            answer = TestResultController.GetTestResultsContentWrong(testResultsID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetTestResultsContentHadToBe_testResultsID_TestResultsContentHadToBe(int testResultsID, List<string> result)
        {
            //Arrange
            List<string> answer = new List<string>();
            //Act
            answer = TestResultController.GetTestResultsContentHadToBe(testResultsID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetAllTestResultsFromAccount_accountIDTestID_AllTestResultsFromAccount(int accountID, int testID, List<TestResult> result)
        {
            //Arrange
            List<TestResult> answer = new List<TestResult>();
            //Act
            answer = TestResultController.GetAllTestResultsFromAccount(accountID, testID);
            //Assert
            Assert.AreEqual(result, answer);
        }
        #endregion

        #region Insert
        [Test]
        public void SaveResultsandInsertResultsContent_testResultsID_testResultID(int testID, int accountID, int wordsEachMinute, int pauses, List<string> rightAnswers, Dictionary<int, string> wrongAnswers, List<string> lines, int score)
        {
            //Arrange
            int answer;
            List<string> result;
            //Act
            answer = TestResultController.SaveResults(testID, accountID, wordsEachMinute, pauses, rightAnswers, wrongAnswers, lines, score);
            result = Database.SelectQuery("Select Max(testResultID) from testresults");
            //Assert
            Assert.AreEqual(int.Parse(result[0]), answer);
        }


        #endregion
    }
}
