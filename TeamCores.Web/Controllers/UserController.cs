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
			return View();
		}

		[UserAuthorization(RequiredPermissions = "U04")]
		public IActionResult Edit()
		{
			return View();
		}

		/// <summary>
		/// 初始化超级用户账号 
		/// </summary>
		/// <returns></returns>
		public IActionResult SuperInit()
		{
			return View();
		}

        [UserAuthorization]
        public IActionResult Modify()
        {
            return View();
        }

        [UserAuthorization]
        public IActionResult ModifyPwd()
        {
            return View();
        }
	}
}
