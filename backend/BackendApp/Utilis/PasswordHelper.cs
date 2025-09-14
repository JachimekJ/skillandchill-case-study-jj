using System;

namespace BackendApp.Utils
{
    public static class PasswordHelper
    {
        public static void GenerateHash()
        {
            string password = "admin123";
            string hash = BCrypt.Net.BCrypt.HashPassword(password);
            Console.WriteLine($"Hash dla admin123: {hash}");
        }
    }
}
