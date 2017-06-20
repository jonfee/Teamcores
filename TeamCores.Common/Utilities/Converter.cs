using System;
using System.Collections.Generic;
using System.Text;

namespace TeamCores.Common.Utilities
{
    public class Converter
    {
        public static DateTime? GetDateTime(string text)
        {
            DateTime time;
            var result = DateTime.TryParse(text, out time);
            if (result)
            {
                return time;
            }
            else
            {
                return null;
            }
        }

        public static long ToInt64(string text)
        {
            long num;
            var result = long.TryParse(text, out num);
            return num;
        }

        public static int ToInt(string text)
        {
            int num;
            var result = int.TryParse(text, out num);
            return num;
        }
    }
}
