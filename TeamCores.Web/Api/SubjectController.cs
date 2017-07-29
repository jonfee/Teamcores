using Microsoft.AspNetCore.Mvc;
using TeamCores.Domain.Models.Subject;
using TeamCores.Domain.Services;
using TeamCores.Misc.Controller;
using TeamCores.Misc.Filters;
using TeamCores.Web.ViewModel.Subject;

namespace TeamCores.Web.Api
{
	[Route("api/Subject")]
	public class SubjectController : BaseController
	{
		SubjectService service = null;

		public SubjectController()
		{
			service = new SubjectService();
		}

		/// <summary>
		/// ������Ŀ
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("add")]
		public IActionResult Add(NewSubjectViewModel model)
		{
			var success = service.AddSubject(model.Name);

			return Ok(success);
		}

		/// <summary>
		/// ɾ����Ŀ
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("delete")]
		public IActionResult Delete(long id)
		{
			var success = service.Delete(id);

			return Ok(success);
		}

		/// <summary>
		/// ����״̬Ϊ�����á�
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
		/// ����״̬Ϊ�����á�
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

		/// <summary>
		/// �Կ�Ŀ���ƽ���������
		/// </summary>
		/// <param name="id">��ĿID</param>
		/// <param name="newName">������</param>
		/// <returns></returns>
		[HttpPost]
		[Route("rename")]
		public IActionResult Rename(long id, string newName)
		{
			var success = service.Rename(id, newName);

			return Ok(success);
		}

		/// <summary>
		/// ������Ŀ
		/// </summary>
		/// <param name="searcher"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("search")]
		public IActionResult Search(SubjectSearcherViewModel searcher)
		{
			if (searcher == null)
			{
				searcher = new SubjectSearcherViewModel
				{
					PageIndex = 1,
					PageSize = 10
				};
			}

			var result = service.Search(searcher.PageSize, searcher.PageIndex, searcher.Keyword, searcher.Status);

			return Ok(result);
		}
	}
}