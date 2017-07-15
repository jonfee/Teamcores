using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TeamCores.Models;

namespace TeamCores.ExceptionHandler
{
	/// <summary>
	/// 通用异常时的处理
	/// </summary>
	internal class NormalExceptionHandler : Handler
	{
		private HttpContext httpContext;

		private Exception exception;

		public NormalExceptionHandler(HttpContext context, Exception exception)
		{
			this.httpContext = context;
			this.exception = exception;
		}

		protected override JsonModel<object> GetErrorData()
		{
			// 输出结果
			var result = new JsonModel<object>();

			result.Code = $"SYSTEM_ERROR";
			result.Message = exception.Message;
			result.Data = null;

			return result;
		}

		protected override async Task ResponseWriteAsync(string responseContent)
		{
			//输出内容
			var response = httpContext.Response;

			//状态码
			if (exception is UnauthorizedAccessException)
			{
				response.StatusCode = (int)HttpStatusCode.Unauthorized;
			}
			else if (exception is Exception)
			{
				response.StatusCode = (int)HttpStatusCode.BadRequest;
			}

			response.ContentType = "application/json;charset=utf-8";

			await response.WriteAsync(responseContent).ConfigureAwait(false);
		}

		protected override void WriteLog()
		{
			//暂未实现
		}
	}
}
