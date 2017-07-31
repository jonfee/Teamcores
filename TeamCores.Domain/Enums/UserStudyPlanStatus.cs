using System.ComponentModel;

namespace TeamCores.Domain.Enums
{
	/// <summary>
	/// 用户学习计划状态 枚举
	/// </summary>
	public enum UserStudyPlanStatus
    {
		/// <summary>
		/// 未开始
		/// </summary>
		[Description("未开始")]
		NOTSTARTED =1,
		/// <summary>
		/// 学习中
		/// </summary>
		[Description("学习中")]
		STUDYING,
		/// <summary>
		/// 已完成
		/// </summary>
		[Description("已完成")]
		COMPLETE
	}
}
