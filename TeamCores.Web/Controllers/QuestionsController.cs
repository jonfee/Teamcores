using Microsoft.AspNetCore.Mvc;
using TeamCores.Misc.Filters;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TeamCores.Web.Controllers
{
    public class QuestionsController : Controller
    {
		[UserAuthorization(RequiredPermissions = "Q01")]
		public IActionResult Index()
        {
            return View();
        }

		/// <summary>
		/// 新增考题
		/// </summary>
		/// <returns> </returns>
		[UserAuthorization(RequiredPermissions = "Q02")]
		public IActionResult Add()
        {
            return View();
        }

		/// <summary>
		/// 编辑考题
		/// </summary>
		/// <returns> </returns>
		[UserAuthorization(RequiredPermissions = "Q04")]
		public IActionResult Edit()
        {
            return View();
        }
    }
}