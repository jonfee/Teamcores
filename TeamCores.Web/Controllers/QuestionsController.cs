﻿using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TeamCores.Web.Controllers
{
    public class QuestionsController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 新增考题
        /// </summary>
        /// <returns> </returns>
        public IActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// 编辑考题
        /// </summary>
        /// <returns> </returns>
        public IActionResult Edit()
        {
            return View();
        }
    }
}