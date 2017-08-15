using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using TeamCores.Common.Json;
using TeamCores.Models;

namespace TeamCores.Misc.Filters.AuthResult
{
	/// <summary>
	/// 无权限状态
	/// </summary>
	internal class NoAccessState : AuthState
	{
		public override void AjaxResponse(AjaxRequester requester)
		{
			var data = new JsonModel<string>
			{
				Code = "NO_ACCESS",
				Message = "权限不足",
				Data = null
			};

			string dataForJson = JsonUtility.JsonSerializeObject(data);

			requester.FilterContext.Result = new OkObjectResult(dataForJson);

		}

		public override void DefaultResponse(DefaultRequester requester)
		{
			//跳转到无权限提示页面
			requester.FilterContext.Result = new RedirectToRouteResult("default", new RouteValueDictionary(new { action = "noaccess" }));
		}
	}
}
