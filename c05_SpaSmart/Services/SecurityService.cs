using System.Security.Cryptography;
using System.Text;
using BCrypt.Net;

namespace c05_SpaSmart.Services
{
    public class SecurityService
    {
        // Mã hóa AES
        private readonly string _encryptionKey = "SpaSmartSecretKey_2026_DotNet8_XX"; 

        // Hash mật khẩu 1 chiều Bcrypt
        public string HashPassword(string plainPassword)
        {
            return BCrypt.Net.BCrypt.HashPassword(plainPassword);
        }

        // Check mật khẩu so với Hash
        public bool VerifyPassword(string plainPassword, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(plainPassword, hashedPassword);
        }
    }
}
