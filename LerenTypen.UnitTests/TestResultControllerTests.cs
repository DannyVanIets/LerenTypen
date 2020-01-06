using LerenTypen.Controllers;
using LerenTypen.Models;
using NUnit.Framework;
using System;
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
        public void GetTestResults_TestResultID_ReturnNoException()
        {
            // Arrange
            int testResultID = Database.GetFirstTestResultID();
            // Act & Assert
            Assert.DoesNotThrow(() => TestResultController.GetTestResults(testResultID));
        }

        [Test]
        public void GetWordsPerMinuteByPeriod_AccountIDAndDates_ReturnNoException()
        {
            // Arrange
            int accountID = Database.GetFirstAccountID();
            // Act & Assert
            Assert.DoesNotThrow(() => TestResultController.GetWordsPerMinuteByPeriod(accountID, DateTime.Now.AddDays(-5), DateTime.Now));
        }

        [Test]
        public void GetDateRange_AccountID_ReturnNoException()
        {
            // Arrange
            int accountID = Database.GetFirstAccountID();
            // Act & Assert
            Assert.DoesNotThrow(() => TestResultController.GetDateRange(accountID));
        }

        [Test]
        public void GetTestResultsContentRight_TestResultID_ReturnNoException()
        {
            // Arrange
            int testResultID = Database.GetFirstTestResultID();
            // Act & Assert
            Assert.DoesNotThrow(() => TestResultController.GetTestResultsContentRight(testResultID));
        }

        [Test]
        public void GetTestResultsContentWrong_TestIDAndTestResultID_ReturnNoException()
        {
            // Arrange
            int testID = Database.GetFirstTestID();
            int testResultID = Database.GetFirstTestResultID();
            // Act & Assert
            Assert.DoesNotThrow(() => TestResultController.GetTestResultsContentWrong(testID, testResultID));
        }

        [Test]
        public void GetTestResultsContentHadToBe_TestResultID_ReturnNoException()
        {
            // Arrange
            int testID = Database.GetFirstTestID();
            int testResultID = Database.GetFirstTestResultID();
            // Act & Assert
            Assert.DoesNotThrow(() => TestResultController.GetTestResultsContentWrong(testID, testResultID));
        }
        #endregion

        #region Insert
        //[Test]
        //// Happy
        //[TestCase(20)]
        //// Unhappy
        //[TestCase(0)]
        //public void SaveResultsandInsertResultsContent_testResultsID_testResultID(int testID)
        //{
        //    //Arrange
        //    int answer;
        //    List<string> rightAnswers = new List<string>() { "test", "test" };
        //    Dictionary<int, string> wrongAnswers = new Dictionary<int, string>();
        //    wrongAnswers.Add(2, "Test");
        //    List<string> lines = new List<string>() { "test", "test", "test", "test" };
        //    //Act
        //    answer = TestResultController.SaveResults(testID, 1, 1, 1, rightAnswers, wrongAnswers, lines, 5, 55, true);
        //    //Assert
        //    Assert.IsNotNull(answer);
        //}
        #endregion
    }
}
