using System.Collections.Generic;
using NUnit.Framework;

namespace PasswordVerifier.Test
{
    [TestFixture]
    public class PasswordVerifierTests
    {
        private readonly IEnumerable<IRule> _rules = new IRule[]
        {
            new MoreThanEightCharacters(),
            new ContainsAtLeastOneUppercaseCharacter(),
            new ContainsAtLeastOneLowercaseCharacter(),
            new ContainsAtLeastOneNumber()
        };

        [TestCase("1bcdefghI", "Valid")]
        public void Should_verify_password(string password, string expectation)
        {
            var verifier = new Verifier(_rules, password);
            Assert.AreEqual(verifier.Verify(), expectation);
        }

        [TestCase("a", "Password should be at least 8 characters")]
        [TestCase("abcdefghi", "Password should contain at least one uppercase character")]
        [TestCase("ABCDEFGHI", "Password should contain at least one lowercase character")]
        [TestCase("aBcDeFgHi", "Password should contain at least one number")]
        public void Passwords_throws_exception_if_conditions_not_met(string password, string exceptation)
        {
            var verifier = new Verifier(_rules, password);
            var ex = Assert.Throws<IncorrectPassword>(() => verifier.Verify());
            Assert.That(ex.Message, Is.EqualTo(exceptation));
        }

        [Test]
        public void Should_return_invalid_for_null_passwords()
        {
            var ex = Assert.Throws<IncorrectPassword>(() => new Verifier(_rules, null));
            Assert.AreEqual(ex.Message, "Password is null or empty");
        }
    }
}
