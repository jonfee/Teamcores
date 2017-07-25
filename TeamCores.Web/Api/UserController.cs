using Microsoft.AspNetCore.Mvc;
using TeamCores.Domain.Services;
using TeamCores.Misc.Controller;
using TeamCores.Models;
using TeamCores.Web.ViewModel.User;

namespace TeamCores.Web.Api
{
    [Route("api/User")]
    public class UserController : BaseController
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

			return Ok(result);
		}

		[HttpPost]
		[Route("setenabled")]
		public IActionResult SetEnabled(long userId)
		{
			new UserService().SetEnabled(userId);

			return Ok(true);
		}

		[HttpPost]
		[Route("setdisabled")]
		public IActionResult SetDisabled(long userId)
		{
			new UserService().SetDisabled(userId);

			return Ok(true);
		}
	}
}