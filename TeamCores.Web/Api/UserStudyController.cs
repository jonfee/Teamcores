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
		/// 用户学习计划搜索
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

		/// <summary>
		/// 学员学习课程章节
		/// </summary>
		/// <param name="userId">学员用户ID</param>
		/// <param name="chapterId">学习的课程章节ID</param>
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