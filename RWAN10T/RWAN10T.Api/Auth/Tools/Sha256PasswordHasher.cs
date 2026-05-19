using RWAN10T.Api.Auth.Contract;
using System.Security.Cryptography;
using System.Text;

namespace RWAN10T.Api.Auth.Tools
{
    public class Sha256PasswordHasher : IPasswordHasher
    {
        public string Hash(string password)
        {
            var inputBytes = Encoding.UTF8.GetBytes(password);
            var hashBytes = SHA256.HashData(inputBytes);

            var builder = new StringBuilder();
            foreach (var b in hashBytes)
            {
                builder.Append(b.ToString("x2"));
            }

            return builder.ToString();
        }

        public bool Verify(string password, string hashedPassword)
        {
            return Hash(password) == hashedPassword;
        }
    }
}
