using System;
using System.Text.RegularExpressions;
using System.Net;

namespace SafeVault.Utils
{
    public static class InputValidator
    {
        public static string SanitizeUsername(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Username cannot be empty");
            // Remove any HTML tags
            return Regex.Replace(input, "<.*?>", string.Empty);
        }

        public static string SanitizeEmail(string input)
        {
            if (!Regex.IsMatch(input, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new ArgumentException("Invalid email format");
            return input;
        }

        public static string EncodeForHtml(string input)
        {
            return WebUtility.HtmlEncode(input);
        }
    }
}