namespace TeamCores.Models
{
	/// <summary>
	/// 学习计划及计划下的课程
	/// </summary>
	public class PlanCoursesModel
    {
		/// <summary>
		/// 计划ID
		/// </summary>
		public long PlanId { get; set; }

		/// <summary>
		/// 课程ID
		/// </summary>
		public long[] Courses { get; set; }
    }
}
