using Microsoft.AspNetCore.Mvc;
using TeamCores.Common.Json;
using TeamCores.Misc.Filters;
using TeamCores.Models;

namespace TeamCores.Misc.Controller
{
	/// <summary>
	/// Controller的自定义基类
	/// </summary>
	public class BaseController : ControllerBase
	{
		/// <summary>
		/// 无权限请求结果
		/// </summary>
		/// <returns></returns>
		public OkObjectResult NoAccess()
		{
			return Ok(new JsonModel<string>
			{
				Code = "NO_ACCESS",
				Message = "没有当前请求的访问权限",
				Data = null
			});
		}

		/// <summary>
		/// 未登录或登录超时请求结果
		/// </summary>
		/// <returns></returns>
		public OkObjectResult NoLogin()
		{
			return Ok(new JsonModel<string>
			{
				Code = "LOGIN_TIMEROUT",
				Message = "未登录或登录超时",
				Data = null
			});
		}

		/// <summary>
		/// Creates an Microsoft.AspNetCore.Mvc.OkObjectResult object that produces an OK (200) response.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="data"></param>
		/// <returns></returns>
		public OkObjectResult Ok<T>(T data)
		{
			var jsonModel = new JsonModel<T>()
			{
				Data = data
			};

			return Ok<T>(jsonModel);
		}

		/// <summary>
		/// Creates an Microsoft.AspNetCore.Mvc.OkObjectResult object that produces an OK (200) response.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="code"></param>
		/// <param name="message"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public OkObjectResult Ok<T>(string code, string message, T data)
		{
			var jsonModel = new JsonModel<T>()
			{
				Code = code,
				Message = message,
				Data = data
			};

			return Ok<T>(jsonModel);
		}

		/// <summary>
		/// Creates an Microsoft.AspNetCore.Mvc.OkObjectResult object that produces an OK (200) response.
		/// </summary>
		/// <param name="success"></param>
		/// <returns></returns>
		public OkObjectResult Ok(bool success)
		{
			var jsonModel = new JsonModel<bool>()
			{
				Code = success ? "" : "FAILURE",
				Message = success ? "SUCCESS" : "FAILURE",
				Data = success
			};

			return Ok(jsonModel);
		}

		/// <summary>
		/// Creates an Microsoft.AspNetCore.Mvc.OkObjectResult object that produces an OK (200) response.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="data"></param>
		/// <returns></returns>
		public OkObjectResult Ok<T>(JsonModel<T> data)
		{
			string result = JsonUtility.JsonSerializeObject(data);

			return base.Ok(result);
		}
	}
}
