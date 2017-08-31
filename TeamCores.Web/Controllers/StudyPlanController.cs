using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeamCores.Misc.Filters;

namespace TeamCores.Web.Controllers
{
    [Route("studyplan")]
    public class StudyPlanController : Controller
    {
        [UserAuthorization]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 添加学习计划
        /// </summary>
        /// <returns></returns>
        [UserAuthorization(RequiredPermissions = "P02")]
        [Route("add")]
        public IActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// 查看学习计划详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [UserAuthorization(RequiredPermissions = "P01")]
        [Route("details/{id}")]
        public IActionResult Details(long id)
        {
            return View();
        }

        /// <summary>
        /// 学习计划下某学员的学习情况
        /// </summary>
        /// <param name="studentId">学员用户ID</param>
        /// <param name="planId">学习计划ID</param>
        /// <returns></returns>
        [UserAuthorization(RequiredPermissions = "P01")]
        [Route("student")]
        public IActionResult Student(long planId, long studentId)
        {
            return View();
        }
    }
}