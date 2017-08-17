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
			var user = Utility.GetUserContext();

			ViewBag.Username = user.Username;
            ViewBag.UserId = user.UserId;
			ViewBag.UserEmail = user.Email;

			return View();
        }
    }
}