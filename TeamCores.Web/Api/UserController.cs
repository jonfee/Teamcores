using Microsoft.AspNetCore.Mvc;
using TeamCores.Domain.Services;
using TeamCores.Domain.Services.Request;
using TeamCores.Misc.Controller;
using TeamCores.Misc.Filters;
using TeamCores.Web.ViewModel.User;

namespace TeamCores.Web.Api
{
	[Route("api/User")]
	public class UserController : BaseController
	{
		UserService service = null;

		public UserController()
		{
			service = new UserService();
		}

		[HttpPost]
		[Route("search")]
		[UserAuthorization(RequiredPermissions = "U01")]
		public IActionResult Search(UserSearcherViewModel searcher)
		{
			if (searcher == null)
			{
				searcher = new UserSearcherViewModel
				{
					PageIndex = 1,
					PageSize = 10
				};
			}

			var result = service.Search(searcher.PageSize, searcher.PageIndex, searcher.Keyword,
				searcher.Status);

			return Ok(result);
		}

		[HttpPost]
		[Route("setenabled")]
		[UserAuthorization(RequiredPermissions = "U04")]
		public IActionResult SetEnabled(long userId)
		{
			bool success = service.SetEnabled(userId);

			return Ok(success);
		}

		[HttpPost]
		[Route("setdisabled")]
		[UserAuthorization(RequiredPermissions = "U04")]
		public IActionResult SetDisabled(long userId)
		{
			bool success = service.SetDisabled(userId);

			return Ok(success);
		}

		[HttpPost]
		[Route("add")]
		[UserAuthorization(RequiredPermissions = "U02")]
		public IActionResult Add(NewUserViewModel user)
		{
			NewUserRequest request = new NewUserRequest
			{
				Email = user.Email,
				Mobile = user.Mobile,
				Name = user.Name,
				Password = user.Password,
				Title = user.Title,
				Username = user.Username,
				Permissions = user.Permissions,
				IgnorePermission = false
			};

			bool success = service.AddUser(request);

			return Ok(success);
		}

		[HttpPost]
		[Route("modifypwd")]
		[UserAuthorization(RequiredPermissions = "U04")]
		public IActionResult PasswordModifyTo(long userId, string oldWord, string newWord)
		{
			bool success = service.ModifyPassword(userId, oldWord, newWord);

			return Ok(success);
		}

		[HttpPost]
		[Route("resetpwd")]
		[UserAuthorization(RequiredPermissions = "U05")]
		public IActionResult ResetPassword(long userId, string newWord)
		{
			bool success = service.ResetPassword(userId, newWord);

			return Ok(success);
		}

		[HttpPost]
		[Route("modify")]
		[UserAuthorization(RequiredPermissions = "U04")]
		public IActionResult ModifyTo(UserModifyViewModel model)
		{
			bool success = service.ModifyFor(model.UserId,
				model.Username,
				model.Email,
				model.Mobile,
				model.Title,
				model.Name,
				model.Permissions);

			return Ok(success);
		}

		[HttpPost]
		[Route("get")]
		[UserAuthorization(RequiredPermissions = "U01")]
		public IActionResult GetUser(long id)
		{
			var data = service.GetUserAccount(id);

			return Ok(data);
		}

		[HttpGet]
		[Route("permissions")]
		[UserAuthorization(RequiredPermissions = "U02,U04")]
		public IActionResult GetAllPermissions()
		{
			var data = new PermissionService().GetPermissionsGroupByModule();

			return Ok(data);
		}
	}
}