using System;
using System.Security.Cryptography;
using System.Text;

namespace Common.Commons
{
    public class HashString
    {
        public static string StringToHash(string input, string hashtype)
        {
            Byte[] clearBytes;
            Byte[] hashedBytes;
            string output = String.Empty;

            switch (hashtype)
            {
                case "SHA256":
                    clearBytes = Encoding.UTF8.GetBytes(input);
                    SHA256 sha256 = new SHA256Managed();
                    sha256.ComputeHash(clearBytes);
                    hashedBytes = sha256.Hash;
                    sha256.Clear();
                    output = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                    break;
                case "SHA384":
                    clearBytes = Encoding.UTF8.GetBytes(input);
                    SHA384 sha384 = new SHA384Managed();
                    sha384.ComputeHash(clearBytes);
                    hashedBytes = sha384.Hash;
                    sha384.Clear();
                    output = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                    break;
                case "SHA512":
                    clearBytes = Encoding.UTF8.GetBytes(input);
                    SHA512 sha512 = new SHA512Managed();
                    sha512.ComputeHash(clearBytes);
                    hashedBytes = sha512.Hash;
                    sha512.Clear();
                    output = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                    break;
            }
            return output;
        }
    }
}
