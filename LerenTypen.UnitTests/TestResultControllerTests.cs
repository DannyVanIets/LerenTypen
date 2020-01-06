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
            Assert.DoesNotThrow(() => TestResultController.GetTestResultsContentHadToBe(testResultID));
        }

        [Test]
        public void GetAllTestResultsFromAccount_TestIDAndAccountID_ReturnNoException()
        {
            // Arrange
            int testID = Database.GetFirstTestID();
            int accountID = Database.GetFirstAccountID();
            // Act & Assert
            Assert.DoesNotThrow(() => TestResultController.GetAllTestResultsFromAccountAndTest(accountID, testID));
        }

        [Test]
        public void GetUnfinishedTestResultID_TestIDAndAccountID_ReturnNoException()
        {
            // Arrange
            int testID = Database.GetFirstTestID();
            int accountID = Database.GetFirstAccountID();
            // Act & Assert
            Assert.DoesNotThrow(() => TestResultController.GetUnfinishedTestResultID(accountID, testID));
        }

        [Test]
        public void GetAmountOfPauses_TestResultID_ReturnNoException()
        {
            // Arrange
            int testResultID = Database.GetFirstTestResultID();
            // Act & Assert
            Assert.DoesNotThrow(() => TestResultController.GetAmountOfPauses(testResultID));
        }

        [Test]
        public void GetTime_TestResultID_ReturnNoException()
        {
            // Arrange
            int testResultID = Database.GetFirstTestResultID();
            // Act & Assert
            Assert.DoesNotThrow(() => TestResultController.GetTime(testResultID));
        }

        [Test]
        public void CalculatePercentageRight_TestResultID_ReturnNoException()
        {
            // Arrange
            int testID = Database.GetFirstTestID();
            int testResultID = Database.GetFirstTestResultID();
            // Act & Assert
            Assert.DoesNotThrow(() => TestResultController.CalculatePercentageRight(testID, testResultID));
        }
        #endregion

        #region Insert
        // SaveResult
        // InsertResultsContent
        #endregion

        #region Delete
        // DeleteTestResult
        // DeleteTestResultContent
        #endregion
    }
}
