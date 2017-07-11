using System;

namespace TeamCores.Models
{
    public class JsonModel<T>
    {
        /// <summary>
        /// 错误代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 附加数据
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 是否错误
        /// </summary>
        public bool Error
        {
            get
            {
                if (string.IsNullOrEmpty(Code))
                {
                    return false;
                }
                return true;
            }
        }
    }
}
