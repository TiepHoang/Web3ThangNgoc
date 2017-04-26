using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Web3ThangNgoc.Models
{
    public class iMD5
    {
        public static string GetMD5Hash(string inputData)
        {
            //MD5 md5 = MD5.Create();
            //byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(inputData));
            //var stringBuider = new StringBuilder();
            //for (int i = 0; i < data.Length; i++)
            //{
            //    stringBuider.Append(data[i].ToString("2x"));
            //}
            //return stringBuider.ToString();
            return BitConverter.ToString(encryptData(inputData));
        }

        public static bool Vietify(string input, string hash)
        {
            StringComparer companer = StringComparer.OrdinalIgnoreCase;
            return companer.Compare(GetMD5Hash(input), hash) == 0;
        }

        public static byte[] encryptData(string data)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] hashedBytes;
            UTF8Encoding encoder = new UTF8Encoding();
            hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(data));
            return hashedBytes;
        }
    }
}