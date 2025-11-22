
using NUnit.Framework;
using System;
using SafeVault.Utils;
using SafeVault.Data;

namespace SafeVault.Tests
{
    [TestFixture]
    public class TestInputValidation
    {
        [Test]
        public void TestSanitizeUsername_RemovesMaliciousChars()
        {
            var input = "user; DROP TABLE Users; --";
            var sanitized = InputValidator.SanitizeUsername(input);
            Assert.AreEqual("userDROPTABLEUsers", sanitized);
        }

        [Test]
        public void TestSanitizeEmail_InvalidThrows()
        {
            Assert.Throws<ArgumentException>(() => InputValidator.SanitizeEmail("bademail@com"));
        }

        [Test]
        public void TestSQLInjection_Prevented()
        {
            var repo = new UserRepository("Server=localhost;Database=SafeVault;Uid=root;Pwd=;");
            var maliciousInput = "admin'; DROP TABLE Users; --";

            Assert.DoesNotThrow(() =>
            {
                var sanitizedUsername = InputValidator.SanitizeUsername(maliciousInput);
                repo.AddUser(sanitizedUsername, "safe@example.com");
            });
        }

        [Test]
        public void TestXSS_Prevented()
        {
            var maliciousInput = "<script>alert('xss')</script>";
            var sanitized = InputValidator.SanitizeUsername(maliciousInput);
            Assert.IsFalse(sanitized.Contains("<script>"));
        }
    }
}