using Microsoft.AspNetCore.Mvc;
using TeamCores.Domain.Services;
using TeamCores.Misc.Controller;
using TeamCores.Web.ViewModel.UserStudy;

namespace TeamCores.Web.Api
{
	[Route("api/UserStudyPlan")]
	public class UserStudyPlanController : BaseController
	{
		UserStudyPlanService service = null;

		public UserStudyPlanController()
		{
			service = new UserStudyPlanService();
		}

		/// <summary>
		/// 用户学习计划搜索
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("searchplans")]
		public IActionResult SearchPlans(PlanSearcherViewModel model)
		{
			var data = service.Search(model.PageSize, model.PageIndex, model.StudentId, model.StudyStatus);

			return Ok(data);
		}

		/// <summary>
		/// 获取用户学习计划学习情况
		/// </summary>
		/// <param name="userId">用户ID</param>
		/// <param name="planId">学习计划ID</param>
		/// <returns></returns>
		[HttpPost]
		[Route("details")]
		public IActionResult GetDetails(long userId, long planId)
		{
			var data = service.GetPlanDetails(userId, planId);

			return Ok(data);
		}
	}
}