using System.Security.Cryptography;
using System.Text;

namespace PassSaver
{
    public class PasswordHasher
    {
        public string Hash(string originalPassword)
        {
            var hashAlgorithm = SHA512.Create();
            return Convert.ToBase64String(hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(originalPassword)));
        }
    }
}
