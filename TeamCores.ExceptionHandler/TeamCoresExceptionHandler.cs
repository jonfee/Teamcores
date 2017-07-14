using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TeamCores.Common;
using TeamCores.Models;

namespace TeamCores.ExceptionHandler
{
	/// <summary>
	/// TeamCoresException异常时的处理
	/// </summary>
	internal class TeamCoresExceptionHandler : Handler<Dictionary<object,object>>
	{
		private HttpContext httpContext;

		private TeamCoresException exception;

		public TeamCoresExceptionHandler(HttpContext context, TeamCoresException exception)
		{
			this.httpContext = context;
			this.exception = exception;
		}

		protected override JsonModel<Dictionary<object,object>> GetErrorData()
		{
			// 待输出错误对象
			var result = new JsonModel<Dictionary<object, object>>();
			//根据异常类型，处理输出结果
			result.Code = "BUSINISS_ERROR";
			//特别处理：业务异常的详细内容
			result.Data = exception.Errors;
			//错误消息
			result.Message = exception.Message;

			return result;
		}

		protected override async Task ResponseWriteAsync(string responseContent)
		{
			//输出内容
			var response = httpContext.Response;
			response.ContentType = "application/json;charset=utf-8";

			await response.WriteAsync(responseContent).ConfigureAwait(false);
		}

		protected override void WriteLog()
		{
			//暂未实现
		}
	}
}
