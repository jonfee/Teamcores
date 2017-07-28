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
		/// �����γ��½�
		/// </summary>
		/// <param name="model">�¿γ��½���ͼģ��</param>
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

		/// <summary>
		/// �����½�
		/// </summary>
		/// <param name="id">�½�ID</param>
		/// <returns></returns>
		[HttpPost]
		[Route("setenable")]
		public IActionResult SetEnable(long id)
		{
			var success = service.SetEnable(id);

			return Ok(success);
		}

		/// <summary>
		/// �����½�
		/// </summary>
		/// <param name="id">�½�ID</param>
		/// <returns></returns>
		[HttpPost]
		[Route("setdisabel")]
		public IActionResult SetDisable(long id)
		{
			var success = service.SetDisable(id);

			return Ok(success);
		}

		/// <summary>
		/// ɾ���½�
		/// </summary>
		/// <param name="id">�½�ID</param>
		/// <returns></returns>
		[HttpPost]
		[Route("delete")]
		public IActionResult Delete(long id)
		{
			var success = service.Delete(id);

			return Ok(success);
		}

		/// <summary>
		/// �༭�½���Ϣ
		/// </summary>
		/// <param name="model">�½ڱ༭��ͼģ��</param>
		/// <returns></returns>
		[HttpPost]
		[Route("modify")]
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
		/// �����γ��½�
		/// </summary>
		/// <param name="model">�γ��½�������ģ��</param>
		/// <returns></returns>
		[HttpPost]
		[Route("search")]
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
	}
}