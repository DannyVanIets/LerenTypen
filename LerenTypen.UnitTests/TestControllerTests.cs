using LerenTypen.Controllers;
using LerenTypen.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace LerenTypen.UnitTests
{
    class TestControllerTests
    {
        private int TestResultID;
        private int TestID;
        private int AccountID;

        public TestControllerTests()
        {
            Database.Connect();

            TestResultID = Database.GetFirstTestResultID();
            TestID = Database.GetFirstTestID();
            AccountID = Database.GetFirstAccountID();
        }       

        #region Select
        [Test]
        public void GetTestHighScore_TestID_ReturnNoException()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => TestController.GetTestHighscore(TestID));
        }

        [Test]
        public void GetUserRating_TestAndUserID_ReturnNoException()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => TestController.GetUserRating(TestID, AccountID));
        }

        [Test]
        public void GetTestAverageScore_TestID_ReturnNoException()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => TestController.GetTestAverageScore(TestID));
        }

        [Test]
        public void GetTest_TestID_ReturnNoException()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => TestController.GetTest(TestID));
        }

        [Test]
        public void GetAllTests_ReturnNoException()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => TestController.GetAllTests());
        }

        [Test]
        public void GetTrendingTestIDs_ReturnNoException()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => TestController.GetTrendingTestIDs(3));
        }

        [Test]
        public void GetTrendingTests_ReturnNoException()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => TestController.GetTrendingTestIDs(TestID));
        }

        [Test]
        public void GetTrendingTestsNameAndID_ReturnNoException()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => TestController.GetTrendingTestsNameAndID());
        }

        [Test]
        public void GetWordHighscore_TestID_ReturnNoException()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => TestController.GetWordHighscore(TestID));
        }

        [Test]
        public void GetTestInformation_TestID_ReturnNoException()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => TestController.GetTestInformation(TestID));
        }

        [Test]
        public void GetTop3FastestTypers_TestID_ReturnNoException()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => TestController.GetTop3FastestTypers(TestID));
        }

        [Test]
        public void GetTop3Highscores_TestID_ReturnNoException()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => TestController.GetTop3Highscores(TestID));
        }

        [Test]
        public void GetTestName_TestID_ReturnNoException()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => TestController.GetTestName(TestID));
        }

        [Test]
        public void GetTestContent_TestID_ReturnNoException()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => TestController.GetTestContent(TestID));
        }

        [Test]
        public void GetMyTestNames_AccountID_ReturnNoException()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => TestController.GetMyTestNames(AccountID));
        }

        [Test]
        public void GetAllMyTestswithIsPrivate_AccountID_ReturnNoException()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => TestController.GetAllMyTestswithIsPrivate(AccountID));
        }

        [Test]
        public void GetAllMyTestsAlreadyMade_AccountID_ReturnNoException()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => TestController.GetAllMyTestswithIsPrivate(AccountID));
        }

        [Test]
        public void GetAmountOfWordsFromTest_TestID_ReturnNoException()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => TestController.GetAmountOfWordsFromTest(TestID));
        }

        [Test]
        public void GetAllTestsAlreadyMade_AccountID_ReturnNoException()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => TestController.GetAllTestsAlreadyMade(AccountID));
        }

        [Test]
        public void GetTimesMade_TestID_ReturnNoException()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => TestController.GetTimesMade(TestID));
        }

        [Test]
        public void GetAllMyTestsAlreadyMadeTop3_AccountID_ReturnNoException()
        {
            // Arrange
            int accountID = Database.GetFirstAccountID();
            // Act & Assert
            Assert.DoesNotThrow(() => TestController.GetAllMyTestsAlreadyMadeTop3(accountID));
        }

        [Test]
        public void GetUnfinishedTestIDsFromAccount_AccountID_ReturnNoException()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => TestController.GetUnfinishedTestIDsFromAccount(AccountID));
        }

        [Test]
        public void GetAllLinesFromResult_TestResultID_ReturnNoException()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => TestController.GetAllLinesFromResult(TestResultID));
        }
        #endregion

        #region Insert        
        public void AddTestandContent_TestContent_ReturnNoException(string testName, int testType, int testDifficulty, int isPrivate, List<string> content, int uploadedBy, bool result)
        {
            //Arrange
            bool answer;
            //Act
            answer = TestController.AddTest(testName, testType, testDifficulty, isPrivate, content, uploadedBy, 1);
            //Assert
            Assert.AreEqual(result, answer);
        }

        // AddTest

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

        public void UpdateTestToArchived_testID_NoException(int testID, bool result)
        {
            //Arrange
            bool answer;
            //Act
            answer = TestController.UpdateTestToArchived(testID);
            //Assert
            Assert.AreEqual(result, answer);
        }


        // SetBeingEdited

        // UnsetBeingEdited
        #endregion
    }
}
