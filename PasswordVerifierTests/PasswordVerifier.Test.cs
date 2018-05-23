using NUnit.Framework;

namespace PasswordVerifier.Test
{
    [TestFixture]
    public class PasswordVerifierTests
    {
        [TestCase("1bcdefghI", "Valid")]
        public void Should_verify_password(string password, string expection)
        {
            var verifier = new Verifier(password);
            Assert.AreEqual(verifier.Verify(), expection);
        }

        [Test]
        public void Should_pass_when_three_conditions_met()
        {
            var verifier = new Verifier("abcdefghI");
            Assert.AreEqual(verifier.Verify(), "Valid");
        }

        [TestCase("a", "Password should be at least 8 characters")]
        [TestCase("abcdefghi", "Password should contain at least one uppercase character")]
        [TestCase("ABCDEFGHI", "Password should contain at least one lowercase character")]
        [TestCase("aBcDeFgHi", "Password should contain at least one number")]
        public void Passwords_throws_exception_if_conditions_not_met(string password, string exception)
        {
            var verifier = new Verifier(password);
            var ex = Assert.Throws<IncorrectPassword>(() => verifier.Verify());
            Assert.That(ex.Message, Is.EqualTo(exception));
        }

        [Test]
        public void Should_return_invalid_for_null_passwords()
        {
            var ex = Assert.Throws<IncorrectPassword>(() => new Verifier(null));
            Assert.AreEqual(ex.Message, "Password is null or empty");
        }
    }
}
