namespace TeamCores.Models
{
	/// <summary>
	/// 考卷模板简要信息
	/// </summary>
	public class ExamSimpleInfo
	{
		/// <summary>
		/// 考试题目ID
		/// </summary>
		public long ExamId { get; set; }

		/// <summary>
		/// 考试题目标题
		/// </summary>
		public string Title { get; set; }

        /// <summary>
        /// 考卷类型
        /// </summary>
        public int ExamType { get; set; }

		/// <summary>
		/// 考试时间（单位：分钟）
		/// </summary>
		public int Time { get; set; }
		
		/// <summary>
		/// 考卷总分
		/// </summary>
		public int Total { get; set; }

		/// <summary>
		/// 考卷及格分
		/// </summary>
		public int Pass { get; set; }
	}
}
