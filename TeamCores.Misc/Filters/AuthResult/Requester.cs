using Microsoft.AspNetCore.Mvc.Filters;

namespace TeamCores.Misc.Filters.AuthResult
{
	/// <summary>
	/// 访问请求者 
	/// </summary>
	internal abstract class Requester
	{
		public AuthorizationFilterContext FilterContext;

		public Requester(AuthorizationFilterContext context)
		{
			FilterContext = context;
		}

		/// <summary>
		/// 接收访问状态
		/// </summary>
		/// <param name="state"><see cref="AuthState"/>权限访问状态</param>
		public abstract void Accept(AuthState state);
	}
}
