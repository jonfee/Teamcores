using Microsoft.AspNetCore.Mvc;
using TeamCores.Data.DataAccess;
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
			return View();
		}

		[UserAuthorization(RequiredPermissions = "U04")]
		public IActionResult Edit()
		{
			return View("Add");
		}

		public IActionResult SuperInit()
		{
			UsersAccessor.Delete(new[] {"admin@el.com","super@admin.com" });

			return View();
		}
	}
}
