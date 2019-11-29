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
            public static void Add_ThreeNumbers_ReturnsSum()
            {
                //Arrange

                int result = 0;
                //Act
                result = calculator.Add("1,2,3");
                //Assert
                Assert.AreEqual(result, 6);
            }

        }

    }
}