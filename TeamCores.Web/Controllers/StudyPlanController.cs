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
		/// ���ѧϰ�ƻ�
		/// </summary>
		/// <returns></returns>
		[UserAuthorization(RequiredPermissions = "P02")]
		public IActionResult Add()
		{
			return View();
		}
    }
}