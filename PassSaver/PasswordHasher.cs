using System.Security.Cryptography;
using System.Text;

namespace PassSaver
{
    public interface IPasswordHasher
    {
        string Hash(string originalPassword);
    }
    public class PasswordHasher :  IPasswordHasher
    {
        public string Hash(string originalPassword)
        {
            var hashAlgorithm = SHA512.Create();
            return Convert.ToBase64String(hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(originalPassword)));
        }
    }
}
