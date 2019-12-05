using LerenTypen.Controllers;
using LerenTypen.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace LerenTypen.UnitTests
{
    class AccountControllerTests
    {
        public AccountControllerTests()
        {
            Database.Connect();
        }

        #region Select
        [Test]
        //[TestCase(1, new Account("HenkerDenker", new DateTime(1990, 10, 09), "Henk", "Denk"))]
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
        // Happy
        [TestCase(1, true)]
        [TestCase(2, true)]
        [TestCase(3, false)]
        // Unhappy
        [TestCase(500, false)]
        [TestCase(int.MaxValue, false)]
        public void IsAdmin_accountnumber_IsAdmin(int accountID, bool result)
        {
            //Arrange
            bool answer;
            //Act
            answer = AccountController.IsAdmin(accountID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        // Happy
        [TestCase(1, 2)]
        [TestCase(2, 2)]
        [TestCase(3, 0)]
        // Unhappy
        [TestCase(500, null)]
        [TestCase(int.MaxValue, null)]
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
        // Happy
        [TestCase(1, "HenkerDenker")]
        [TestCase(2, "H")]
        [TestCase(1, "Danny van Iets")]
        // Unhappy
        [TestCase(500, null)]
        [TestCase(int.MaxValue, null)]
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
        //[TestCase(1, new Account())]
        public void GetAllAccountInformationExceptPassword_accountID_AccountInformationExceptPassword(int accountID, Account result)
        {
            //Arrange
            Account answer;
            //Act
            answer = AccountController.GetAllAccountInformationExceptPassword(accountID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        // Happy
        [TestCase(1, "ecd71870d1963316a97e3ac3408c9835ad8cf0f3c1bc703527c30265534f75ae")]
        [TestCase(3, "1b5b02d88f6890414e6b149ba6f1f17a611eed6f0cdb5befa9a415f1644af872")]
        // Unhappy
        [TestCase(500, null)]
        [TestCase(int.MaxValue, null)]
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
        //[TestCase(new List<UserTable>())]
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
        //new DateTime
        public void UpdateAccount_AccountData_Bool(int accountID, string userName, DateTime birthday, string firstName, string surname, string securityQuestion, string securityAnswer, bool result)
        {
            //Arrange
            bool answer;
            //Act
            answer = AccountController.UpdateAccount(accountID, userName, birthday, firstName, surname, securityQuestion, securityAnswer);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        // Happy
        [TestCase("H", "Henker", "Pranker", true)]
        // Unhappy
        [TestCase("DannyVanIets", "Henker", "Denker", true)]
        [TestCase("H", "Danny", "Van Iets", true)]
        public void UpdateAccount_AccountData_Bool(string userName, string firstName, string surname, bool result)
        {
            //Arrange
            bool answer;
            //Act
            answer = AccountController.UpdateAccount(userName, firstName, surname);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        //new DateTime
        public void UpdateAccountWithPassword_AccountData_Bool(int accountID, string userName, string password, DateTime birthday, string firstName, string surname, string securityQuestion, string securityAnswer, bool result)
        {
            //Arrange
            bool answer;
            //Act
            answer = AccountController.UpdateAccountWithPassword(accountID, userName, password, birthday, firstName, surname, securityQuestion, securityAnswer);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        // Happy
        [TestCase("HenkerDenker", true)]
        [TestCase("Danny van Iets", true)]
        // Unhappy
        [TestCase("SJON", true)]
        [TestCase("", true)]
        public void MakeAdmin_Username_Bool(string userName, bool result)
        {
            //Arrange
            bool answer;
            //Act
            answer = AccountController.MakeAdmin(userName);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        // Happy
        [TestCase("HenkerDenker", true)]
        [TestCase("Danny van Iets", true)]
        // Unhappy
        [TestCase("SJON", true)]
        [TestCase("", true)]
        public void MakeStudent_Username_Bool(string userName, bool result)
        {
            //Arrange
            bool answer;
            //Act
            answer = AccountController.MakeStudent(userName);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        // Happy
        [TestCase("HenkerDenker", true)]
        [TestCase("Danny van Iets", true)]
        // Unhappy
        [TestCase("SJON", true)]
        [TestCase("", true)]
        public void MakeTeacher_Username_Bool(string userName, bool result)
        {
            //Arrange
            bool answer;
            //Act
            answer = AccountController.MakeTeacher(userName);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        // Happy
        [TestCase("Danny van Zonder", true)]
        // Unhappy
        [TestCase("SJON", true)]
        [TestCase("", true)]
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
