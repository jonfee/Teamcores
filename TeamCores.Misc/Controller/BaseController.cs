using Microsoft.AspNetCore.Mvc;
using TeamCores.Common.Json;
using TeamCores.Models;

namespace TeamCores.Misc.Controller
{
	/// <summary>
	/// Controller的自定义基类
	/// </summary>
	public class BaseController : ControllerBase
	{
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
