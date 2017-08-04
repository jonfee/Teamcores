namespace TeamCores.Models
{
	/// <summary>
	/// 用户学习计划进度
	/// </summary>
	public class UserStudyPlanProgressModel
	{
		/// <summary>
		/// 学员用户ID
		/// </summary>
		public long StudentId { get; set; }

		/// <summary>
		/// 计划ID
		/// </summary>
		public long PlanId { get; set; }

		/// <summary>
		/// 进度
		/// </summary>
		public float Progress { get; set; }
	}
}
