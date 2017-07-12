using System;
using System.Text;

namespace TeamCores.Common.Utilities
{
    public static class Strings
    {
        /// <summary>
        /// 密码加密，三次加密，加密顺序：MD5，SHA1，MD5
        /// </summary>
        /// <param name="text">需要加密的密码</param>
        /// <returns></returns>
        public static string PasswordEncrypt(this string text)
        {
            var md5 = text.MD5Encrypt();
            var sha = md5.SHA1Encrypt();
            return sha.MD5Encrypt();
        }

        /// <summary>
        /// 将全角数字转换为数字
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string SBCCaseToNumberic(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }
            char[] c = text.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                byte[] b = Encoding.Unicode.GetBytes(c, i, 1);
                if (b.Length == 2)
                {
                    if (b[1] == 255)
                    {
                        b[0] = (byte)(b[0] + 32);
                        b[1] = 0;
                        c[i] = Encoding.Unicode.GetChars(b)[0];
                    }
                }
            }
            return new string(c);
        }

        /// <summary>
        /// 建立密文
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string CreateToken(this string text)
        {
            return string.Format("token{0}@windx#lite", text).MD5Encrypt();
        }

        /// <summary>
        /// 以UTF8编码获取内容长度
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static int GetUTF8ByteLen(string text)
        {
            return Encoding.UTF8.GetByteCount(text);
        }

        /// <summary>   
        /// 计算文本长度，区分中英文字符，中文算两个长度，英文算一个长度
        /// </summary>
        /// <param name="Text">需计算长度的字符串</param>
        /// <returns>int</returns>
        public static int TextLength(string Text)
        {
            int len = 0;
            if (string.IsNullOrEmpty(Text))
            {
                return len;
            }

            for (int i = 0; i < Text.Length; i++)
            {
                byte[] bytes = Encoding.UTF8.GetBytes(Text.Substring(i, 1));
                if (bytes.Length > 1)
                    len += 2;  //如果长度大于1，是中文，占两个字节，+2
                else
                    len += 1;  //如果长度等于1，是英文，占一个字节，+1
            }

            return len;
        }

        /// <summary>
        /// 字符串null 自动转换成空
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string NullAsEmpty(string text)
        {
            return text ?? string.Empty;
        }

        /// <summary>
        /// 对象null自动转换成空字符串
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string NullAsEmpty(object text)
        {
            return object.ReferenceEquals(text, null) ? string.Empty : (string)text;
        }

        /// <summary>
        /// 获取URL字符串中得域名信息,如果URL字符串不符合规定，返回空值
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetUrlHost(string url)
        {
            try
            {
                return new Uri(url).Host;
            }
            catch { return string.Empty; }
        }

        /// <summary>
        /// 将数字字符串转换为数字
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static int ConvertToInt32(this string text)
        {
            decimal i;
            if (!decimal.TryParse(text, out i))
            {
                i = 0;
            }
            return (int)i;
        }
    }
}
