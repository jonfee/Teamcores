using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TeamCores.Web.Controllers
{
    public class ExamsController : Controller
    {
        // GET: /<controller>/
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

		public IActionResult TestList()
		{
			return View();
		}

		public IActionResult Test(long id)
		{
			return View();
		}

        public IActionResult MyTestList()
        {
            return View();
        }

        /// <summary>
        /// ������������
        /// </summary>
        /// <param name="id">�������Ծ�ID</param>
        /// <returns></returns>
        public IActionResult TestDetails(long id)
        {
            return View();
        }
    }
}
