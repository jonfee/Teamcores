using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeamCores.Misc.Filters;

namespace TeamCores.Web.Controllers
{
    public class StudyPlanController : Controller
    {
		[UserAuthorization]
        public IActionResult Index()
        {
            return View();
        }

		/// <summary>
		/// 添加学习计划
		/// </summary>
		/// <returns></returns>
		[UserAuthorization(RequiredPermissions = "P02")]
		public IActionResult Add()
		{
			return View();
		}

		/// <summary>
		/// 查看学习计划详细信息
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[UserAuthorization(RequiredPermissions ="P01")]
		public IActionResult Details(long id)
		{
			return View();
		}
    }
}