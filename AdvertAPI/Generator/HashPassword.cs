using System;
using System.Security.Cryptography;
using System.Text;


namespace AdvertAPI.Generator
{
    public class HashPassword
    {
        public static string HashPass(string password, string salt)
        {
            var sha = SHA256.Create();
            var saltedPassword = password + salt;
            return Convert.ToBase64String(sha.ComputeHash(Encoding.Unicode.GetBytes(saltedPassword)));
        }
    }
}
