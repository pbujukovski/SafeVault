using NUnit.Framework;
using SafeVault.Utils;
using SafeVault.Data;
using System;

namespace SafeVault.Tests
{
    [TestFixture]
    public class TestAuthAndAuthorization
    {
        private readonly string _connectionString = "Server=localhost;Database=SafeVault;Uid=root;Pwd=;";

        [Test]
        public void TestPasswordHashingAndVerification()
        {
            var password = "SecurePass123!";
            var hash = Authentication.HashPassword(password);

            Assert.IsTrue(Authentication.VerifyPassword(password, hash));
            Assert.IsFalse(Authentication.VerifyPassword("WrongPass", hash));
        }

        [Test]
        public void TestUserAuthentication()
        {
            var repo = new UserRepository(_connectionString);
            repo.RegisterUser("testuser", "test@example.com", "TestPass123!");

            Assert.IsTrue(repo.AuthenticateUser("testuser", "TestPass123!"));
            Assert.IsFalse(repo.AuthenticateUser("testuser", "WrongPass"));
        }

        [Test]
        public void TestUserAuthorization()
        {
            // Assuming user ID 1 is assigned the "admin" role
            bool isAdmin = Authorization.UserHasRole(1, "admin", _connectionString);
            Assert.IsTrue(isAdmin);

            bool isUser = Authorization.UserHasRole(1, "user", _connectionString);
            Assert.IsFalse(isUser); // if the user is only admin
        }
    }
}