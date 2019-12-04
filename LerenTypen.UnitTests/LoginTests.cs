using NUnit.Framework;
using LerenTypen;
using LerenTypen.Models;

namespace Tests
{
    [TestFixture]
    public class LoginTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void ComputeSha256Hash_CheckHash_ReturnsSHA256Hash()
        {
            Converter converter = new Converter();
            string result = converter.ComputeSha256Hash("test123");
            Assert.AreEqual(result, "ecd71870d1963316a97e3ac3408c9835ad8cf0f3c1bc703527c30265534f75ae");
        }
    }
}