using LerenTypen.Controllers;
using LerenTypen.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace LerenTypen.UnitTests
{
    class ReviewControllerTests
    {
        private int ReviewID { get; set; }
        private int AccountID { get; set; }
        private int TestID { get; set; }

        public ReviewControllerTests()
        {
            Database.Connect();
            // Arrange
            AccountID = Database.GetFirstAccountID();
            ReviewID = Database.GetFirstReviewID();
            TestID = Database.GetFirstTestResultID();
        }

        #region Select
        [Test]
        public void IsAdmin_accountnumber_ReturnNoException()
        {
            // Arrange
            int adminAccountID = Database.GetFirstAdminAccount();
            // Act & Assert
            Assert.DoesNotThrow(() => AccountController.IsAdmin(adminAccountID));
        }

        [Test]
        public void CheckIfUserHasMadeAReview_TestIDAndAccountID_ReturnNoException()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => ReviewController.CheckIfUserHasMadeAReview(TestID, AccountID));
        }

        [Test]
        public void GetUserReviewDetails_TestID_ReturnNoException()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => ReviewController.GetUserReviewDetails(TestID));
        }

        [Test]
        public void GetRatingScore_TestID_ReturnNoException()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => ReviewController.GetRatingScore(TestID));
        }
        #endregion
    }
}

