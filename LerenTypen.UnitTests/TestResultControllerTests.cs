using LerenTypen.Controllers;
using LerenTypen.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace LerenTypen.UnitTests
{
    class TestResultControllerTests
    {
        #region TestResultControllerTests
        [Test]
        public void GetTestResults_testResultsID_ListString(int testResultsID, List<string> result)
        {
            //Arrange
            List<string> answer = new List<string>();
            //Act
            answer = TestResultController.GetTestResults(testResultsID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetTestResultsContentRight_testResultsID_ListString(int testResultsID, List<string> result)
        {
            //Arrange
            List<string> answer = new List<string>();
            //Act
            answer = TestResultController.GetTestResultsContentRight(testResultsID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetTestResultsContentWrong_testResultsID_ListString(int testResultsID, List<string> result)
        {
            //Arrange
            List<string> answer = new List<string>();
            //Act
            answer = TestResultController.GetTestResultsContentWrong(testResultsID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetTestResultsContentHadToBe_testResultsID_ListString(int testResultsID, List<string> result)
        {
            //Arrange
            List<string> answer = new List<string>();
            //Act
            answer = TestResultController.GetTestResultsContentHadToBe(testResultsID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetAllTestResultsFromAccount_accountIDTestID_ListTestResult(int accountID, int testID, List<TestResult> result)
        {
            //Arrange
            List<TestResult> answer = new List<TestResult>();
            //Act
            answer = TestResultController.GetAllTestResultsFromAccount(accountID, testID);
            //Assert
            Assert.AreEqual(result, answer);
        }
        #endregion
    }
}
