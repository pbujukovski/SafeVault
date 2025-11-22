// Utils/Authentication.cs
using BCrypt.Net;

namespace SafeVault.Utils
{
    public static class Authentication
    {
        // Hash a plain text password
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        // Verify a password against the stored hash
        public static bool VerifyPassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}