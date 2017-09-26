using Microsoft.AspNetCore.Mvc;
using TeamCores.Misc.Filters;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TeamCores.Web.Controllers
{
	public class CourseController : Controller
    {
		[UserAuthorization(RequiredPermissions = "C01")]
		public IActionResult Index()
        {
            return View();
        }

		[UserAuthorization(RequiredPermissions = "C01")]
		public IActionResult Details(long id)
        {
            return View();
        }

		[UserAuthorization(RequiredPermissions = "C02")]
		public IActionResult Add()
        {
            return View();
        }

		[UserAuthorization(RequiredPermissions = "C03")]
		public IActionResult Edit()
        {
            return View();
        }
    }
}
