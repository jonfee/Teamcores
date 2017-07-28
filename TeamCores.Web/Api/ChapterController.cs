using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeamCores.Domain.Services;
using TeamCores.Misc.Controller;
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
	}
}