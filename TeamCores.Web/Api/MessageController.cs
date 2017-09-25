using Microsoft.AspNetCore.Mvc;
using TeamCores.Domain.Services;
using TeamCores.Domain.Services.Request;
using TeamCores.Misc;
using TeamCores.Misc.Controller;
using TeamCores.Misc.Filters;
using TeamCores.Web.ViewModel.Message;

namespace TeamCores.Web.Api
{
	[Route("api/Message")]
	public class MessageController : BaseController
	{
		MessageService service = null;

		public MessageController()
		{
			service = new MessageService();
		}

		[HttpGet]
		[Route("details/{id}")]
		[UserAuthorization]
		public IActionResult Details(long id)
		{
			long userId = Utility.GetUserContext().UserId;

			var message = service.ReadMessasge(userId, id);

			return Ok(message);
		}

		/// <summary>
		/// 查看消息列表
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("search")]
		[UserAuthorization(RequiredPermissions = "T01")]
		public IActionResult Search(MessageSearchViewModel model)
		{
			long userId = Utility.GetUserContext().UserId;

			MessageSearchRequest request = new MessageSearchRequest
			{
				PageIndex = model.PageIndex,
				PageSize = model.PageSize,
				IsReaded = model.IsReaded,
				ReceiverId = userId
			};

			var data = service.Search(request);

			return Ok(data);
		}
	}
}