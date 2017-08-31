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
		/// ��ǰ�û�ѧϰ�ƻ�����
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
		/// ��ȡָ��ѧԱ��ѧϰ�ƻ�
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
		/// ��ȡ�û�ѧϰ�ƻ�ѧϰ���
		/// </summary>
		/// <param name="planId">ѧϰ�ƻ�ID</param>
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