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
		/// �û�ѧϰ�ƻ�����
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
		/// ��ȡ�û�ѧϰ�ƻ�ѧϰ���
		/// </summary>
		/// <param name="userId">�û�ID</param>
		/// <param name="planId">ѧϰ�ƻ�ID</param>
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