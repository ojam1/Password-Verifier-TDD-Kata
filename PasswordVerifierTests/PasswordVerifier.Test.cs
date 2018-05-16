using NUnit.Framework;

namespace PasswordVerifier.Test
{
    [TestFixture]
    public class PasswordVerifierTests
    {
        private Verifier _verifier;
        [SetUp]
        public void SetUp()
        {
            _verifier = new Verifier();
        }

        [TestCase("abcdefghi","Valid")]
        [TestCase("abc","Invalid")]
        public void should_verify_password_longer_than_8_characters(string password, string expected)
        {
            Assert.AreEqual(_verifier.Verify(password), expected);
        }

        [Test]
        public void should_return_invalid_for_null_passwords()
        {
            Assert.AreEqual(_verifier.Verify(null), "Invalid");
        }
    }
}
