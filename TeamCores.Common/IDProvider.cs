using System;
using System.Collections.Generic;
using System.Text;
using TeamCores.Common.Utilities;

namespace TeamCores.Common
{
    public class IDProvider
    {
        public static uint District
        {
            get
            {
                return 1;
            }
        }

        public static uint Platform
        {
            get
            {
                return 1;
            }
        }
        /// <summary>
        /// 生成一个新的ID
        /// </summary>
        /// <returns></returns>
        public static long NewId
        {
            get
            {
                return (long)IDCreater.NewId(Platform, District);
            }
        }
    }
}
