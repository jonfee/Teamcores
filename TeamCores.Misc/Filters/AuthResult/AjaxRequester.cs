using Microsoft.AspNetCore.Mvc.Filters;

namespace TeamCores.Misc.Filters.AuthResult
{
	/// <summary>
	/// AJAX访问请求者
	/// </summary>
	internal class AjaxRequester : Requester
	{
		public AjaxRequester(AuthorizationFilterContext context) : base(context)
		{

		}

		public override void Accept(AuthState state)
		{
			state.AjaxResponse(this);
		}
	}
}
