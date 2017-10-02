using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using TeamCores.Common.Json;
using TeamCores.Models;

namespace TeamCores.Misc.Filters.AuthResult
{
	/// <summary>
	/// 未登录状态
	/// </summary>
	internal class NoLoginState : AuthState
	{
		public override void AjaxResponse(AjaxRequester requester)
		{
			var data = new JsonModel<string>
			{
				Code = "LOGIN_TIMEROUT",
				Message = "未登录或登录超时",
				Data = @"{ LOGIN_TIMEROUT:""未登录或登录超时""}"
            };

			string dataForJson = JsonUtility.JsonSerializeObject(data);

			requester.FilterContext.Result = new OkObjectResult(dataForJson);
		}

		public override void DefaultResponse(DefaultRequester requester)
		{
			//跳转到登录页
			requester.FilterContext.Result = new RedirectToRouteResult("default", new RouteValueDictionary(new {controller="home", action = "login" }));
		}
	}
}
