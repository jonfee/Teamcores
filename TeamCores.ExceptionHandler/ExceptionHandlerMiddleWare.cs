using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TeamCores.Common;

namespace TeamCores.ExceptionHandler
{
	/// <summary>
	/// 全局异常处理程序
	/// </summary>
	public class ExceptionHandlerMiddleWare
	{
		private readonly RequestDelegate next;

		public ExceptionHandlerMiddleWare(RequestDelegate next)
		{
			this.next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await next(context);
			}
			catch (TeamCoresException ex)
			{
				Handler<Dictionary<object, object>> handler = new TeamCoresExceptionHandler(context, ex);
				await handler.HandleAsync();
			}
			catch (Exception ex)
			{
				Handler<string> handler = new NormalExceptionHandler(context, ex);
				await handler.HandleAsync();
			}
		}
	}
}
