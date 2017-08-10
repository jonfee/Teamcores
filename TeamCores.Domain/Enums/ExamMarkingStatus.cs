using System.ComponentModel;

namespace TeamCores.Domain.Enums
{
	/// <summary>
	/// 考卷阅卷状态
	/// </summary>
	public enum ExamMarkingStatus
	{
		/// <summary>
		/// 已阅
		/// </summary>
		[Description("已阅")]
		READED,
		/// <summary>
		/// 未阅
		/// </summary>
		[Description("未阅")]
		DIDNOT_READ
	}
}
