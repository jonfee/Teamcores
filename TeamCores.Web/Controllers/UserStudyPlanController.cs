using Microsoft.AspNetCore.Mvc;
using TeamCores.Misc.Filters;

namespace TeamCores.Web.Controllers
{
    [Route("UserStudyPlan")]
    public class UserStudyPlanController : Controller
    {
        /// <summary>
        /// ѧԱѧϰ�ƻ�����
        /// </summary>
        /// <param name="id">ѧϰ�ƻ�ID</param>
        /// <returns></returns>
        [UserAuthorization]
        [Route("details/{id}")]
        public IActionResult Details(long id)
        {
            return View();
        }
    }
}