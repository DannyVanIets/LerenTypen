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
        [TestCase("H", "ecd71870d1963316a97e3ac3408c9835ad8cf0f3c1bc703527c30265534f75ae", 2)]
        // Unhappy
        [TestCase("Piet", "Klaas", 0)]
        [TestCase("", "", 0)]
        public void GetAccountIDForLogin_LoginData_ID(string username, string password, int result)
        {
            //Arrange
            int answer = 0;
            //Act
            answer = LoginController.GetAccountIDForLogin(username, password);
            //Assert
            Assert.AreEqual(result, answer);
        }
        #endregion

        #region Insert
        // new DateTime
        public void RegisterUser_Userdata_Bool(string username, string password, DateTime birthday, string firstname, string lastname, string securityvraag, string securityanswer, bool result)
        {
            //Arrange
            bool answer = false;
            //Act
            answer = LoginController.RegisterUser(username, password, birthday, firstname, lastname, securityvraag, securityanswer);
            //Assert
            Assert.AreEqual(result, answer);
        }
        #endregion

        [Test]
        public void ComputeSha256Hash_CheckHash_ReturnsSHA256Hash()
        {
            string result = LoginController.ComputeSha256Hash("test123");
            Assert.AreEqual(result, "ecd71870d1963316a97e3ac3408c9835ad8cf0f3c1bc703527c30265534f75ae");
        }
    }
}
