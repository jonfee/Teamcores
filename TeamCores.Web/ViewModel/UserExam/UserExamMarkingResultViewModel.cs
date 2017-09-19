using System.Collections.Generic;
using TeamCores.Common.Json;

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
		public string Result { get; set; }

		/// <summary>
		/// 阅卷结果的健值结构形式
		/// </summary>
		public Dictionary<long, int> ResultDictionary
		{
			get
			{
				return JsonUtility.JsonDeserialize<Dictionary<long, int>>(Result);
			}
		}
	}
}
