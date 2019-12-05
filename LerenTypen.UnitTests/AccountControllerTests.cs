using LerenTypen.Controllers;
using LerenTypen.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace LerenTypen.UnitTests
{
    class AccountControllerTests
    {
        #region Select
        [Test]
        public void GetUserAccount_AccountID_Account(int accountID, Account result)
        {
            //Arrange
            Account answer;
            //Act
            answer = AccountController.GetUserAccount(accountID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void IsAdmin_accountnumber_NoException(int accountNumber, bool result)
        {
            //Arrange
            bool answer;
            //Act
            answer = AccountController.IsAdmin(accountNumber);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetAccountType_accountID_AccountType(int accountID, int result)
        {
            //Arrange
            List<string> answerList = new List<string>();
            int answer;
            //Act
            answerList = AccountController.GetAccountType(accountID);
            answer = int.Parse(answerList[0]);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetUserName_accountID_Username(int accountID, string result)
        {
            //Arrange
            string answer = "";
            //Act
            answer = AccountController.GetUsername(accountID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetAllAccountInformationExceptPassword_accountID_AccountInformationExceptPassword(int accountID, object result)
        {
            //Arrange
            object answer; ;
            //Act
            answer = AccountController.GetAllAccountInformationExceptPassword(accountID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetPasswordFromAccount_accountID_Password(int accountID, string result)
        {
            //Arrange
            string answer = "";
            //Act
            answer = AccountController.GetPasswordFromAccount(accountID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetAllUsers_ReturnsAllUsers(List<UserTable> result)
        {
            //Arrange
            List<UserTable> answer;
            //Act
            answer = AccountController.GetAllUsers();
            //Assert
            Assert.AreEqual(result, answer);
        }
        #endregion

        #region Update
        [Test]
        public void UpdateAccount_AccountData_Bool(int accountID, string userName, DateTime birthday, string firstName, string surname, string securityQuestion, string securityAnswer, bool result)
        {
            //Arrange
            bool answer;
            //Act
            answer = AccountController.UpdateAccount(accountID, userName, birthday, firstName, surname, securityQuestion, securityAnswer);
            //Assert
            Assert.AreEqual(result, answer);
        }

        public void UpdateAccount_AccountData_Bool(string userName, string firstName, string surname, bool result)
        {
            //Arrange
            bool answer;
            //Act
            answer = AccountController.UpdateAccount(userName, firstName, surname);
            //Assert
            Assert.AreEqual(result, answer);
        }

        public void UpdateAccountWithPassword_AccountData_Bool(int accountID, string userName, string password, DateTime birthday, string firstName, string surname, string securityQuestion, string securityAnswer, bool result)
        {
            //Arrange
            bool answer;
            //Act
            answer = AccountController.UpdateAccountWithPassword(accountID, userName, password, birthday, firstName, surname, securityQuestion, securityAnswer);
            //Assert
            Assert.AreEqual(result, answer);
        }

        public void MakeAdmin_Username_Bool(string userName, bool result)
        {
            //Arrange
            bool answer;
            //Act
            answer = AccountController.MakeAdmin(userName);
            //Assert
            Assert.AreEqual(result, answer);
        }

        public void MakeStudent_Username_Bool(string userName, bool result)
        {
            //Arrange
            bool answer;
            //Act
            answer = AccountController.MakeStudent(userName);
            //Assert
            Assert.AreEqual(result, answer);
        }

        public void MakeTeacher_Username_Bool(string userName, bool result)
        {
            //Arrange
            bool answer;
            //Act
            answer = AccountController.MakeTeacher(userName);
            //Assert
            Assert.AreEqual(result, answer);
        }

        public void DeleteAccount_Username_Bool(string userName, bool result)
        {
            //Arrange
            bool answer;
            //Act
            answer = AccountController.DeleteAccount(userName);
            //Assert
            Assert.AreEqual(result, answer);
        }

        #endregion
    }
}
