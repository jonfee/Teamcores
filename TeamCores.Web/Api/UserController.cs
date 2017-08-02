using Microsoft.AspNetCore.Mvc;

using TeamCores.Domain.Models;
using TeamCores.Domain.Services;
using TeamCores.Misc.Controller;
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
        public IActionResult Add(UserAddViewModel user)
        {
            new UserService().AddUser(new NewUser
            {
                Email = user.Email,
                Mobile = user.Mobile,
                Password = user.Password,
                Title = user.Title,
                Username = user.Username,
                Name = user.Name
            });

            return Ok(true);
        }

        [HttpPost]
        [Route("edit")]
        public IActionResult Edit(UserAddViewModel user)
        {
            new UserService().ModifyFor(user.UserId, user.Username, user.Email, user.Mobile, user.Title, user.Name);

            return Ok(true);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name = "userId" > </param>
        /// <returns> </returns>
        [HttpPost]
        [Route("{userId:long}")]
        public IActionResult GetUser(long userId)
        {
            var user = new UserService().GetUserAccount(userId);

            return Ok(user);
        }
    }
}