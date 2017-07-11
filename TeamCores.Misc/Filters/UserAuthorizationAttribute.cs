using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeamCores.Misc.Filters
{
    public class UserAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!HasAllowAnonymous(context))
            {
                var user = UserContext.Standby(context.HttpContext);
                if (user.IsGuest || !user.IsAdmin)
                {
                    context.Result = new RedirectToRouteResult("areashome", new RouteValueDictionary(new { action = "login" }));
                }
            }
        }

        private bool HasAllowAnonymous(AuthorizationFilterContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return context.Filters.Any(item => item is IAllowAnonymousFilter);
        }

    }
}
