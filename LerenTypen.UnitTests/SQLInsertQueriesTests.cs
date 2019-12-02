using NUnit.Framework;
using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;

namespace LerenTypen.UnitTests
{
    [TestFixture]
    class SQLInsertQueriesTests
    {
        

        List<string> Contents = new List<string>();

        [Test]
        [TestCase("Dummy toets", 1, 1, 0, Content, 1)]
        public void Add_Test_ReturnBool(string testName, int testType, int testDifficulty, int isPrivate, List<string> content, int uploadedBy, bool result)
        {
            //Arrange
            bool answer = false;
            //Act
            answer = Database.AddTest(testName, testType, testDifficulty, isPrivate, content, uploadedBy);
            //Assert
            Assert.AreEqual(result, answer);
        }
    }
}
