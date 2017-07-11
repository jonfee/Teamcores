using System;
using System.Collections.Generic;
using System.Text;

namespace TeamCores.Models
{
    public class PagerModel<T>
    {
        /// <summary>
        /// 总记录数
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 分页大小
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// 当前页数
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 表格数据
        /// </summary>
        public List<T> Table { get; set; }
    }
}
