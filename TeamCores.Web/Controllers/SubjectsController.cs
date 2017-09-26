using Microsoft.AspNetCore.Mvc;
using TeamCores.Misc.Filters;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TeamCores.Web.Controllers
{
    public class SubjectsController : Controller
    {
		[UserAuthorization(RequiredPermissions = "K99")]
		public IActionResult Index()
        {
            return View();
        }
    }
}