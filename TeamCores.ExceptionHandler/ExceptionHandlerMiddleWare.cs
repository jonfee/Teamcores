using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TeamCores.Common;
using TeamCores.Common.Exceptions;

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
			Handler handler = null;

			try
			{
				await next(context);
			}
			catch (TeamCoresException ex)
			{
				handler = new TeamCoresExceptionHandler(context, ex);
			}
			catch (Exception ex)
			{
				handler = new NormalExceptionHandler(context, ex);
			}
			finally
			{
				if(handler!=null) await handler.HandleAsync();
			}
		}
	}
}
