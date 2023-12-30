using System.Security.Cryptography;
using System.Text;

namespace SOWEDANE.Utils
{
    public class PasswordHasher
    {
        public static string HashPassword(string salt, string password)
        {
          
            using (var deriveBytes = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes(salt), 10000))
            {
                byte[] hash = deriveBytes.GetBytes(32); // 32 bytes for a 256-bit key
                return Convert.ToBase64String(hash);
            }
        }

        public static string GenerateSalt()
        {
            byte[] salt = new byte[16]; // You can adjust the size of the salt
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }
    }
}
