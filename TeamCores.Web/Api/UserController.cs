using Microsoft.AspNetCore.Mvc;
using TeamCores.Domain.Services;
using TeamCores.Models;
using TeamCores.Web.ViewModel.User;

namespace TeamCores.Web.Api
{
    [Route("api/User")]
    public class UserController : Controller
    {
		[HttpPost]
		[Route("search")]
		public IActionResult Search(UserSearcherViewModel searcher)
		{
			if (searcher == null)
			{
				searcher = new UserSearcherViewModel
				{
					PageIndex = 1,
					PageSize = 10
				};
			}

			var result = new UserService().Search(searcher.PageSize, searcher.PageIndex, searcher.Keyword, searcher.Status);

			var data = new JsonModel<object>()
			{
				Data = result
			};

			return Json(data);
		}
	}
}