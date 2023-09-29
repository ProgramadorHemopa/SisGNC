using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace HMP.DataObjects.SisRNCWeb
{
    public static class Cryptography
    {
        public static string EncryptString(string pData, string pKey)
        {
            byte[] b = Encoding.UTF8.GetBytes(pData);
            byte[] pw = Encoding.UTF8.GetBytes(pKey);

            RijndaelManaged rm = new RijndaelManaged();

            PasswordDeriveBytes pdb = new PasswordDeriveBytes(pKey, new MD5CryptoServiceProvider().ComputeHash(pw));
            rm.Key = pdb.GetBytes(32);
            rm.IV = pdb.GetBytes(16);
            rm.BlockSize = 128;
            rm.Padding = PaddingMode.PKCS7;

            MemoryStream ms = new MemoryStream();

            CryptoStream cryptStream = new CryptoStream(ms, rm.CreateEncryptor(rm.Key, rm.IV), CryptoStreamMode.Write);
            cryptStream.Write(b, 0, b.Length);
            cryptStream.FlushFinalBlock();

            return System.Convert.ToBase64String(ms.ToArray());
        }

        public static string DecryptString(byte[] pData, string pKey)
        {
            byte[] pw = Encoding.UTF8.GetBytes(pKey);

            RijndaelManaged rm = new RijndaelManaged();
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(pKey, new MD5CryptoServiceProvider().ComputeHash(pw));
            rm.Key = pdb.GetBytes(32);
            rm.IV = pdb.GetBytes(16);
            rm.BlockSize = 128;
            rm.Padding = PaddingMode.PKCS7;

            MemoryStream ms = new MemoryStream(pData, 0, pData.Length);

            CryptoStream cryptStream = new CryptoStream(ms, rm.CreateDecryptor(rm.Key, rm.IV), CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cryptStream);

            return sr.ReadToEnd();
        }
    }
}

