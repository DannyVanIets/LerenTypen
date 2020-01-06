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
        public TestControllerTests()
        {
            Database.Connect();
        }       

        #region Select
        [Test]
        public void GetTestHighScore_TestID_ReturnNoException()
        {
            // Arrange
            int testID = Database.GetFirstTestID();
            // Act & Assert
            Assert.DoesNotThrow(() => TestController.GetTestHighscore(testID));
        }

        [Test]
        public void GetUserRating_TestAndUserID_ReturnNoException()
        {
            // Arrange
            int testID = Database.GetFirstAccountID();
            int userID = Database.GetFirstAccountID();
            // Act & Assert
            Assert.DoesNotThrow(() => TestController.GetUserRating(testID, userID));
        }

        [Test]
        public void GetTestAverageScore_TestID_ReturnNoException()
        {
            // Arrange
            int testID = Database.GetFirstTestID();
            // Act & Assert
            Assert.DoesNotThrow(() => TestController.GetTestAverageScore(testID));
        }

        [Test]
        public void GetTest_TestID_ReturnNoException()
        {
            // Arrange
            int testID = Database.GetFirstTestID();
            // Act & Assert
            Assert.DoesNotThrow(() => TestController.GetTest(testID));
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
            // Arrange
            int testID = Database.GetFirstTestID();
            // Act & Assert
            Assert.DoesNotThrow(() => TestController.GetTrendingTestIDs(3));
        }

        [Test]
        public void GetTrendingTests_ReturnNoException()
        {
            // Arrange
            int testID = Database.GetFirstTestID();
            // Act & Assert
            Assert.DoesNotThrow(() => TestController.GetTrendingTestIDs(testID));
        }

        [Test]
        public void GetTrendingTestsNameAndID_ReturnNoException()
        {
            // Arrange
            int testID = Database.GetFirstTestID();
            // Act & Assert
            Assert.DoesNotThrow(() => TestController.GetTrendingTestsNameAndID());
        }

        [Test]
        public void GetWordHighscore_TestID_ReturnNoException()
        {
            // Arrange
            int testID = Database.GetFirstTestID();
            // Act & Assert
            Assert.DoesNotThrow(() => TestController.GetWordHighscore(testID));
        }

        [Test]
        public void GetTestInformation_TestID_ReturnNoException()
        {
            // Arrange
            int testID = Database.GetFirstTestID();
            // Act & Assert
            Assert.DoesNotThrow(() => TestController.GetTestInformation(testID));
        }

        [Test]
        public void GetTop3FastestTypers_TestID_ReturnNoException()
        {
            // Arrange
            int testID = Database.GetFirstTestID();
            // Act & Assert
            Assert.DoesNotThrow(() => TestController.GetTop3FastestTypers(testID));
        }

        [Test]
        public void GetTop3Highscores_TestID_ReturnNoException()
        {
            // Arrange
            int testID = Database.GetFirstTestID();
            // Act & Assert
            Assert.DoesNotThrow(() => TestController.GetTop3Highscores(testID));
        }

        [Test]
        public void GetTestName_TestID_ReturnNoException()
        {
            // Arrange
            int testID = Database.GetFirstTestID();
            // Act & Assert
            Assert.DoesNotThrow(() => TestController.GetTestName(testID));
        }

        [Test]
        public void GetTestContent_TestID_ReturnNoException()
        {
            // Arrange
            int testID = Database.GetFirstTestID();
            // Act & Assert
            Assert.DoesNotThrow(() => TestController.GetTestContent(testID));
        }

        [Test]
        public void GetMyTestNames_AccountID_ReturnNoException()
        {
            // Arrange
            int accountID = Database.GetFirstAccountID();
            // Act & Assert
            Assert.DoesNotThrow(() => TestController.GetMyTestNames(accountID));
        }

        [Test]
        public void GetAllMyTestswithIsPrivate_AccountID_ReturnNoException()
        {
            // Arrange
            int accountID = Database.GetFirstAccountID();
            // Act & Assert
            Assert.DoesNotThrow(() => TestController.GetAllMyTestswithIsPrivate(accountID));
        }

        [Test]
        public void GetAllMyTestsAlreadyMade_AccountID_ReturnNoException()
        {
            // Arrange
            int accountID = Database.GetFirstAccountID();
            // Act & Assert
            Assert.DoesNotThrow(() => TestController.GetAllMyTestswithIsPrivate(accountID));
        }

        [Test]
        public void GetAmountOfWordsFromTest_TestID_ReturnNoException()
        {
            // Arrange
            int testID = Database.GetFirstTestID();
            // Act & Assert
            Assert.DoesNotThrow(() => TestController.GetAmountOfWordsFromTest(testID));
        }

        [Test]
        public void GetAllTestsAlreadyMade_AccountID_ReturnNoException()
        {
            // Arrange
            int accountID = Database.GetFirstAccountID();
            // Act & Assert
            Assert.DoesNotThrow(() => TestController.GetAllTestsAlreadyMade(accountID));
        }

        [Test]
        public void GetTimesMade_TestID_ReturnNoException()
        {
            // Arrange
            int testID = Database.GetFirstTestID();
            // Act & Assert
            Assert.DoesNotThrow(() => TestController.GetTimesMade(testID));
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
            // Arrange
            int accountID = Database.GetFirstAccountID();
            // Act & Assert
            Assert.DoesNotThrow(() => TestController.GetUnfinishedTestIDsFromAccount(accountID));
        }

        [Test]
        public void GetAllLinesFromResult_TestID_ReturnNoException()
        {
            // Arrange
            int testResultID = Database.GetFirstTestResultID();
            // Act & Assert
            Assert.DoesNotThrow(() => TestController.GetAllLinesFromResult(testResultID));
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
