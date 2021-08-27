using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Cinema
{
    class Cod
    {
        public static string EncodeString(string originalString)
        {
            String codedstring = "";

            string hash = "f0xle@rn";

            byte[] data = UTF8Encoding.UTF8.GetBytes(originalString);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateEncryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    codedstring = Convert.ToBase64String(results, 0, results.Length);
                }
            }

            return codedstring;
        }

        public static string DecodeString(string codedlstring)
        {
            string decodedstring = "";
            string hash = "f0xle@rn";

            byte[] data1 = Convert.FromBase64String(codedlstring);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateDecryptor();
                    byte[] results = transform.TransformFinalBlock(data1, 0, data1.Length);
                    decodedstring = UTF8Encoding.UTF8.GetString(results);
                }
            }

            return decodedstring;
        }
    }
}
