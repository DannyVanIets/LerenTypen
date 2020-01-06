using LerenTypen.Controllers;
using LerenTypen.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace LerenTypen.UnitTests
{
    class TestResultControllerTests
    {
        private int TestResultID;
        private int TestID;
        private int AccountID;

        public TestResultControllerTests()
        {
            Database.Connect();

            TestResultID = Database.GetFirstTestResultID();
            TestID = Database.GetFirstTestID();
            AccountID = Database.GetFirstAccountID();
        }

        #region Select
        [Test]
        public void GetTestResults_TestResultID_ReturnNoException()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => TestResultController.GetTestResults(TestResultID));
        }

        [Test]
        public void GetWordsPerMinuteByPeriod_AccountIDAndDates_ReturnNoException()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => TestResultController.GetWordsPerMinuteByPeriod(AccountID, DateTime.Now.AddDays(-5), DateTime.Now));
        }

        [Test]
        public void GetDateRange_AccountID_ReturnNoException()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => TestResultController.GetDateRange(AccountID));
        }

        [Test]
        public void GetTestResultsContentRight_TestResultID_ReturnNoException()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => TestResultController.GetTestResultsContentRight(TestResultID));
        }

        [Test]
        public void GetTestResultsContentWrong_TestIDAndTestResultID_ReturnNoException()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => TestResultController.GetTestResultsContentWrong(TestID, TestResultID));
        }

        [Test]
        public void GetTestResultsContentHadToBe_TestResultID_ReturnNoException()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => TestResultController.GetTestResultsContentHadToBe(TestResultID));
        }

        [Test]
        public void GetAllTestResultsFromAccount_TestIDAndAccountID_ReturnNoException()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => TestResultController.GetAllTestResultsFromAccountAndTest(AccountID, TestID));
        }

        [Test]
        public void GetUnfinishedTestResultID_TestIDAndAccountID_ReturnNoException()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => TestResultController.GetUnfinishedTestResultID(AccountID, TestID));
        }

        [Test]
        public void GetAmountOfPauses_TestResultID_ReturnNoException()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => TestResultController.GetAmountOfPauses(TestResultID));
        }

        [Test]
        public void GetTime_TestResultID_ReturnNoException()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => TestResultController.GetTime(TestResultID));
        }

        [Test]
        public void CalculatePercentageRight_TestResultID_ReturnNoException()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => TestResultController.CalculatePercentageRight(TestID, TestResultID));
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
