using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamCores.Misc;

namespace TeamCores.Web.Component
{
    public class ModuleNavViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            ViewBag.Username = Utility.GetUserContext().Username;
            ViewBag.UserId = Utility.GetUserContext().UserId;
            return View();
        }
    }
}