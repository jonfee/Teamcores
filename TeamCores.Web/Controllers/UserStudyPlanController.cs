using Microsoft.AspNetCore.Mvc;
using TeamCores.Misc.Filters;

namespace TeamCores.Web.Controllers
{
    [Route("UserStudyPlan")]
    public class UserStudyPlanController : Controller
    {
        /// <summary>
        /// 学员学习计划详情
        /// </summary>
        /// <param name="id">学习计划ID</param>
        /// <returns></returns>
        [UserAuthorization]
        [Route("details/{id}")]
        public IActionResult Details(long id)
        {
            return View();
        }
    }
}