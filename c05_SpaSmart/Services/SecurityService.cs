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

        // Mã hóa 2 chiều (Encrypt) -> AES
        public string EncryptData(string plainText)
        {
            if (string.IsNullOrEmpty(plainText)) 
                return plainText;

            byte[] iv = new byte[16]; 
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(_encryptionKey);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        // Giải mã dữ liệu Decrypt
        public string DecryptData(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText)) 
                return cipherText;

            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(_encryptionKey);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
