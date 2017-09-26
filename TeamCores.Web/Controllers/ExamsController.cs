using Microsoft.AspNetCore.Mvc;
using TeamCores.Misc.Filters;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TeamCores.Web.Controllers
{
	public class ExamsController : Controller
	{
		/// <summary>
		/// 考卷模板管理(列表)
		/// </summary>
		/// <param name="p">当前页</param>
		/// <returns></returns>
		[UserAuthorization(RequiredPermissions = "E01")]
		public IActionResult Index(int p)
		{
			return View();
		}

		/// <summary>
		/// 新增考卷模板
		/// </summary>
		/// <returns></returns>
		[UserAuthorization(RequiredPermissions = "E02")]
		public IActionResult Add()
		{
			return View();
		}

		/// <summary>
		/// 考卷模板详情
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[UserAuthorization(RequiredPermissions = "E01")]
		public IActionResult Details(long id)
		{
			return View();
		}

		/// <summary>
		/// 考卷中心
		/// </summary>
		/// <param name="p">当前页</param>
		/// <returns></returns>
		[UserAuthorization]
		public IActionResult TestList(int p)
		{
			return View();
		}

		/// <summary>
		/// 开始考试/练习
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[UserAuthorization]
		public IActionResult Test(long id)
		{
			return View();
		}

		/// <summary>
		/// 我的考卷列表
		/// </summary>
		/// <param name="p">当前页</param>
		/// <returns></returns>
		[UserAuthorization]
		public IActionResult MyTestList(int p)
		{
			return View();
		}

		/// <summary>
		/// 考生考卷详情
		/// </summary>
		/// <param name="id">考生考试卷ID</param>
		/// <returns></returns>
		[UserAuthorization]
		public IActionResult TestDetails(long id)
		{
			return View();
		}

		/// <summary>
		/// 阅卷中心
		/// </summary>
		/// <param name="status">阅卷状态</param>
		/// <param name="p">当前页</param>
		/// <returns></returns>
		[UserAuthorization(RequiredPermissions = "T01")]
		public IActionResult ReviewCenter(int status, int p)
		{
			return View();
		}

		/// <summary>
		/// 开始阅卷
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[UserAuthorization(RequiredPermissions = "T01")]
		public IActionResult Review(long id)
		{
			return View();
		}
	}
}
