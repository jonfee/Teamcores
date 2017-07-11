using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TeamCores.Common.Utilities
{
    public static class Matching
    {
        /// <summary>
        /// 判断字符串是否是邮件
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsEmail(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return false;
            return Regex.IsMatch(text, @"^([0-9a-zA-Z]+[-._+&])*[0-9a-zA-Z]+@([-0-9a-zA-Z]+[.])+[a-zA-Z]{2,6}$");
        }

        /// <summary>
        /// 判断字符串是否是数字
        /// </summary>
        /// <param name="text">被判断的字符串</param>
        /// <returns></returns>
        public static bool IsNumeric(this string text)
        {
            string pattern = @"^\-?[0-9]+$";
            return Regex.IsMatch(text, pattern);
        }

        /// <summary>
        /// 是否是Int类型字符串
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsInteger(this string text)
        {
            int i;
            return int.TryParse(text, out i);
        }

        /// <summary>
        /// 判断是否是是长整形字符串
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsInt64(this string text)
        {
            long i;
            return long.TryParse(text, out i);
        }

        /// <summary>
        /// 判断是否是中国的手机号码
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsCnPhone(this string text)
        {
            string pattern = @"^1[3|4|5|7|8]\d{9}$";
            return Regex.IsMatch(text, pattern);
        }

        /// <summary>
        /// 判断字符串是否是相对URL
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsUri(this string text)
        {
            Uri uri;
            return Uri.TryCreate(text, UriKind.Relative, out uri);
        }

        /// <summary>
        /// 判断一个字符串是否为url
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsUrl(this string str)
        {
            try
            {
                string Url = @"^http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$";
                return Regex.IsMatch(str, Url);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
