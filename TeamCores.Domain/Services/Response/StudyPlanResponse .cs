namespace TeamCores.Domain.Services.Response
{
	public class StudyPlanResponse : Data.Entity.StudyPlan
	{
		/// <summary>
		/// 计划制定者
		/// </summary>
		public string Creator { get; set; }

		public StudyPlanResponse() { }

		public StudyPlanResponse(Data.Entity.StudyPlan plan)
		{
			if (plan != null)
			{
				PlanId = plan.PlanId;
				UserId = plan.UserId;
				Student = plan.Student;
				Title = plan.Title;
				Content = plan.Content;
				Status = plan.Status;
				CreateTime = plan.CreateTime;
			}
		}
	}
}
