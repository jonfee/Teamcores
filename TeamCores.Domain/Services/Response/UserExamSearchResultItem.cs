using System;

namespace TeamCores.Domain.Services.Response
{
	/// <summary>
	/// 用户考卷搜索结果单数据项
	/// </summary>
	public class UserExamSearchResultItem
	{
		/// <summary>
		/// 用户考卷ID
		/// </summary>
		public long UserExamId { get; set; }

		/// <summary>
		/// 用户ID
		/// </summary>
		public long UserId { get; set; }

		/// <summary>
		/// 用户姓名
		/// </summary>
		public string UserName { get; set; }

		/// <summary>
		/// 用户手机号
		/// </summary>
		public string UserMobile { get; set; }

		/// <summary>
		/// 用户头衔
		/// </summary>
		public string UserTitle { get; set; }

		/// <summary>
		/// 考试题目ID
		/// </summary>
		public long ExamId { get; set; }

		/// <summary>
		/// 考卷模板标题
		/// </summary>
		public string ExamTitle { get; set; }

		/// <summary>
		/// 考试时间（单位：分钟）
		/// </summary>
		public int MaxTime { get; set; }

		/// <summary>
		/// 实际答卷时间(分钟）
		/// </summary>
		public int ActualTime { get; set; }

		/// <summary>
		/// 考卷总分
		/// </summary>
		public int Total { get; set; }

		/// <summary>
		/// 考卷及格分
		/// </summary>
		public int Pass { get; set; }

		/// <summary>
		/// 考试得分
		/// </summary>
		public int Score { get; set; }

		/// <summary>
		/// 创建时间，也是考试开始时间
		/// </summary>
		public DateTime CreateTime { get; set; }

		/// <summary>
		/// 交卷时间
		/// </summary>
		public DateTime? PostTime { get; set; }

		/// <summary>
		/// 阅卷状态
		/// </summary>
		public int MarkingStatus { get; set; }

		/// <summary>
		/// 阅卷时间
		/// </summary>
		public DateTime? MarkingTime { get; set; }
	}
}
