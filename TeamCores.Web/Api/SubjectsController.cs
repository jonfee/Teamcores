using Microsoft.AspNetCore.Mvc;
using TeamCores.Domain.Services;
using TeamCores.Misc.Controller;
using TeamCores.Web.ViewModel.Subject;

namespace TeamCores.Web.Api
{
	[Route("api/Subjects")]
	public class SubjectsController : BaseController
	{
		SubjectService service = null;

		public SubjectsController()
		{
			service = new SubjectService();
		}

		/// <summary>
		/// 新增科目
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
		/// 删除科目
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
		/// 设置状态为”启用“
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
		/// 设置状态为”禁用“
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
		/// 对科目名称进行重命名
		/// </summary>
		/// <param name="id">科目ID</param>
		/// <param name="newName">新名称</param>
		/// <returns></returns>
		[HttpPost]
		[Route("rename")]
		public IActionResult Rename(long id, string newName)
		{
			var success = service.Rename(id, newName);

			return Ok(success);
		}

		/// <summary>
		/// 搜索科目
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

		/// <summary>
		/// 获取科目信息
		/// </summary>
		/// <param name="id">科目ID</param>
		/// <returns></returns>
		[HttpPost]
		[Route("get")]
		public IActionResult GetSubject(long id)
		{
			var data = service.GetSubject(id);

			return Ok(data);
		}

		/// <summary>
		/// 获取科目的详细信息
		/// </summary>
		/// <param name="id">科目ID</param>
		/// <returns></returns>
		[HttpPost]
		[Route("details")]
		public IActionResult GetDetails(long id)
		{
			var data = service.GetDetails(id);

			return Ok(data);
		}
	}
}