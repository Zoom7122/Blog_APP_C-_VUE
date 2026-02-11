using BlogAPP_BLL.Intarface;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BlogAPP_BLL.Models
{
    public class PasswordService : IPasswordService
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 200_000;

        public string HashPassword(string password)
        {
            var salt = new byte[SaltSize];
            RandomNumberGenerator.Fill(salt);

            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            var hash = pbkdf2.GetBytes(HashSize);

            // Сохраняем в формате: iterations.saltBase64.hashBase64
            return $"{Iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
        }

        public bool VerifyPassword(string password, string storedHash)
        {
            if (string.IsNullOrWhiteSpace(storedHash))
                return false;

            var parts = storedHash.Split('.', 3);
            if (parts.Length != 3) return false;

            if (!int.TryParse(parts[0], out var iterations)) return false;
            var salt = Convert.FromBase64String(parts[1]);
            var expectedHash = Convert.FromBase64String(parts[2]);

            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
            var computedHash = pbkdf2.GetBytes(expectedHash.Length);

            return CryptographicOperations.FixedTimeEquals(computedHash, expectedHash);
        }
    }
}
