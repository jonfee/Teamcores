using Microsoft.AspNetCore.Mvc;
using TeamCores.Domain.Services;
using TeamCores.Misc;
using TeamCores.Misc.Controller;
using TeamCores.Misc.Filters;
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
		/// 当前用户学习计划搜索
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("myplans")]
		[UserAuthorization]
		public IActionResult SearchPlans(PlanSearcherViewModel model)
		{
			long userId = Utility.GetUserContext().UserId;

			var data = service.Search(model.PageSize, model.PageIndex, userId, model.StudyStatus);

			return Ok(data);
		}

		/// <summary>
		/// 获取指定学员的学习计划
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("searchplans")]
		[UserAuthorization(RequiredPermissions = "S01")]
		public IActionResult SearchUserPlans(UserPlanSearchViewModel model)
		{
			var data = service.Search(model.PageSize, model.PageIndex, model.StudentId, model.StudyStatus);

			return Ok(data);
		}

		/// <summary>
		/// 获取用户学习计划学习情况
		/// </summary>
		/// <param name="planId">学习计划ID</param>
		/// <returns></returns>
		[HttpGet]
		[Route("details")]
		[UserAuthorization]
		public IActionResult GetDetails(long planId)
		{
			long userId = Utility.GetUserContext().UserId;

			var data = service.GetPlanDetails(userId, planId);

			return Ok(data);
		}
	}
}