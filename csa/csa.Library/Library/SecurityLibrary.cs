using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace csa.Library
{
    public class SecurityLibrary
    {
        public static string SecretKey = "A7Q36vKo)+8l";

        public static string Base64Encode(string InputData)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(InputData);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string InputBase64Data)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(InputBase64Data);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static string GenRandomPwd(int length = 6)
        {
            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = "ABCDEFGHJKLMNPQRSTWXYabcdefghjkmnpqrstwxy0123456789@#$%^&*?";
            Random random = new Random();

            // Select one random character at a time from the string  
            // and create an array of chars  
            char[] chars = new char[length];

            for (int i = 0; i < length; i++)
            { chars[i] = validChars[random.Next(0, validChars.Length)]; }

            return new string(chars);
        }


        public static class SHA_256
        {
            public static string GenerateSHA256String(string inputString)
            {
                SHA256 SHA256 = SHA256Managed.Create();
                byte[] bytes = Encoding.UTF8.GetBytes(inputString);
                byte[] hash = SHA256.ComputeHash(bytes);
                return GetStringFromHash(hash);
            }

            private static string GetStringFromHash(byte[] hash)
            {
                StringBuilder result = new StringBuilder();

                for (int i = 0; i < hash.Length; i++)
                {
                    result.Append(hash[i].ToString("X2"));
                }
                return result.ToString().ToLower();
            }
        }

        public static string Encrypt(string StringToEncrypt)
        {
            byte[] inputByteArray = Encoding.UTF8.GetBytes(StringToEncrypt);
            byte[] rgbIV = { 0x21, 0x43, 0x56, 0x87, 0x10, 0xfd, 0xea, 0x1c };
            byte[] key = { };
            try
            {
                key = System.Text.Encoding.UTF8.GetBytes(Constant.CryptoKey);
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, rgbIV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string Decrypt(string EncryptedText)
        {
            byte[] inputByteArray = new byte[EncryptedText.Length + 1];
            byte[] rgbIV = { 0x21, 0x43, 0x56, 0x87, 0x10, 0xfd, 0xea, 0x1c };
            byte[] key = { };

            try
            {
                key = System.Text.Encoding.UTF8.GetBytes(Constant.CryptoKey);
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(EncryptedText.Replace(" ", "+"));
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, rgbIV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //================================================================================================

        public static string Encrypt2(string StringToEncrypt, string CryptoKey)
        {
            try
            {
                TripleDESCryptoServiceProvider objDESCrypto = new TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider objHashMD5 = new MD5CryptoServiceProvider();

                byte[] byteHash, byteBuff;

                byteHash = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(CryptoKey));

                objHashMD5 = null;
                objDESCrypto.Key = byteHash;
                objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB

                byteBuff = ASCIIEncoding.ASCII.GetBytes(StringToEncrypt);

                return Convert.ToBase64String(objDESCrypto.CreateEncryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length));
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string Decrypt2(string EncryptedText, string CryptoKey)
        {
            try
            {
                TripleDESCryptoServiceProvider objDESCrypto = new TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider objHashMD5 = new MD5CryptoServiceProvider();

                byte[] byteHash, byteBuff;

                byteHash = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(CryptoKey));

                objHashMD5 = null;
                objDESCrypto.Key = byteHash;
                objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB

                byteBuff = Convert.FromBase64String(EncryptedText);

                string strDecrypted = ASCIIEncoding.ASCII.GetString(objDESCrypto.CreateDecryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length));

                objDESCrypto = null;

                return strDecrypted;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string GenerateSalt(int Size = 128)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[Size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        public static string SHA1Hash(string InputString, bool UpperCase = false)
        {
            using (SHA1 sha1 = SHA1.Create())
            {
                StringBuilder sb = new StringBuilder();

                byte[] hash = sha1.ComputeHash(Encoding.GetEncoding(1252).GetBytes(InputString));

                if (UpperCase)
                {
                    foreach (byte item in hash)
                    { sb.AppendFormat("{0:X2}", item); }
                }
                else
                {
                    foreach (byte item in hash)
                    { sb.AppendFormat("{0:x2}", item); }
                }

                return sb.ToString();
            }
        }

        public static string SHA256Hash(string InputString, bool UpperCase = false)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                StringBuilder sb = new StringBuilder();

                byte[] hash = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(InputString));

                if (UpperCase)
                {
                    foreach (byte item in hash)
                    { sb.AppendFormat("{0:X2}", item); }
                }
                else
                {
                    foreach (byte item in hash)
                    { sb.AppendFormat("{0:x2}", item); }
                }

                return sb.ToString();
            }
        }

        public static string SHA512Hash(string InputString, bool UpperCase = false)
        {
            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] message = UE.GetBytes(InputString);

            SHA512Managed sha512 = new SHA512Managed();
            StringBuilder sb = new StringBuilder();

            byte[] hash = sha512.ComputeHash(message);

            if (UpperCase)
            {
                foreach (byte item in hash)
                { sb.AppendFormat("{0:X2}", item); }
            }
            else
            {
                foreach (byte item in hash)
                { sb.AppendFormat("{0:x2}", item); }
            }

            return sb.ToString();
        }

        public static string MD5Hash(string Input, bool UpperCase = false)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(Input));

                StringBuilder sb = new StringBuilder();

                if (UpperCase)
                {
                    foreach (byte item in hash)
                    { sb.AppendFormat("{0:X2}", item); }
                }
                else
                {
                    foreach (byte item in hash)
                    { sb.AppendFormat("{0:x2}", item); }
                }

                // Return the hexadecimal string.
                return sb.ToString();
            }
        }

        public static string MD5FileStreamHash(FileStream InputData)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(InputData);

                StringBuilder sb = new StringBuilder();

                foreach (byte item in hash)
                { sb.AppendFormat("{0:x2}", item); }

                // Return the hexadecimal string.
                return sb.ToString();
            }
        }

        public static string UrlParamEncrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI707741";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }


        public static string UrlParamDecrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI707741";
            cipherText = cipherText.Replace(" ", "+");
            try
            {
                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }
                        cipherText = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
            }
            catch (Exception)
            {

            }

            return cipherText;
        }
    }
}
