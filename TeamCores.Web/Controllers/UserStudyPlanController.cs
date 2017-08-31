using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TeamCores.Web.Controllers
{
    public class UserStudyPlanController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}