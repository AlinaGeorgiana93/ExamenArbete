using System;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Utils
{
    public static class InputValidationUtils
    {
        // Normalizes names (capitalize first letter, rest lowercase)
        public static string NormalizeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return name;

            name = name.Trim().ToLower();
            return char.ToUpper(name[0]) + name.Substring(1);
        }

        // Validate names (no numbers or special characters, at least 2 characters)
        public static bool IsValidName(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length < 2)
                return false;

            return Regex.IsMatch(name, @"^[A-Za-zÀ-ÖØ-öø-ÿ]+$"); // Supports international characters
        }

        // Validate username (at least 4 characters, can contain letters, digits, underscores)
        public static bool IsValidUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username) || username.Length < 4)
                return false;

            return Regex.IsMatch(username, @"^[a-zA-Z0-9_]+$");
        }

        // Validate email using .NET built-in MailAddress
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;

            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // Validate password: minimum 6 characters, at least one uppercase letter and one special character
         public static bool IsStrongPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password) || password.Length < 6) return false;
        return password.Any(char.IsUpper) &&
               password.Any(char.IsLower) &&
               password.Any(char.IsDigit) &&
               password.Any(c => !char.IsLetterOrDigit(c));
    }
    }
}
