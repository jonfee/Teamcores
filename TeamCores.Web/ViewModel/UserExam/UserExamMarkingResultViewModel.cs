using System.Collections.Generic;

namespace TeamCores.Web.ViewModel.UserExam
{
	/// <summary>
	/// 用户考卷阅卷结果提交视图模型
	/// </summary>
	public class UserExamMarkingResultViewModel
    {
		public long UserExamId { get; set; }

		/// <summary>
		/// 阅卷结果（题目及对应得分情况）集合
		/// </summary>
		public Dictionary<long, int> Result { get; set; }
    }
}
