using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace TeamCores.Common.Utilities
{
    public static class Encrypt
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static string MD5Encrypt(this string txt)
        {
            MD5 md5 = MD5.Create();
            return BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(txt))).Replace("-", "");
        }

        /// <summary>
        /// Sha1加密
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static string SHA1Encrypt(this string txt)
        {
            byte[] StrRes = Encoding.UTF8.GetBytes(txt);
            HashAlgorithm iSHA = SHA1.Create();
            StrRes = iSHA.ComputeHash(StrRes);
            StringBuilder EnText = new StringBuilder();
            foreach (byte iByte in StrRes)
            {
                EnText.AppendFormat("{0:x2}", iByte);
            }
            return EnText.ToString();
        }
    }
}
