using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamCores.Domain.Services;
using TeamCores.Domain.Services.Request;
using TeamCores.Misc;
using TeamCores.Misc.Filters;
using TeamCores.Models;
using TeamCores.Models.Enum;
using TeamCores.Web.ViewModel;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TeamCores.Web.Controllers
{
	[UserAuthorization]
	public class HomeController : Controller
	{
		// GET: /<controller>/
		[AllowAnonymous]
		public IActionResult Index()
		{
			return View();
		}

		[AllowAnonymous]
		public IActionResult Login()
		{
			//NewUserRequest user = new NewUserRequest
			//{
			//	Email = "admin@el.com",
			//	Mobile = "18866669999",
			//	Username = "admin",
			//	Password = "123456",
			//	Name = "系统管理员",
			//	Title = "系统管理员",
			//	IgnorePermission = true
			//};

			//new UserService().AddUser(user);

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

		/// <summary>
		/// 退出登录
		/// </summary>
		/// <returns></returns>
		[UserAuthorization]
		public IActionResult SignOut()
		{
			Utility.GetUserContext().Logout();

			return RedirectToAction("Login");
		}
	}
}
