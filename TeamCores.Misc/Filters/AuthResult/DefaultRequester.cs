using Microsoft.AspNetCore.Mvc.Filters;

namespace TeamCores.Misc.Filters.AuthResult
{
	/// <summary>
	/// 默认访问请求者
	/// </summary>
	internal class DefaultRequester : Requester
	{
		public DefaultRequester(AuthorizationFilterContext context) : base(context)
		{

		}

		public override void Accept(AuthState state)
		{
			state.DefaultResponse(this);
		}
	}
}
