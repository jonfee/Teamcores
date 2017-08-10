using System.ComponentModel;

namespace TeamCores.Domain.Enums
{
	/// <summary>
	/// 考卷模板状态枚举
	/// </summary>
	public enum ExamsStatus
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
