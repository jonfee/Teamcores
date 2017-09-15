using System.Collections.Generic;
using System.Linq;
using TeamCores.Common.Json;

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
		/// 考卷的答案,格式为Json字符串
		/// </summary>
		public string Answers { get; set; }

		/// <summary>
		/// 考卷答案的健值结构形式
		/// </summary>
		public Dictionary<long, string> AnswersDictionary
		{
			get
			{
				return JsonUtility.JsonDeserialize<Dictionary<long, string>>(Answers);
			}
		}
    }
}
