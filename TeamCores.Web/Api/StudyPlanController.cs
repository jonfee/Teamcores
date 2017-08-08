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
		/// <param name="id">学习计划ID</param>
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
		/// <param name="id">学习计划ID</param>
		/// <returns></returns>
		[HttpPost]
		[Route("setdisable")]
		public IActionResult SetDisable(long id)
		{
			var success = service.SetDisable(id);

			return Ok(success);
		}

		/// <summary>
		/// 获取学习计划详细信息
		/// </summary>
		/// <param name="id">学习计划ID</param>
		/// <returns></returns>
		[HttpPost]
		[Route("details")]
		public IActionResult GetStudyPlanDetails(long id)
		{
			var data = service.GetStudyPlanDetails(id);

			return Ok(data);
		}

		/// <summary>
		/// 获取学习计划中指定学员的学习情况
		/// </summary>
		/// <param name="planId">学习计划ID</param>
		/// <param name="studentId">学员用户ID</param>
		/// <returns></returns>
		[HttpPost]
		[Route("studingdetails")]
		public IActionResult GetUserStudyPlanDetails(long planId, long studentId)
		{
			var data = service.GetyPlanStudingDetails(planId, studentId);

			return Ok(data);
		}
	}
}