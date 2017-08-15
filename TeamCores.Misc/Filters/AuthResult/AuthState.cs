namespace TeamCores.Misc.Filters.AuthResult
{
	/// <summary>
	/// 权限控制状态
	/// </summary>
	internal abstract class AuthState
	{
		/// <summary>
		/// 默认请求输出
		/// </summary>
		/// <param name="requester"></param>
		public abstract void DefaultResponse(DefaultRequester requester);

		/// <summary>
		/// AJAX请求输出
		/// </summary>
		/// <param name="requester"></param>
		public abstract void AjaxResponse(AjaxRequester requester);
	}
}
