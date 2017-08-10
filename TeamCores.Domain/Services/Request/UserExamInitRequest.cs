using System.Collections.Generic;
using TeamCores.Domain.Services.Response;

namespace TeamCores.Domain.Services.Request
{
	/// <summary>
	/// 用户参加考试时初始化考卷请求
	/// </summary>
	public class UserExamInitRequest
	{
		/// <summary>
		/// 用户参考的考卷ID
		/// </summary>
		public long UserExamId { get; set; }

		/// <summary>
		/// 用户ID
		/// </summary>
		public long UserId { get; set; }

		/// <summary>
		/// 考试题目ID
		/// </summary>
		public long ExamId { get; set; }

		/// <summary>
		/// 题目及作答结果
		/// </summary>
		public List<UserExamQuestionResult> QuestionsResults { get; set; }
	}
}
