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
    }
}