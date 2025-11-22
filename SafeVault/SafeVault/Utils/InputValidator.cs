namespace SafeVault.Utils;

using System.Text.RegularExpressions;

public static class InputValidator
{
    // Validate username: allow only letters, numbers, underscores, 3-20 characters
    public static string SanitizeUsername(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("Username cannot be empty");

        var sanitized = Regex.Replace(username, @"[^\w]", "");
        if (sanitized.Length < 3 || sanitized.Length > 20)
            throw new ArgumentException("Username must be between 3 and 20 characters");
        
        return sanitized;
    }

    // Validate email using regex
    public static string SanitizeEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty");

        var pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        if (!Regex.IsMatch(email, pattern))
            throw new ArgumentException("Invalid email format");

        return email;
    }
}
