using Microsoft.AspNetCore.Mvc;
using TeamCores.Misc.Filters;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TeamCores.Web.Controllers
{
	public class UserController : Controller
	{
		// GET: /<controller>/
		[UserAuthorization(RequiredPermissions = "U01")]
		public IActionResult Index()
		{
			return View();
		}

		/// <summary>
		/// 新增用户
		/// </summary>
		/// <returns></returns>
		[UserAuthorization(RequiredPermissions = "U02")]
		public IActionResult Add()
		{
			ViewData["Title"] = "新增用户";

			return View();
		}

		[UserAuthorization(RequiredPermissions = "U04")]
		public IActionResult Edit()
		{
			ViewData["Title"] = "编辑用户";

			return View("Add");
		}
	}
}
