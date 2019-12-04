using LerenTypen.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace LerenTypen.UnitTests
{
    [TestFixture]
    public class SQLSelectQueriesTests
    {
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
            answer = Database.UserExists(username);
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
            answer = Database.GetAccountIDForLogin(username, password);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetAccountIDForUpdate_Username_AccountID(string accountUsername, int result)
        {
            //Arrange
            int answer = 0;
            //Act
            answer = Database.GetAccountIDForUpdate(accountUsername);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        // No Account class in this solution for testing yet
        public void GetUserAccount_AccountID_Account(int accountID, object result)
        {
            //Arrange
            object answer = 0;
            //Act
            answer = Database.GetUserAccount(accountID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void IsAdmin_accountnumber_bool(int accountNumber, bool result)
        {
            //Arrange
            bool answer;
            //Act
            answer = Database.IsAdmin(accountNumber);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetUsers_ReturnsListUser(List<User> result)
        {
            //Arrange
            List<User> answer = new List<User>();
            //Act
            answer = Database.GetUsers();
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetAccountType_accountID_ListString(int accountID, List<string> result)
        {
            //Arrange
            List<string> answer = new List<string>();
            //Act
            answer = Database.GetAccountType(accountID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetAllTests_ReturnsListTestTable(List<TestTable> result)
        {
            //Arrange
            List<TestTable> answer = new List<TestTable>();
            //Act
            answer = Database.GetAllTests();
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetAmountOfWordsFromTest_testID_int(int testID, int result)
        {
            //Arrange
            int answer = 0;
            //Act
            answer = Database.GetAmountOfWordsFromTest(testID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetUserName_accountID_string(int accountID, string result)
        {
            //Arrange
            string answer = "";
            //Act
            answer = Database.GetUserName(accountID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetTestInformation_testID_ListInt(int testID, List<int> result)
        {
            //Arrange
            List<int> answer = new List<int>();
            //Act
            answer = Database.GetTestInformation(testID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetTestName_testID_string(int testID, string result)
        {
            //Arrange
            string answer = "";
            //Act
            answer = Database.GetTestName(testID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetTestContent_testID_ListString(int testID, List<string> result)
        {
            //Arrange
            List<string> answer = new List<string>();
            //Act
            answer = Database.GetTestContent(testID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetAllMyTestswithIsPrivate_accountID_ListTestTable(int accountID, List<TestTable> result)
        {
            //Arrange
            List<TestTable> answer = new List<TestTable>();
            //Act
            answer = Database.GetAllMyTestswithIsPrivate(accountID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetAllMyTestsAlreadyMade_intIngelogd_ListTestTable(int ingelogd, List<TestTable> result)
        {
            //Arrange
            List<TestTable> answer = new List<TestTable>();
            //Act
            answer = Database.GetAllMyTestsAlreadyMade(ingelogd);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetTestResults_testResultsID_ListString(int testResultsID, List<string> result)
        {
            //Arrange
            List<string> answer = new List<string>();
            //Act
            answer = Database.GetTestResults(testResultsID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetTestResultsContentRight_testResultsID_ListString(int testResultsID, List<string> result)
        {
            //Arrange
            List<string> answer = new List<string>();
            //Act
            answer = Database.GetTestResultsContentRight(testResultsID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetTestResultsContentWrong_testResultsID_ListString(int testResultsID, List<string> result)
        {
            //Arrange
            List<string> answer = new List<string>();
            //Act
            answer = Database.GetTestResultsContentWrong(testResultsID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetTestResultsContentHadToBe_testResultsID_ListString(int testResultsID, List<string> result)
        {
            //Arrange
            List<string> answer = new List<string>();
            //Act
            answer = Database.GetTestResultsContentHadToBe(testResultsID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetAllAccountInformationExceptPassword_accountID_Account(int accountID, object result)
        {
            //Arrange
            object answer; ;
            //Act
            answer = Database.GetAllAccountInformationExceptPassword(accountID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetPasswordFromAccount_accountID_String(int accountID, string result)
        {
            //Arrange
            string answer = "";
            //Act
            answer = Database.GetPasswordFromAccount(accountID);
            //Assert
            Assert.AreEqual(result, answer);
        }

        [Test]
        public void GetAllTestsAlreadyMade_intIngelogd_ListTestTable(int ingelogd, List<TestTable> result)
        {
            //Arrange
            List<TestTable> answer = new List<TestTable>();
            //Act
            answer = Database.GetAllTestsAlreadyMade(ingelogd);
            //Assert
            Assert.AreEqual(result, answer);
        }
    }
}
