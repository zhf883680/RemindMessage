using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace WebApiTpl.Core.Extensions
{
    public class SecurityExtensions
    {
        
        /// <summary>
        ///     md5加密
        /// </summary>
        /// <param name="sValue"></param>
        /// <returns></returns>
        public static string StringToMd5String(string sValue)
        {
            var retValue = "";
            var oMD5 = new MD5CryptoServiceProvider();

            var returnByte = oMD5.ComputeHash(Encoding.Unicode.GetBytes(sValue));

            for (var i = 0; i < returnByte.Length; i++) retValue += returnByte[i].ToString("X2");

            oMD5 = null;

            return retValue.ToUpper();
        }

        /// <summary>
        ///     utf8的md5加密
        /// </summary>
        /// <param name="sValue"></param>
        /// <returns></returns>
        public static string StringToMd5StringByUTF8(string sValue)
        {
            var retValue = "";
            var oMD5 = new MD5CryptoServiceProvider();

            var returnByte = oMD5.ComputeHash(Encoding.UTF8.GetBytes(sValue));

            for (var i = 0; i < returnByte.Length; i++) retValue += returnByte[i].ToString("X2");

            oMD5 = null;

            return retValue.ToUpper();
        }

        /// <summary>
        ///     全局卸载密码SHA加密
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static string StringToSHA(string strValue)
        {
            var retValue = "";
            byte[] tmpByte;
            //SHA512 sha1 = new SHA512CryptoServiceProvider();
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            tmpByte = sha1.ComputeHash(Encoding.Unicode.GetBytes(strValue));
            for (var i = 0; i < tmpByte.Length; i++) retValue += tmpByte[i].ToString("X2");
            sha1.Clear();
            return retValue.ToUpper();
        }
         /// <summary>
        /// aes加密
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static object AESEncryption(string encryptStr, string key="aesKey")
        {
            try
            {
                //byte[] keyArray = Encoding.UTF8.GetBytes(Key);
                byte[] keyArray = Convert.FromBase64String(key);
                byte[] toEncryptArray = Encoding.UTF8.GetBytes(encryptStr);

                RijndaelManaged rDel = new RijndaelManaged();
                rDel.Key = keyArray;
                rDel.Mode = CipherMode.ECB;
                rDel.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = rDel.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// AES 算法解密(ECB模式) 将密文base64解码进行解密，返回明文
        /// </summary>
        /// <param name="decryptStr">密文</param>
        /// <param name="key">密钥</param>
        /// <returns>明文</returns>
        public static string AesDecrypt(string decryptStr, string key= "aesKey")
        {
            try
            {
                //byte[] keyArray = Encoding.UTF8.GetBytes(Key);
                byte[] keyArray = Convert.FromBase64String(key);
                byte[] toEncryptArray = Convert.FromBase64String(decryptStr);

                RijndaelManaged rDel = new RijndaelManaged();
                rDel.Key = keyArray;
                rDel.Mode = CipherMode.ECB;
                rDel.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = rDel.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                return Encoding.UTF8.GetString(resultArray);//  UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// 获取到文件的hash值 
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static string FileToMD5(string filepath)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider oMD5 =
                new System.Security.Cryptography.MD5CryptoServiceProvider();
            try
            {
                var stream = new FileStream(filepath, FileMode.Open);
                byte[] hashByte = oMD5.ComputeHash(stream);
                string retValue = "";
                for (int i = 0; i < hashByte.Length; i++)
                {
                    retValue += hashByte[i].ToString("X2");
                }
                stream.Close();
                oMD5 = null;
                return retValue.ToUpper();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }

            return null;
        }
    }
}