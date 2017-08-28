using Microsoft.AspNetCore.Mvc;
using TeamCores.Domain.Services;
using TeamCores.Misc.Controller;
using TeamCores.Misc.Filters;
using TeamCores.Web.ViewModel.Chapter;

namespace TeamCores.Web.Api
{
	[Route("api/Chapter")]
	public class ChapterController : BaseController
	{
		ChapterService service = null;

		public ChapterController()
		{
			service = new ChapterService();
		}

		/// <summary>
		/// 新增课程章节
		/// </summary>
		/// <param name="model">新课程章节视图模型</param>
		/// <returns></returns>
		[HttpPost]
		[Route("add")]
		[UserAuthorization(RequiredPermissions = "C04")]
		public IActionResult Add(NewChapterViewModel model)
		{
			var success = service.Add(
				model.CourseId,
				model.ParentId,
				model.Title,
				model.Content,
				model.Video);

			return Ok(success);
		}

		/// <summary>
		/// 启用章节
		/// </summary>
		/// <param name="id">章节ID</param>
		/// <returns></returns>
		[HttpPost]
		[Route("setenable")]
		[UserAuthorization(RequiredPermissions = "C05")]
		public IActionResult SetEnable(long id)
		{
			var success = service.SetEnable(id);

			return Ok(success);
		}

		/// <summary>
		/// 禁用章节
		/// </summary>
		/// <param name="id">章节ID</param>
		/// <returns></returns>
		[HttpPost]
		[Route("setdisabel")]
		[UserAuthorization(RequiredPermissions = "C05")]
		public IActionResult SetDisable(long id)
		{
			var success = service.SetDisable(id);

			return Ok(success);
		}

		/// <summary>
		/// 删除章节
		/// </summary>
		/// <param name="id">章节ID</param>
		/// <returns></returns>
		[HttpPost]
		[Route("delete")]
		[UserAuthorization(RequiredPermissions = "C06")]
		public IActionResult Delete(long id)
		{
			var success = service.Delete(id);

			return Ok(success);
		}

		/// <summary>
		/// 编辑章节信息
		/// </summary>
		/// <param name="model">章节编辑视图模型</param>
		/// <returns></returns>
		[HttpPost]
		[Route("modify")]
		[UserAuthorization(RequiredPermissions = "C05")]
		public IActionResult Modify(ModifyChapterViewModel model)
		{
			var success = service.Modify(
				model.ChapterId,
				model.CourseId,
				model.ParentId,
				model.Title,
				model.Content,
				model.Video,
				model.Status);

			return Ok(success);
		}

		/// <summary>
		/// 搜索课程章节
		/// </summary>
		/// <param name="model">课程章节搜索器模型</param>
		/// <returns></returns>
		[HttpPost]
		[Route("search")]
		[UserAuthorization(RequiredPermissions = "C01")]
		public IActionResult Search(ChapterSearcherViewModel searcher)
		{
			if (searcher == null)
			{
				searcher = new ChapterSearcherViewModel
				{
					PageIndex = 1,
					PageSize = 10
				};
			}

			var result = service.Search(
				searcher.PageSize, 
				searcher.PageIndex, 
				searcher.Keyword, 
				searcher.CourseId, 
				searcher.Status);

			return Ok(result);
		}

		/// <summary>
		/// 获取章节信息
		/// </summary>
		/// <param name="id">章节ID</param>
		/// <returns></returns>
		[HttpPost]
		[Route("get")]
		[UserAuthorization(RequiredPermissions = "C01")]
		public IActionResult GetChapter(long id)
		{
			var data = service.GetChapter(id);

			return Ok(data);
		}

		/// <summary>
		/// 获取课程章节详细信息
		/// </summary>
		/// <param name="id">章节ID</param>
		/// <returns></returns>
		[HttpPost]
		[Route("details")]
		[UserAuthorization(RequiredPermissions = "C01")]
		public IActionResult GetDetails(long id)
		{
			var data = service.GetDetails(id);

			return Ok(data);
		}
	}
}