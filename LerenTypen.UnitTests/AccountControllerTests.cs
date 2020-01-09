using LerenTypen.Controllers;
using LerenTypen.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace LerenTypen.UnitTests
{
    class AccountControllerTests
    {
        private int AccountID { get; set; }

        public AccountControllerTests()
        {
            Database.Connect();
            // Arrange
            AccountID = Database.GetFirstAccountID();
        }

        #region Select[Test]
        public void GetAllAccountInformation_accountID_ReturnNoException()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => AccountController.GetAllAccountInformation(AccountID));
        }

        [Test]
        public void GetAmountOfAdmins_AccountID_ReturnNoException()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => AccountController.GetAmountOfAdmins(AccountID));
        }

        [Test]
        public void GetUserAccount_AccountID_ReturnNoException()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => AccountController.GetUserAccount(AccountID));
        }

        [Test]
        public void GetAccountNamesAndBirthdate_AccountID_ReturnNoException()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => AccountController.GetAccountNamesAndBirthdate(AccountID));
        }

        [Test]
        public void GetUserName_accountID_ReturnNoException()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => AccountController.GetUsername(AccountID));
        }

        [Test]
        public void GetAllAccountInformationExceptPassword_accountID_ReturnNoException()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => AccountController.GetAllAccountInformationExceptPassword(AccountID));
        }

        [Test]
        public void GetPasswordFromAccount_accountID_ReturnNoException()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => AccountController.GetPasswordFromAccount(AccountID));
        }

        [Test]
        public void GetLast3TestsMade_accountID_ReturnNoException()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => AccountController.GetLast3TestsMade());
        }

        [Test]
        public void GetAllUsers_ReturnNoException()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => AccountController.GetAllUsers());
        }

        [Test]
        public void IsAdmin_accountnumber_ReturnNoException()
        {
            // Arrange
            int adminAccountID = Database.GetFirstAdminAccount();
            // Act & Assert
            Assert.DoesNotThrow(() => AccountController.IsAdmin(adminAccountID));
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

