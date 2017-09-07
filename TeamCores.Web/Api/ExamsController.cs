using Microsoft.AspNetCore.Mvc;
using TeamCores.Common.Utilities;
using TeamCores.Domain.Models.Exams;
using TeamCores.Domain.Services;
using TeamCores.Domain.Services.Request;
using TeamCores.Misc.Controller;
using TeamCores.Misc.Filters;
using TeamCores.Web.ViewModel.Exams;

namespace TeamCores.Web.Api
{
	/// <summary>
	/// 考卷相关接口控制器
	/// </summary>
	[Route("api/Exams")]
	public class ExamsController : BaseController
	{
		ExamsService service = null;

		public ExamsController()
		{
			service = new ExamsService();
		}

		/// <summary>
		/// 添加考卷信息
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("add")]
		[UserAuthorization(RequiredPermissions = "E02")]
		public IActionResult Add(NewExamsRequest request)
		{
			var success = service.Add(request);

			return Ok(success);
		}

		/// <summary>
		/// 设置考卷状态为启用
		/// </summary>
		/// <param name="id">考卷ID</param>
		/// <returns></returns>
		[HttpPost]
		[Route("setenable")]
		[UserAuthorization(RequiredPermissions = "E03")]
		public IActionResult SetEnable(long id)
		{
			var success = service.SetEnable(id);

			return Ok(success);
		}

		/// <summary>
		/// 设置考卷状态为禁用
		/// </summary>
		/// <param name="id">考卷ID</param>
		/// <returns></returns>
		[HttpPost]
		[Route("setdisable")]
		[UserAuthorization(RequiredPermissions = "E03")]
		public IActionResult SetDisable(long id)
		{
			var success = service.SetDisable(id);

			return Ok(success);
		}

		/// <summary>
		/// 编辑考卷
		/// </summary>
		/// <param name="model">考卷的编辑内容</param>
		/// <returns></returns>
		[HttpPost]
		[Route("modify")]
		[UserAuthorization(RequiredPermissions = "E03")]
		public IActionResult Modify(ExamsModifyViewModel model)
		{
			var examsId = model.ExamsId;

			ExamsModifyState state = null;
			model.CopyTo(state);

			var success = service.ModifyTo(examsId, state);

			return Ok(success);
		}

		/// <summary>
		/// 搜索考卷
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("search")]
		[UserAuthorization(RequiredPermissions = "E01")]
		public IActionResult Search(ExamsSearcherViewModel model)
		{
			ExamsSearchRequest request = new ExamsSearchRequest
			{
				CourseId = model.CourseId,
				Keyword = model.Keyword,
				PageIndex = model.PageIndex,
				PageSize = model.PageSize,
				Type = model.Type,
				Status = model.Status
			};

			var result = service.Search(request);

			return Ok(result);
		}

		/// <summary>
		/// 获取考卷信息
		/// </summary>
		/// <param name="id">考卷ID</param>
		/// <returns></returns>
		[HttpGet]
		[Route("get")]
		[UserAuthorization(RequiredPermissions = "E01")]
		public IActionResult GetExams(long id)
		{
			var data = service.GetExams(id);

			return Ok(id);
		}

		/// <summary>
		/// 获取考卷详细信息
		/// </summary>
		/// <param name="id">考卷ID</param>
		/// <returns></returns>
		[HttpPost]
		[Route("details")]
		[UserAuthorization(RequiredPermissions = "E01")]
		public IActionResult GetDetails(long id)
		{
			var data = service.GetDetails(id);

			return Ok(id);
		}
	}
}