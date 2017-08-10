using System.Collections.Generic;

namespace TeamCores.Domain.Services.Request
{
	/// <summary>
	/// 考卷答案提交请求
	/// </summary>
	public class ExamPagerSubmitRequest
	{
		/// <summary>
		/// 用户ID
		/// </summary>
		public long UserId { get; set; }

		/// <summary>
		/// 用户的考卷ID
		/// </summary>
		public long UserExamId { get; set; }

		/// <summary>
		/// 考卷作答结果
		/// </summary>
		public Dictionary<long, string> AnswerResults { get; set; }
	}
}
