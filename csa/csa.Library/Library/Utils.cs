using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

//using DeviceDetectorNET;

namespace csa.Library
{
    public class Utils
    {
        public class Utilities
        {
            static Regex MOBI_CHK = new Regex(@"(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows ce|xda|xiino", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
            static Regex MOBI_VER_CHK = new Regex(@"1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);

            /* get IP address */
            public static string GetIPAddress()
            {
                String ipAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                if (string.IsNullOrEmpty(ipAddress))
                { ipAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]; }
                else
                { ipAddress = ipAddress.Split(',')[0]; }

                return ipAddress;
            }

            /* get request browser */
            public static string GetRequestBrowser()
            {
                string retVal = string.Empty;

                HttpBrowserCapabilities httpBrowser = null;
                string userAgent = string.Empty;

                try
                {
                    //get `httpBrowser`
                    httpBrowser = HttpContext.Current.Request.Browser;

                    //get `httpUserAgent`
                    userAgent = HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"];
                }
                catch
                { }
                
                if (httpBrowser.IsMobileDevice)
                {
                    if (userAgent.Length < 4)
                    { retVal = httpBrowser.Browser; }

                    //bool mobiChk = MobileCheck.IsMatch(userAgent);
                    //bool mobiVerChk = MobileVersionCheck.IsMatch(userAgent.Substring(0, 4));

                    Match mobiChk = MOBI_CHK.Match(userAgent);
                    Match mobiVerChk = MOBI_VER_CHK.Match(userAgent);

                    if (mobiChk.Success)
                    { retVal = mobiChk.Value; }

                    if (mobiVerChk.Success)
                    { retVal = (string.IsNullOrWhiteSpace(retVal) ? (mobiVerChk.Value) : (string.Format("{0} {1}", retVal, mobiVerChk.Value))); }
                }
                else
                { retVal = httpBrowser.Browser; }

                return retVal;
            }

            /*
            public static string GetByDeviceDetector()
            {
                string retVal = string.Empty;

                string userAgent = string.Empty;

                try
                {
                    //get `httpUserAgent`
                    userAgent = HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"];

                    //NameValueCollection header = HttpContext.Current.Request.Headers; 
                }
                catch
                { }

                var dd = new DeviceDetector(userAgent);
                //var clientHints = ClientHints.Factory(headers);

                var device = dd.GetDeviceName();
                
                //if (device == "desktop")
                //{
                //    retVal = dd.GetBrowserClient().ParserName;
                //}
                //else
                //{
                //    var os = dd.GetOs().ParserName;
                //    var browser = dd.GetBrowserClient().ParserName;

                //    if (!string.IsNullOrEmpty(os))
                //    { retVal = retVal + $"{os} "; }

                //    if (!string.IsNullOrEmpty(browser))
                //    { retVal = retVal + $"{browser} "; }

                //    retVal = retVal.Trim();
                //}
                
                return retVal;
            }
            */

            public static long GenTimeStamp(DateTime baseDateTime)
            {
                var dtOffset = new DateTimeOffset(baseDateTime);
                return dtOffset.ToUnixTimeMilliseconds();
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

            public static string GetFileSize(double fileSize)
            {
                string retVal = "";

                if (fileSize < 1000)
                { retVal = $"{fileSize} B"; }
                else if (Math.Round((fileSize / 1024D)) < 1000)
                { retVal = $"{Math.Round(fileSize / 1024D)} KB"; }
                else if (Math.Round((fileSize / (1024D * 1024D))) < 1000)
                { retVal = $"{Math.Round((fileSize / (1024D * 1024D)))} MB"; }
                else
                { retVal = $"{Math.Round((fileSize / (1024D * 1024D * 1024D)))} GB"; }

                return retVal;
            }
            public static string NumberToWords(double doubleNumber)
            {
                var beforeFloatingPoint = (int)Math.Floor(doubleNumber);
                var beforeFloatingPointWord = $"{NumberToWords(beforeFloatingPoint)} ringgit";
                var afterFloatingPointWord =
                    $"{SmallNumberToWord((int)((Convert.ToDecimal(doubleNumber) - beforeFloatingPoint) * 100), "")} cents";
                return $"{beforeFloatingPointWord} and {afterFloatingPointWord}";
            }

            private static string NumberToWords(int number)
            {
                if (number == 0)
                    return "zero";

                if (number < 0)
                    return "minus " + NumberToWords(Math.Abs(number));

                var words = "";

                if (number / 1000000000 > 0)
                {

                    words += NumberToWords(number / 1000000000) + " billion ";
                    number %= 1000000000;
                }

                if (number / 1000000 > 0)
                {
                    words += NumberToWords(number / 1000000) + " million ";
                    number %= 1000000;
                }

                if (number / 1000 > 0)
                {
                    words += NumberToWords(number / 1000) + " thousand ";
                    number %= 1000;
                }

                if (number / 100 > 0)
                {
                    words += NumberToWords(number / 100) + " hundred ";
                    number %= 100;
                }

                words = SmallNumberToWord(number, words);

                return words;
            }

            private static string SmallNumberToWord(int number, string words)
            {
                if (number <= 0) return words;
                if (words != "")
                    words += " ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
                return words;
            }
            

            public static string GenSignature(string apiSecret, string message)
            {
                var key = Encoding.UTF8.GetBytes(apiSecret);
                string stringHash;

                using (var hmac = new HMACSHA256(key))
                {
                    var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
                    var hex = new StringBuilder(hash.Length * 2);

                    foreach (var item in hash)
                    { hex.AppendFormat("{0:x2}", item); }

                    stringHash = hex.ToString();
                }

                return stringHash;
            }
        }

        public class Validation
        {
            public static bool IsUserName(string Input)
            {
                //no space allowed
                Regex regex = new Regex(@"^\S*$");
                Match match = regex.Match(Input);

                if (match.Success)
                { return true; }
                else
                { return false; }
            }

            public static bool IsPassword(string Input)
            {
                /*
                Minimum eight characters, at least one letter and one number:- "^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$"
                Minimum eight characters, at least one letter, one number and one special character:- "^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$"
                Minimum eight characters, at least one uppercase letter, one lowercase letter and one number:- "^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$"
                Minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character:- "^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$"
                Minimum eight and maximum 10 characters, at least one uppercase letter, one lowercase letter, one number and one special character:- "^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,10}$"
                */

                /*
                Minimum four characters, which MUST start with alpha, NO space allowed - ^[a-zA-Z][a-zA-Z0-9!@#$%^&*_?.\\\/-]{3,}$
                Minimum four characters, allow alpha-numeric and certain symbols, NO space allowed - ^[a-zA-Z0-9!@#$%^&*_?.\\\/-]{3,}$
                */

                //no space allow
                Regex regex = new Regex(@"^\S*$");
                Match match = regex.Match(Input);

                if (match.Success)
                { return true; }
                else
                { return false; }
            }

            public static bool IsEmail(string Input)
            {
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(Input);

                if (match.Success)
                { return true; }
                else
                { return false; }
            }

            public static bool IsPhoneNo(string Input)
            {
                //space allowed, has extension with # :- ^\(?\+?[0-9]{1,3}\)? ?-?[0-9]{1,3} ?-?[0-9]{3,5} ?-?[0-9]{4}( ?-?[0-9]{3})?(?:\s*(?:#)\s*(\d+))?$
                //space allowed, no extension :- ^\(?\+?[0-9]{1,3}\)? ?-?[0-9]{1,3} ?-?[0-9]{3,5} ?-?[0-9]{4}( ?-?[0-9]{3})?$

                Regex regex = new Regex(@"^\(?\+?[0-9]{1,3}\)? ?-?[0-9]{1,3} ?-?[0-9]{3,5} ?-?[0-9]{4}( ?-?[0-9]{3})?$");
                Match match = regex.Match(Input);

                if (match.Success)
                { return true; }
                else
                { return false; }
            }

            public static bool IsMYICNumber(string Input)
            {
                //no space allowed
                Regex regex = new Regex(@"^(([[0-9]{2})(0[1-9]|1[0-2])(0[1-9]|[12][0-9]|3[01]))-?([0-9]{2})-?([0-9]{4})$");
                Match match = regex.Match(Input);

                if (match.Success)
                { return true; }
                else
                { return false; }
            }
        }

        /* Encode Helper */
        public class EncodeHelper
        {
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
        }

        public class IntegerEncodeHelper
        {
            public static long Base36toInt(string s)
            {
                char[] baseChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
                char[] target = s.ToCharArray();
                double result = 0;

                for (int i = 0; i < target.Length; i++)
                {
                    result += Array.IndexOf(baseChars, target[i]) * Math.Pow(baseChars.Length, target.Length - i - 1);
                }

                return Convert.ToInt64(result);
            }

            public static string IntTo36Base(long value)
            {
                char[] baseChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
                string result = string.Empty;
                int targetBase = baseChars.Length;

                do
                {
                    result = baseChars[value % targetBase] + result;
                    value = value / targetBase;
                }
                while (value > 0);

                return result;
            }
        }

        /* Cryptography Helper */
        public class CryptoHelper
        {
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
        }

        /* Hashing Helper */
        public class HashHelper
        {
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

            //public static bool VerifyFileStreamMD5Hash(FileStream InputData, string Hashing)
            //{       
            //    return StringComparer.OrdinalIgnoreCase.Compare(MD5FileStreamHash(InputData), Hashing) == 0;
            //}
        }

        /* Enum Helper */
        public class EnumHelper
        {
            public static string GetDescription(Enum enumType)
            {
                string description = string.Empty;
                System.ComponentModel.DescriptionAttribute descAttr;

                System.Reflection.FieldInfo fi = enumType.GetType().GetField(enumType.ToString());
                descAttr = (System.ComponentModel.DescriptionAttribute)Attribute.GetCustomAttribute(fi, typeof(System.ComponentModel.DescriptionAttribute));

                if (descAttr != null)
                { description = descAttr.Description; }
                else
                { description = enumType.ToString(); }

                return description;
            }

            public static T GetEnumForDescription<T>(string description)
            {
                object returnValue = Activator.CreateInstance(typeof(T));
                Array enumArray = Enum.GetValues(typeof(T));

                foreach (object obj in enumArray)
                {
                    string text = GetDescription((Enum)obj);

                    if (text == description)
                    { returnValue = (T)obj; }
                }

                return (T)returnValue;
            }

            public static T EnumStringContains<T>(string enumString)
            {
                T retVal = default(T);

                try
                {
                    Array enumAry = Enum.GetValues(typeof(T));

                    foreach (object obj in enumAry)
                    {
                        if (obj.ToString().ToLower().StartsWith(enumString.ToLower()))
                        { retVal = (T)obj; break; }
                    }

                }
                catch { }

                return retVal;
            }
        }

        /* Conversion Helper */
        public class ConversionHelper
        {
            /* DateTime */
            public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
            {
                // Unix timestamp is seconds past epoch
                System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
                return dtDateTime;
            }

            public static DateTime JavaTimeStampToDateTime(double javaTimeStamp)
            {
                // Java timestamp is milliseconds past epoch
                System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                dtDateTime = dtDateTime.AddMilliseconds(javaTimeStamp).ToLocalTime();
                return dtDateTime;
            }

            /* char byte decimal conversion */
            public static decimal ToDecimal(byte[] bytes)
            {
                int[] bits = new int[4];
                bits[0] = ((bytes[0] | (bytes[1] << 8)) | (bytes[2] << 0x10)) | (bytes[3] << 0x18); //lo
                bits[1] = ((bytes[4] | (bytes[5] << 8)) | (bytes[6] << 0x10)) | (bytes[7] << 0x18); //mid
                bits[2] = ((bytes[8] | (bytes[9] << 8)) | (bytes[10] << 0x10)) | (bytes[11] << 0x18); //hi
                bits[3] = ((bytes[12] | (bytes[13] << 8)) | (bytes[14] << 0x10)) | (bytes[15] << 0x18); //flags

                return new decimal(bits);
            }

            public static byte[] GetBytes(decimal d)
            {
                byte[] bytes = new byte[16];

                int[] bits = decimal.GetBits(d);
                int lo = bits[0];
                int mid = bits[1];
                int hi = bits[2];
                int flags = bits[3];

                bytes[0] = (byte)lo;
                bytes[1] = (byte)(lo >> 8);
                bytes[2] = (byte)(lo >> 0x10);
                bytes[3] = (byte)(lo >> 0x18);
                bytes[4] = (byte)mid;
                bytes[5] = (byte)(mid >> 8);
                bytes[6] = (byte)(mid >> 0x10);
                bytes[7] = (byte)(mid >> 0x18);
                bytes[8] = (byte)hi;
                bytes[9] = (byte)(hi >> 8);
                bytes[10] = (byte)(hi >> 0x10);
                bytes[11] = (byte)(hi >> 0x18);
                bytes[12] = (byte)flags;
                bytes[13] = (byte)(flags >> 8);
                bytes[14] = (byte)(flags >> 0x10);
                bytes[15] = (byte)(flags >> 0x18);

                return bytes;
            }
        }

        /* Logger */
        public static void FileLog(string LogPath, string LogText, string LogFileSuffix = "")
        {
            try
            {
                //correcting path
                if (LogPath.EndsWith(@"\")) { LogPath.Remove(LogPath.Length - 1, 1); }

                //set directory security
                System.Security.Principal.SecurityIdentifier everyone = new System.Security.Principal.SecurityIdentifier(System.Security.Principal.WellKnownSidType.WorldSid, null);
                System.Security.AccessControl.DirectorySecurity directorySecurity = new System.Security.AccessControl.DirectorySecurity();
                directorySecurity.AddAccessRule(new System.Security.AccessControl.FileSystemAccessRule(everyone, System.Security.AccessControl.FileSystemRights.FullControl, System.Security.AccessControl.AccessControlType.Allow));

                //verify path exists
                if (!Directory.Exists(LogPath)) { Directory.CreateDirectory(LogPath, directorySecurity); }

                DateTime curr = DateTime.UtcNow;

                string logFileName = string.IsNullOrWhiteSpace(LogFileSuffix) ? string.Format("{0}\\{1:yyyyMMdd}.log", LogPath, curr) : 
                    string.Format("{0}\\{1:yyyyMMdd}_{2}.log", LogPath, curr, LogFileSuffix);
                string logLineStr = string.Format("{0:HH:mm:ss}\t{1}", curr, LogText);

                using (StreamWriter sw = new StreamWriter(logFileName, true))
                {
                    sw.WriteLine(logLineStr);
                    sw.Flush();
                    sw.Close();
                }
            }
            catch { }
        }
    }
}