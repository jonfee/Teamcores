using Microsoft.AspNetCore.Mvc;
using TeamCores.Domain.Services;
using TeamCores.Domain.Services.Request;
using TeamCores.Misc;
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
			var result = service.Search(searcher.PageSize, searcher.PageIndex, searcher.Keyword,
				searcher.Status);

			return Ok(result);
		}

		[HttpGet]
		[Route("listforstatus")]
		[UserAuthorization(RequiredPermissions = "U01")]
		public IActionResult GetSimpleUsers(int? status)
		{
			var data = service.GetSimpleUsers(status);

			return Ok(data);
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
				Permissions = (user.Permissions ?? string.Empty).Split(new[] { ',', '|' }, System.StringSplitOptions.RemoveEmptyEntries),
				IgnorePermission = false
			};

			bool success = service.AddUser(request);

			return Ok(success);
		}

		[HttpPost]
		[Route("initsuper")]
		public IActionResult InitSuper(NewUserViewModel user)
		{
			NewUserRequest request = new NewUserRequest
			{
				Email = user.Email,
				Mobile = user.Mobile,
				Name = user.Name,
				Password = user.Password,
				Title = user.Title,
				Username = user.Username,
				IgnorePermission = false
			};

			bool success = service.InitSuperUser(request);

			return Ok(success);
		}

		[HttpPost]
		[Route("modifypwd")]
		[UserAuthorization(RequiredPermissions = "U04")]
		public IActionResult PasswordModifyTo(string oldWord, string newWord)
		{
            long userId = Utility.GetUserContext().UserId;

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
			string[] codes = (model.Permissions ?? string.Empty).Split(new[] { ',', '|' }, System.StringSplitOptions.RemoveEmptyEntries);

			bool success = service.ModifyFor(model.UserId,
				model.Username,
				model.Email,
				model.Mobile,
				model.Title,
				model.Name,
				codes);

			return Ok(success);
		}

        [HttpPost]
        [Route("modifyself")]
        [UserAuthorization]
        public IActionResult ModifySelfTo(UserModifySelfViewModel model)
        {
            var userId = Utility.GetUserContext().UserId;

            bool success = service.ModifySelfFor(userId,
                model.Username,
                model.Email,
                model.Mobile,
                model.Title,
                model.Name);

            return Ok(success);
        }

        [HttpGet]
		[Route("{id}")]
		[UserAuthorization(RequiredPermissions = "U01")]
		public IActionResult GetUser(long id)
		{
			var data = service.GetUserAccount(id);

			return Ok(data);
		}

        [HttpGet]
        [Route("me")]
        [UserAuthorization]
        public IActionResult GetMe()
        {
            var userId = Utility.GetUserContext().UserId;

            var data = service.GetUserAccount(userId);

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

        [HttpGet]
        [Route("statistics")]
        [UserAuthorization]
        public IActionResult GetStatisticalReports()
        {
            long userId = Utility.GetUserContext().UserId;

            var data = service.GetUserStatisticalReports(userId);

            return Ok(data);
        }

    }
}