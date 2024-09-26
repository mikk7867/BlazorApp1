using Newtonsoft.Json;
using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace BlazorApp1.Codes
{

    public class AsymetricEncrypter
    {
        private string _privateKey;
        private string _publicKey;
        private readonly HttpClient _httpClient;

        public AsymetricEncrypter(HttpClient httpClient)
        {
            _httpClient = httpClient;
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                if(File.Exists("privateKey.txt") && File.Exists("publicKey.txt"))
                {
                    _privateKey = File.ReadAllText("privateKey.txt");
                    _publicKey = File.ReadAllText("publicKey.txt");
                }
                else
                {
                    _privateKey = rsa.ToXmlString(true);
                    _publicKey = rsa.ToXmlString(false);

                    File.WriteAllText("privateKey.txt", _privateKey);
                    File.WriteAllText("publicKey.txt", _publicKey);
                }
            }
        }

        public async Task<string> AsymetricEncrypt(string toEncrypt)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(_publicKey);

                byte[] toEncryptB = System.Text.Encoding.UTF8.GetBytes(toEncrypt);
                byte[] encrypted = rsa.Encrypt(toEncryptB, true);
                var encryptedDataString = Convert.ToBase64String(encrypted);

                return encryptedDataString;
            }
        }

        //public async Task<string> AsymetricEncrypt(string toEncrypt)
        //{
        //    string[] param = new string[] { toEncrypt, _publicKey };
        //    string serialized = JsonConvert.SerializeObject(param);
        //    StringContent sc = new StringContent(serialized);

        //    var encrypted = await _httpClient.PostAsync("https://localhost:7243/api/Encryptor", sc);

        //    string final = encrypted.Content.ReadAsStringAsync().Result;
        //    return final;
        //}

        public string AsymetricDecrypt(string toDecrypt)
        {
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                RSA.FromXmlString(_privateKey);

                byte[] toDecryptArray = Convert.FromBase64String(toDecrypt);
                byte[] decrypted = RSA.Decrypt(toDecryptArray, true);

                string final = Encoding.UTF8.GetString(decrypted);
                return final;
            }
        }
    }
}
