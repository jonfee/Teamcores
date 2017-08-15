using System.Collections.Generic;

namespace TeamCores.Web.ViewModel.UserEvam
{
	/// <summary>
	/// 用户考卷答案提交视图模型
	/// </summary>
	public class UserExamSubmitViewModel
    {

		/// <summary>
		/// 用户考卷ID
		/// </summary>
		public long UserExamId { get; set; }

		/// <summary>
		/// 考卷的答案
		/// </summary>
		public Dictionary<long, string> Answers { get; set; }
    }
}
