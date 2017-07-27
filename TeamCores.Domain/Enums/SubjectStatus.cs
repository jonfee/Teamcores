using System.ComponentModel;

namespace TeamCores.Domain.Enums
{
    /// <summary>
    /// 科目状态枚举
    /// </summary>
    public enum SubjectStatus
    {
        /// <summary>
		/// 启用
		/// </summary>
		[Description("启用")]
        ENABLED = 1,
        /// <summary>
        /// 禁用
        /// </summary>
        [Description("禁用")]
        DISABLED = 0
    }
}
