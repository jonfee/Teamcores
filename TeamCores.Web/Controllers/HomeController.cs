using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeamCores.Web.ViewModel;
using TeamCores.Misc.Filters;
using Microsoft.AspNetCore.Authorization;
using TeamCores.Models.Enum;
using TeamCores.Misc;
using TeamCores.Models;
using TeamCores.Common;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TeamCores.Web.Controllers
{
    [UserAuthorization]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(LoginViewModel model)
        {
            var data = new JsonModel<string>();
            if (!ModelState.IsValid)
            {
                data.Code = "Verification failed!";
                foreach (var item in ModelState.Values)
                {
                    if (item.Errors.Count > 0)
                    {
                        data.Message = item.Errors[0].ErrorMessage;
                        break;
                    }
                }
                return Json(data);
            }
            var result = Utility.GetUserContext().UserLogin(model.Username, model.Password, 7);

            if (result == EnumLoginState.AccountError)
            {
                data.Code = "Account error!";
                data.Message = "账号信息异常！";
                return Json(data);
            }
            else if (result == EnumLoginState.PasswordError)
            {
                data.Code = "Account error!";
                data.Message = "账号信息错误！";
                return Json(data);
            }
            data.Data = Url.RouteUrl("default", new { action = "index" });
            return Json(data);
        }
    }
}
