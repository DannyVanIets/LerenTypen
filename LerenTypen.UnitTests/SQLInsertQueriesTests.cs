using NUnit.Framework;

namespace LerenTypen.UnitTests
{
    [TestFixture]
    class SQLInsertQueriesTests
    {
        [TestFixture]
        public class StringCalculatorTests
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

        }

    }
}