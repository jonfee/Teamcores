namespace TeamCores.Web.ViewModel.Course
{
	/// <summary>
	/// 新课程视图模型
	/// </summary>
	public class NewCourseViewModel
    {
		/// <summary>
		/// 归属科目
		/// </summary>
		public long SubjectId { get; set; }

		/// <summary>
		/// 课程标题
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// 课程封面图片
		/// </summary>
		public string Image { get; set; }

		/// <summary>
		/// 摘要
		/// </summary>
		public string Remarks { get; set; }

		/// <summary>
		/// 内容
		/// </summary>
		public string Content { get; set; }

		/// <summary>
		/// 学习目标
		/// </summary>
		public string Objective { get; set; }
	}
}
