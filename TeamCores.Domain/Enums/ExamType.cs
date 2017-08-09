using System.ComponentModel;

namespace TeamCores.Domain.Enums
{
	/// <summary>
	/// 考卷类型
	/// </summary>
	public enum ExamType
	{
		/// <summary>
		/// 练习卷
		/// </summary>
		[Description("练习卷")]
		TEST_EXAM = 1,
		/// <summary>
		/// 考试卷
		/// </summary>
		[Description("考试卷")]
		LIVE_EXAM
	}
}
