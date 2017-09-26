using Microsoft.AspNetCore.Mvc;
using TeamCores.Misc.Filters;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TeamCores.Web.Controllers
{
	[UserAuthorization]
	public class MessageController : Controller
    {
		[UserAuthorization]
		public IActionResult Index()
        {
            return View();
        }

		[UserAuthorization]
		public IActionResult Details(long id)
		{
			return View();
		}
    }
}
