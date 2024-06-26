using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace API.Utils {
    public class HashingUtils {
        private const int SaltByte = 16;
        private const int HashByte = 32;
        private const int Iteration = 10000;
        private static HashAlgorithmName HashAlgorithm = HashAlgorithmName.SHA512;
        public static void Hash(string value, byte[] salt, out byte[] hash) {
            var pbkdf2 = new Rfc2898DeriveBytes(value, salt, Iteration, HashAlgorithm);
            hash = pbkdf2.GetBytes(HashByte);
        }
        public static void Hash(string value, string salt, out string hash) {
            byte[] saltBytes = Convert.FromBase64String(salt);
            Hash(value, saltBytes, out byte[] hashBytes);
            hash = Convert.ToBase64String(hashBytes);
        }
        public static void Hash(string value, out byte[] salt, out byte[] hash) {
            salt = RandomNumberGenerator.GetBytes(SaltByte);
            var pbkdf2 = new Rfc2898DeriveBytes(value, salt, Iteration, HashAlgorithm);
            hash = pbkdf2.GetBytes(HashByte);
        }

        public static void Hash(string value, out string salt, out string hash) {
            Hash(value, out byte[] saltBytes, out byte[] hashBytes);
            salt = Convert.ToBase64String(saltBytes);
            hash = Convert.ToBase64String(hashBytes);
        }

        public static void Hash(string value, out string hashValue) {
            Hash(value, out byte[] salt, out byte[] hash);
            byte[] hashBytes = new byte[SaltByte + HashByte];
            Array.Copy(salt, 0, hashBytes, 0, SaltByte);
            Array.Copy(hash, 0, hashBytes, SaltByte, HashByte);
            hashValue = Convert.ToBase64String(hashBytes);
        }
    }
}