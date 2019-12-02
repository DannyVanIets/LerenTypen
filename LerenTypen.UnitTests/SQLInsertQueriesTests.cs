using NUnit.Framework;

namespace LerenTypen.UnitTests
{
    [TestFixture]

    /*
    command.Parameters.AddWithValue("@testName", testName);
    command.Parameters.AddWithValue("@testType", testType);
    command.Parameters.AddWithValue("@testDifficulty", testDifficulty);
    command.Parameters.AddWithValue("@isPrivate", isPrivate);
    command.Parameters.AddWithValue("@uploadedBy", uploadedBy);
    */

    class SQLInsertQueriesTests
    {
        [Test]
        [TestCase()]
        public void Add_Test_ReturnBool(string username, bool result)
        {
            //Arrange
            bool answer = false;
            //Act
            //answer = Database.AddTest();
            //Assert
            Assert.AreEqual(result, answer);
        }
    }
}
