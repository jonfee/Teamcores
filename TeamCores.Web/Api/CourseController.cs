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
		/// 娣诲姞璇剧▼
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
		/// 鑾峰彇璇剧▼淇℃伅
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
		/// 鑾峰彇鎸囧畾鐘舵€佷笅鐨勬墍鏈夎绋婭D鍙婂搴斿悕绉?
		/// </summary>
		/// <param name="status">璇剧▼鐘舵€?/param>
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
		/// 鑾峰彇璇剧▼璇︾粏淇℃伅
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
		/// 璁剧疆璇剧▼涓哄惎鐢ㄧ姸鎬?
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
		/// 璁剧疆璇剧▼涓虹鐢ㄧ姸鎬?
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
		/// 鍒犻櫎璇剧▼
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
		/// 缂栬緫璇剧▼淇℃伅
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
		/// 鎼滅储璇剧▼
		/// </summary>
		/// <param name="searcher">璇剧▼鎼滅储鍣ㄨ鍥炬ā鍨?/param>
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