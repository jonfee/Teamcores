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
	/// ¿¼¾íÏà¹Ø½Ó¿Ú¿ØÖÆÆ÷
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
		/// Ìí¼Ó¿¼¾íÐÅÏ¢
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
		/// ÉèÖÃ¿¼¾í×´Ì¬ÎªÆôÓÃ
		/// </summary>
		/// <param name="id">¿¼¾íID</param>
		/// <returns></returns>
		[HttpPost]
		[Route("setenable")]
		public IActionResult SetEnable(long id)
		{
			var success = service.SetEnable(id);

			return Ok(success);
		}

		/// <summary>
		/// ÉèÖÃ¿¼¾í×´Ì¬Îª½ûÓÃ
		/// </summary>
		/// <param name="id">¿¼¾íID</param>
		/// <returns></returns>
		[HttpPost]
		[Route("setdisable")]
		public IActionResult SetDisable(long id)
		{
			var success = service.SetDisable(id);

			return Ok(success);
		}

		/// <summary>
		/// ±à¼­¿¼¾í
		/// </summary>
		/// <param name="model">¿¼¾íµÄ±à¼­ÄÚÈÝ</param>
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
		/// ËÑË÷¿¼¾í
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
	}
}