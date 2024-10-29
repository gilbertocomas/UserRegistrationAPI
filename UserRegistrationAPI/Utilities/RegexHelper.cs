using System.Text.RegularExpressions;

namespace UserRegistrationAPI.Utilities
{
    public static class RegexHelper
    {
        public static string _emailPattern;
        public static string _passwordPattern;

        public static void Configure(IConfiguration configuration)
        {
            _emailPattern = configuration["RegexPatterns:EmailPattern"];
            _passwordPattern = configuration["RegexPatterns:PasswordPattern"];
        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(_emailPattern))
                throw new InvalidOperationException("El patrón de Email no está configurado.");

            return Regex.IsMatch(email, _emailPattern);
        }

        public static bool IsValidPassword(string password)
        {
            if (string.IsNullOrEmpty(_passwordPattern))
                throw new InvalidOperationException("El patrón de contraseña no está configurado.");

            return Regex.IsMatch(password, _passwordPattern);
        }
    }
}
