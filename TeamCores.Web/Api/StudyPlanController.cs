using Microsoft.AspNetCore.Mvc;
using TeamCores.Domain.Services;
using TeamCores.Misc;
using TeamCores.Misc.Controller;
using TeamCores.Web.ViewModel.StudyPlan;

namespace TeamCores.Web.Api
{
	[Route("api/StudyPlan")]
	public class StudyPlanController : BaseController
	{
		StudyPlanService service = null;

		public StudyPlanController()
		{
			service = new StudyPlanService();
		}

		/// <summary>
		/// 添加新学习计划
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("add")]
		public IActionResult Add(NewStudyPlanViewModel model)
		{
			var success = service.Add(
				 Utility.GetUserContext().UserId,
				 model.Title,
				 model.Content,
				 model.Courses,
				 model.Students);

			return Ok(success);
		}

		/// <summary>
		/// 搜索学习计划
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("search")]
		public IActionResult Search(StudyPlanSearcherViewModel model)
		{
			var success = service.Search(
				model.PageSize,
				model.PageIndex,
				model.Keyword,
				model.Status);

			return Ok(success);
		}

		/// <summary>
		/// 启用学习计划
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("setenable")]
		public IActionResult SetEnable(long id)
		{
			var success = service.SetEnable(id);

			return Ok(success);
		}

		/// <summary>
		/// 禁用学习计划
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("setdisable")]
		public IActionResult SetDisable(long id)
		{
			var success = service.SetDisable(id);

			return Ok(success);
		}
	}
}