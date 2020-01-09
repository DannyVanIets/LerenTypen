using LerenTypen.Controllers;
using NUnit.Framework;

namespace LerenTypen.UnitTests
{
    class LeaderboardControllerTests
    {
        public LeaderboardControllerTests()
        {
            Database.Connect();
        }

        #region Select
        public void GetHardTests_ReturnNoException()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => LeaderboardController.GetHardTests(0));
        }

        public void GetMediumTests_ReturnNoException()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => LeaderboardController.GetMediumTests(0));
        }

        public void GetEasyTests_ReturnNoException()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => LeaderboardController.GetEasyTests(0));
        }
        #endregion
    }
}
