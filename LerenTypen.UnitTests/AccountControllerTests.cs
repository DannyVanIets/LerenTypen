using LerenTypen.Controllers;
using LerenTypen.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace LerenTypen.UnitTests
{
    class AccountControllerTests
    {
        #region Select
        [Test]
        // No Account class in this solution for testing yet
        public void GetUserAccount_AccountID_Account(int accountID, object result)
        {
            //Arrange
            object answer = 0;
            //Act
            answer = AccountController.GetUserAccount(accountID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void IsAdmin_accountnumber_bool(int accountNumber, bool result)
        {
            //Arrange
            bool answer;
            //Act
            answer = AccountController.IsAdmin(accountNumber);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetAccountType_accountID_ListString(int accountID, List<string> result)
        {
            //Arrange
            List<string> answer = new List<string>();
            //Act
            answer = AccountController.GetAccountType(accountID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetUserName_accountID_string(int accountID, string result)
        {
            //Arrange
            string answer = "";
            //Act
            answer = AccountController.GetUsername(accountID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetAllAccountInformationExceptPassword_accountID_Account(int accountID, object result)
        {
            //Arrange
            object answer; ;
            //Act
            answer = AccountController.GetAllAccountInformationExceptPassword(accountID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetPasswordFromAccount_accountID_String(int accountID, string result)
        {
            //Arrange
            string answer = "";
            //Act
            answer = AccountController.GetPasswordFromAccount(accountID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetAllUsers(List<UserTable> result)
        {
            //Arrange
            List<UserTable> answer;
            //Act
            answer = AccountController.GetAllUsers();
            //Assert
            Assert.AreEqual(result, answer);
        }
        #endregion

    }
}
