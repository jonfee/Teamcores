using Microsoft.AspNetCore.Mvc;
using TeamCores.Common.Utilities;
using TeamCores.Domain.Models.Exams;
using TeamCores.Domain.Services;
using TeamCores.Domain.Services.Request;
using TeamCores.Misc.Controller;
using TeamCores.Web.ViewModel.Exams;

namespace TeamCores.Web.Api
{
	/// <summary>
	/// ������ؽӿڿ�����
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
		/// ��ӿ�����Ϣ
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("add")]
		public IActionResult Add(NewExamsRequest request)
		{
			var success = service.Add(request);

			return Ok(success);
		}

		/// <summary>
		/// ���ÿ���״̬Ϊ����
		/// </summary>
		/// <param name="id">����ID</param>
		/// <returns></returns>
		[HttpPost]
		[Route("setenable")]
		public IActionResult SetEnable(long id)
		{
			var success = service.SetEnable(id);

			return Ok(success);
		}

		/// <summary>
		/// ���ÿ���״̬Ϊ����
		/// </summary>
		/// <param name="id">����ID</param>
		/// <returns></returns>
		[HttpPost]
		[Route("setdisable")]
		public IActionResult SetDisable(long id)
		{
			var success = service.SetDisable(id);

			return Ok(success);
		}

		/// <summary>
		/// �༭����
		/// </summary>
		/// <param name="model">����ı༭����</param>
		/// <returns></returns>
		[HttpPost]
		[Route("modify")]
		public IActionResult Modify(ExamsModifyViewModel model)
		{
			var examsId = model.ExamsId;

			ExamsModifyState state = null;
			model.CopyTo(state);

			var success = service.ModifyTo(examsId, state);

			return Ok(success);
		}

		/// <summary>
		/// ��������
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("search")]
		public IActionResult Search(ExamsSearcherViewModel model)
		{
			ExamsSearchRequest request = new ExamsSearchRequest
			{
				CourseId = model.CourseId,
				Keyword = model.Keyword,
				PageIndex = model.PageIndex,
				PageSize = model.PageSize,
				Status = model.Status
			};

			var result = service.Search(request);

			return Ok(result);
		}

		/// <summary>
		/// ��ȡ������Ϣ
		/// </summary>
		/// <param name="id">����ID</param>
		/// <returns></returns>
		[HttpPost]
		[Route("get")]
		public IActionResult GetExams(long id)
		{
			var data = service.GetExams(id);

			return Ok(id);
		}

		/// <summary>
		/// ��ȡ������ϸ��Ϣ
		/// </summary>
		/// <param name="id">����ID</param>
		/// <returns></returns>
		[HttpPost]
		[Route("details")]
		public IActionResult GetDetails(long id)
		{
			var data = service.GetDetails(id);

			return Ok(id);
		}
	}
}