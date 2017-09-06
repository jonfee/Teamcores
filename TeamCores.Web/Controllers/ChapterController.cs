using Microsoft.AspNetCore.Mvc;

namespace TeamCores.Web.Controllers
{
	public class ChapterController : Controller
	{
        /// <summary>
        /// 课程章节管理
        /// </summary>
        /// <returns></returns>
	    public IActionResult Index()
	    {
	        return View();
	    }

	    public IActionResult Add()
	    {
	        return View();
	    }

		public IActionResult Details(long id)
		{
			return View();
		}
	}
}