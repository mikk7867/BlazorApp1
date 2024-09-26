using BCrypt.Net;
using System.Security.Cryptography;
using System.Text;

namespace BlazorApp1.Codes
{
    public class HashingHandler
    {
        public string MD5Hashing(string textToHash)
        {
            byte[] inputByte = Encoding.ASCII.GetBytes(textToHash);
            MD5 md5 = MD5.Create();
            byte[] hashedValue = md5.ComputeHash(inputByte);

            return Convert.ToBase64String(hashedValue);
        }

        public string SHA256Hashing(string textToHash)
        {
            byte[] inputByte = Encoding.ASCII.GetBytes(textToHash);
            SHA256 sha256 = SHA256.Create();
            byte[] hashedValue = sha256.ComputeHash(inputByte);

            return Convert.ToBase64String(hashedValue);
        }

        public string HMACHashing(string textToHash)
        {
            byte[] myKey = Encoding.ASCII.GetBytes("NielsErMinFavoritLære");
            byte[] inputByte = Encoding.ASCII.GetBytes(textToHash);

            HMACSHA256 hmacSHA256 = new HMACSHA256();
            hmacSHA256.Key = myKey;
            byte[] hashedValue = hmacSHA256.ComputeHash(inputByte);

            return Convert.ToBase64String(hashedValue);
        }

        public string PBKDF2Hashing(string textToHash)
        {
            byte[] inputByte = Encoding.ASCII.GetBytes(textToHash);
            byte[] saltAsByteArray = Encoding.ASCII.GetBytes("mySalt");
            var hashAlgo = new HashAlgorithmName("SHA256");
            byte[] hashedValue = Rfc2898DeriveBytes.Pbkdf2(inputByte, saltAsByteArray, 11, hashAlgo, 32);

            return Convert.ToBase64String(hashedValue);
        }

        /// <summary>
        /// Kræver NuGet pakke : BCrypt.NET-NEXT
        /// </summary>
        /// <param name="textToHash"></param>
        /// <returns></returns>
        public string BCryptHashing(string textToHash)
        {
            //return BCrypt.Net.BCrypt.HashPassword(textToHash);

            //int workFactor = 11;
            //string salt = BCrypt.Net.BCrypt.GenerateSalt();
            //bool enhancedEntropy = true;
            //return BCrypt.Net.BCrypt.HashPassword(textToHash, salt, enhancedEntropy);

            int workFactor = 11;
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            bool enhancedEntropy = true;
            HashType hashType = HashType.SHA256;
            return BCrypt.Net.BCrypt.HashPassword(textToHash, salt, enhancedEntropy, hashType);
        }

        public bool BCryptVerify(string textToHash, string hashedValueFromDb)
        {
            //return BCrypt.Net.BCrypt.Verify(textToHash, hashedValueFromDb);

            //return BCrypt.Net.BCrypt.Verify(textToHash, hashedValueFromDb, true);

            return BCrypt.Net.BCrypt.Verify(textToHash, hashedValueFromDb, true, BCrypt.Net.HashType.SHA256);
        }
    }
}
