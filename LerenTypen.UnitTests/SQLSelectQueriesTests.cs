using NUnit.Framework;

namespace LerenTypen.UnitTests
{
    [TestFixture]
    class SQLSelectQueriesTests
    {
        [Test]
        [TestCase("Piet", false)]
        [TestCase("H", true)]
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
        [TestCase("Piet", "Klaas", 0)]
        [TestCase("", "", 0)]
        [TestCase("H", "test123", 3)]
        public void GetAccountIDForLogin_UsernamePassword_IntID(string username, string password, int result)
        {
            //Arrange
            int answer = 0;
            //Act
            answer = Database.GetAccountIDForLogin(username, password);
            //Assert
            Assert.AreEqual(result, answer);
        }

        public void GetAccountUsername_intID_UserName(int ID, string result)
        {
            //Arrange
            string answer = "";
            //Act
            answer = Database.GetAccountUsername(ID);
            //Assert
            Assert.AreEqual(result, answer);
        }
    }
}
