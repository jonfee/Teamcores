using Microsoft.AspNetCore.Mvc;
using TeamCores.Misc.Filters;

namespace TeamCores.Web.Controllers
{
	public class ChapterController : Controller
	{
		/// <summary>
		/// 课程章节管理
		/// </summary>
		/// <returns></returns>
		[UserAuthorization(RequiredPermissions = "C01")]
		public IActionResult Index()
	    {
	        return View();
	    }

		[UserAuthorization(RequiredPermissions = "C05")]
		public IActionResult Add()
	    {
	        return View();
	    }

		[UserAuthorization(RequiredPermissions = "C01")]
		public IActionResult Details(long id)
		{
			return View();
		}
	}
}