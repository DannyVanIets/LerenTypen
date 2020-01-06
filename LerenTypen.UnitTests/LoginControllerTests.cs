using LerenTypen.Controllers;
using LerenTypen.Models;
using NUnit.Framework;
using System;

namespace LerenTypen.UnitTests
{
    class LoginControllerTests
    {
        public LoginControllerTests()
        {
            Database.Connect();
        }

        #region Select
        public void GetAccountIDForLogin_UsernameAndPassword_ReturnNoException()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => LoginController.GetAccountIDForLogin("test@test.nl", "test123"));
        }

        public void UserExists_Username_ReturnNoException()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => LoginController.UserExists("test@test.nl"));
        }
        #endregion

        #region Insert
        // RegisterUser
        #endregion

        [Test]
        public void ComputeSha256Hash_CheckHash_ReturnsSHA256Hash()
        {
            string result = LoginController.ComputeSha256Hash("test123");
            Assert.AreEqual(result, "ecd71870d1963316a97e3ac3408c9835ad8cf0f3c1bc703527c30265534f75ae");
        }
    }
}
