using LerenTypen.Controllers;
using LerenTypen.Models;
using NUnit.Framework;

namespace LerenTypen.UnitTests
{
    class LoginControllerTests
    {
        #region Select
        [Test]
        // Happy
        [TestCase("H", true)]
        // Unhappy
        [TestCase("Piet", false)]
        [TestCase("", false)]
        [TestCase("Danny", false)]
        public void UserExists_Username_Bool(string username, bool result)
        {
            //Arrange
            bool answer = false;
            //Act
            answer = LoginController.UserExists(username);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        // Happy
        [TestCase("H", "test123", 3)]
        // Unhappy
        [TestCase("Piet", "Klaas", 0)]
        [TestCase("", "", 0)]
        public void GetAccountIDForLogin_UsernamePassword_IntID(string username, string password, int result)
        {
            //Arrange
            int answer = 0;
            //Act
            answer = LoginController.GetAccountIDForLogin(username, password);
            //Assert
            Assert.AreEqual(result, answer);
        }
        #endregion
    }
}
