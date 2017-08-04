using Microsoft.AspNetCore.Mvc;
using TeamCores.Domain.Services;
using TeamCores.Misc.Controller;
using TeamCores.Web.ViewModel.UserStudy;

namespace TeamCores.Web.Api
{
	[Route("api/UserStudy")]
	public class UserStudyController : BaseController
	{
		UserStudyPlanService service = null;

		public UserStudyController()
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
			var data = service.Search(model.PageSize, model.PageIndex, model.StudyStatus);

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

		/// <summary>
		/// ѧԱѧϰ�γ��½�
		/// </summary>
		/// <param name="userId">ѧԱ�û�ID</param>
		/// <param name="chapterId">ѧϰ�Ŀγ��½�ID</param>
		/// <returns></returns>
		[HttpPost]
		[Route("studing")]
		public IActionResult StudyChapter(long userId, long chapterId)
		{
			var chapterService = new ChapterService();

			var data = chapterService.StudentStuding(userId, chapterId);

			return Ok(data);
		}
	}
}