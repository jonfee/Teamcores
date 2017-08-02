using Microsoft.AspNetCore.Mvc;
using TeamCores.Domain.Models.User;
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
        [HttpPost]
        [Route("search")]
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

            var result = new UserService().Search(searcher.PageSize, searcher.PageIndex, searcher.Keyword,
                searcher.Status);

            return Ok(result);
        }

        [HttpPost]
        [Route("setenabled")]
        public IActionResult SetEnabled(long userId)
        {
            new UserService().SetEnabled(userId);

            return Ok(true);
        }

        [HttpPost]
        [Route("setdisabled")]
        public IActionResult SetDisabled(long userId)
        {
            new UserService().SetDisabled(userId);

            return Ok(true);
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add(NewUserViewModel user)
        {
            NewUserRequest request = new NewUserRequest
            {
                Email = user.Email,
                Mobile = user.Mobile,
                Name = user.Name,
                Password = user.Password,
                Title = user.Title,
                Username = user.Username
            };

            new UserService().AddUser(request);

            return Ok(true);
        }
    }
}