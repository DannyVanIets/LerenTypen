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
        // Happy 
        [TestCase(1, "HenkerDenker", 1990, 10, 9, "Henk", "Denk", false)]
        // Unhappy
        [TestCase(0, null, null, null, null, null, null, true)]
        public void GetUserAccount_AccountID_Account(int accountID, string resultUsername, int resultBirthYear, int resultBirthMonth, int resultBirthDay, string resultFirstName, string resultSurname, bool expectNull)
        {
            //Arrange
            Account answer;
            if (!expectNull)
            {
                DateTime birthday = new DateTime(resultBirthYear, resultBirthMonth, resultBirthDay);

                Account result = new Account(resultUsername, birthday, resultFirstName, resultSurname);

                //Act
                answer = AccountController.GetUserAccount(accountID);
                //Assert
                Assert.AreEqual(result.Birthdate, answer.Birthdate);
                Assert.AreEqual(result.FirstName, answer.FirstName);
                Assert.AreEqual(result.Surname, answer.Surname);
                Assert.AreEqual(result.UserName, answer.UserName);
            }
            else
            {
                answer = AccountController.GetUserAccount(accountID);
                Assert.AreEqual(null, answer);
            }
        }

        [Test]
        // Happy
        [TestCase(1, false)]
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
        [TestCase(3, "Danny van Iets")]
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
        // Happy
        [TestCase(1, "HenkerDenker", 1990, 10, 9, "Henk", "Denk", "Wat is je geboorteplaats?", "Henk Town", false)]
        // Unhappy
        [TestCase(0, null, 1990, 10, 9, null, null, null, null, true)]
        public void GetAllAccountInformationExceptPassword_accountID_AccountInformationExceptPassword(int accountID, string resultUserName, int resultBirthYear, int resultBirthMonth, int resultBirthDay, string resultFirstName, string resultSurName, string securityQuestion, string securityAnswer, bool ExpectNull)
        {

            //Arrange
            Account answer;
            if (!ExpectNull)
            {
                DateTime birthdate = new DateTime(resultBirthYear, resultBirthMonth, resultBirthDay);
                Account result = new Account(resultUserName, birthdate, resultFirstName, resultSurName, securityQuestion, securityAnswer);
                //Act
                answer = AccountController.GetAllAccountInformationExceptPassword(accountID);
                //Assert
                Assert.AreEqual(result.Birthdate, answer.Birthdate);
                Assert.AreEqual(result.FirstName, answer.FirstName);
                Assert.AreEqual(result.SecurityAnswer, answer.SecurityAnswer);
                Assert.AreEqual(result.SecurityQuestion, answer.SecurityQuestion);
                Assert.AreEqual(result.Surname, answer.Surname);
                Assert.AreEqual(result.UserName, answer.UserName);
            }
            else
            {
                answer = AccountController.GetAllAccountInformationExceptPassword(accountID);
                Assert.AreEqual(answer, null);
            }

        }

        [Test]
        // Happy
        [TestCase(2, "ecd71870d1963316a97e3ac3408c9835ad8cf0f3c1bc703527c30265534f75ae")]
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
        // Unhappy
        [TestCase(null)]
        public void GetAllUsers_ReturnsAllUsers(int result)
        {
            //Arrange
            List<UserTable> answer;
            //Act
            answer = AccountController.GetAllUsers();
            //Assert
            Assert.AreNotEqual(result, answer);
        }
        #endregion

        #region Update
        [Test]
        // Happy
        [TestCase(1, "HenkerDenker", 9, 10, 1990, "Henk", "Denk", "Wat is je geboorteplaats?", "Henk Town", true)]
        // Unhappy
        [TestCase(15, "OR 1=1'", 1, 1, 1990, "sjon", "nie", "", "", true)]
        [TestCase(15, "any'' OR 1=1 --", 1, 1, 1990, "sjon", "nie", "", "", true)]
        public void UpdateAccount_AccountData_Bool(int accountID, string userName, int birthDay, int birthMonth, int birthYear, string firstName, string surname, string securityQuestion, string securityAnswer, bool result)
        {
            //Arrange
            bool answer;
            DateTime birthDayDate = new DateTime(birthYear, birthMonth, birthDay);
            //Act
            answer = AccountController.UpdateAccount(accountID, userName, birthDayDate, firstName, surname, securityQuestion, securityAnswer);
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
        // Happy
        [TestCase(1, "HenkerDenker", "test123", 9, 10, 1990, "Henk", "Denk", "Wat is je geboorteplaats?", "Henk Town", true)]
        // Unhappy
        [TestCase(0, "HenkerDenker", "test123", 9, 10, 1990, "Henk", "Denk", "Wat is je geboorteplaats?", "Henk Town", true)]
        public void UpdateAccountWithPassword_AccountData_Bool(int accountID, string userName, string password, int birthDay, int birthMonth, int birthYear, string firstName, string surname, string securityQuestion, string securityAnswer, bool result)
        {
            //Arrange
            DateTime birthDayDate = new DateTime(birthYear, birthMonth, birthDay);
            bool answer;
            //Act
            answer = AccountController.UpdateAccountWithPassword(accountID, userName, password, birthDayDate, firstName, surname, securityQuestion, securityAnswer);
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
