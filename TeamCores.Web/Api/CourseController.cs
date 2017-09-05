using Microsoft.AspNetCore.Mvc;
using TeamCores.Domain.Services;
using TeamCores.Misc;
using TeamCores.Misc.Controller;
using TeamCores.Misc.Filters;
using TeamCores.Web.ViewModel.Course;

namespace TeamCores.Web.Api
{
	[Route("api/Course")]
	public class CourseController : BaseController
	{
		CourseService service = null;

		public CourseController()
		{
			service = new CourseService();
		}

		/// <summary>
		/// 添加课程
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("add")]
		[UserAuthorization(RequiredPermissions = "C02")]
		public IActionResult Add(NewCourseViewModel model)
		{
			var success = service.Add(
				Utility.GetUserContext().UserId,
				model.SubjectId,
				model.Title,
				model.Image,
				model.Content,
				model.Remarks,
				model.Objective);

			return Ok(success);
		}

		/// <summary>
		/// 获取课程信息
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("get")]
		[UserAuthorization(RequiredPermissions = "C01")]
		public IActionResult GetCourse(long id)
		{
			var data = service.GetCourse(id);

			return Ok(data);
		}

		/// <summary>
		/// 获取指定状态下的所有课程ID及对应名称
		/// </summary>
		/// <param name="status">课程状态</param>
		/// <returns></returns>
		[HttpGet]
		[Route("listforstatus")]
		[UserAuthorization]
		public IActionResult GetCourseIdNames(int? status)
		{
			var data = service.GetAllCourseIdNameList(status);

			return Ok(data);
		}

		/// <summary>
		/// 获取课程详细信息
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("details/{id}")]
		[UserAuthorization(RequiredPermissions = "C01")]
		public IActionResult GetDetails(long id, long studentId)
		{
			if (studentId == 0) studentId = Utility.GetUserContext().UserId;

			var data = service.GetDetails(id, studentId);

			return Ok(data);
		}

		/// <summary>
		/// 设置课程为启用状态
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("setenable")]
		[UserAuthorization(RequiredPermissions = "C03")]
		public IActionResult SetEnable(long id)
		{
			var success = service.SetEnable(id);

			return Ok(success);
		}

		/// <summary>
		/// 设置课程为禁用状态
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("setdisable")]
		[UserAuthorization(RequiredPermissions = "C03")]
		public IActionResult SetDisable(long id)
		{
			var success = service.SetDisable(id);

			return Ok(success);
		}

		/// <summary>
		/// 删除课程
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("delete")]
		[UserAuthorization(RequiredPermissions = "C04")]
		public IActionResult Delete(long id)
		{
			var success = service.Delete(id);

			return Ok(success);
		}

		/// <summary>
		/// 编辑课程信息
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("modify")]
		[UserAuthorization(RequiredPermissions = "C03")]
		public IActionResult Modify(ModifyCourseViewModel model)
		{
			var success = service.Modify(
				model.CourseId,
				model.SubjectId,
				model.Title,
				model.Image,
				model.Content,
				model.Remarks,
				model.Objective,
				model.Status);

			return Ok(success);
		}

		/// <summary>
		/// 搜索课程
		/// </summary>
		/// <param name="searcher">课程搜索器视图模型</param>
		/// <returns></returns>
		[HttpPost]
		[Route("search")]
		[UserAuthorization(RequiredPermissions = "C01")]
		public IActionResult Search(CourseSearcherViewModel searcher)
		{
			if (searcher == null)
			{
				searcher = new CourseSearcherViewModel
				{
					PageIndex = 1,
					PageSize = 10
				};
			}

			var result = service.Search(searcher.PageSize, searcher.PageIndex, searcher.Keyword, searcher.SubjectId, searcher.Status);

			return Ok(result);
		}
	}
}