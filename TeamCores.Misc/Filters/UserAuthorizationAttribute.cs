using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using TeamCores.Misc.Filters.AuthResult;

namespace TeamCores.Misc.Filters
{
	public class UserAuthorizationAttribute : Attribute, IAuthorizationFilter
	{
		/// <summary>
		/// 当前操作所需的权限，多个权限用英文逗号分隔（如："A01,A02" )
		/// </summary>
		public string RequiredPermissions { get; set; }

		public void OnAuthorization(AuthorizationFilterContext context)
		{
			if (!HasAllowAnonymous(context))
			{
				//获取登录用户信息
				var user = GetLoginUser(context);

				//请求对象
				Requester requester = GetRequester(context);

				//请求者权限访问状态
				AuthState state = null;
				if (user.IsGuest)
				{
					state = new NoLoginState();
				}
				else if (!PermissionCheck(user.PermissionCode))
				{
					state = new NoAccessState();
				}

				//如果存在授权错误状态，则接收并处理
				if (state != null) requester.Accept(state);
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

		/// <summary>
		/// 获取登录用户信息
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		private UserContext GetLoginUser(AuthorizationFilterContext context)
		{
			//获取登录用户信息
			return UserContext.Standby(context.HttpContext);
		}

		/// <summary>
		/// 获取请求对象
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		private Requester GetRequester(AuthorizationFilterContext context)
		{
			//是否为ajax请求
			bool isAjax = IsAjaxRequest(context.HttpContext);

			//请求者
			Requester requester = null;
			if (isAjax)
			{
				requester = new AjaxRequester(context);
			}
			else
			{
				requester = new DefaultRequester(context);
			}

			return requester;
		}

		/// <summary>
		/// 是否为异步请求
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		private bool IsAjaxRequest(HttpContext context)
		{
			if (context == null) return false;

			return context.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
		}

		/// <summary>
		/// 验证权限
		/// </summary>
		/// <param name="userPermissionCode">当前登录用户的权限编号序列</param>
		/// <returns></returns>
		private bool PermissionCheck(string userPermissionCode)
		{
			//当操作不需要任何权限时
			if (string.IsNullOrWhiteSpace(RequiredPermissions)) return true;

			//无权限设置，表示超级用户，不权限限制
			if (string.IsNullOrWhiteSpace(userPermissionCode)) return true;

			bool success = false;

			//将权限编号序列转换为全大写的编号数组
			string[] needCodes = RequiredPermissions.ToUpper().Split(new[] { ',', '|' }, StringSplitOptions.RemoveEmptyEntries);

			foreach (var code in needCodes)
			{
				if (userPermissionCode.Contains(code))
				{
					success = true;
					break;
				}
			}

			return success;
		}
	}
}
