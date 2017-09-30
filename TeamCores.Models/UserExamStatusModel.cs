using System;

namespace TeamCores.Models
{
	public class UserExamStatusModel
    {
		/// <summary>
		/// 用户考卷数据ID
		/// </summary>
		public long Id { get; set; }

		/// <summary>
		/// 用户ID
		/// </summary>
		public long UserId { get; set; }

		/// <summary>
		/// 答卷总计时间(分钟）
		/// </summary>
		public int Times { get; set; }
		
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
	}
}
