using System.Threading.Tasks;
using Newtonsoft.Json;
using TeamCores.Common.Json;
using TeamCores.Models;

namespace TeamCores.ExceptionHandler
{
	/// <summary>
	/// 处理类抽象基类
	/// </summary>
	internal abstract class Handler
	{
		/// <summary>
		/// 写错误日志
		/// </summary>
		protected abstract void WriteLog();

		/// <summary>
		/// 获取错误数据
		/// </summary>
		/// <returns></returns>
		protected abstract JsonModel<object> GetErrorData();

		/// <summary>
		/// Response输出
		/// </summary>
		/// <param name="responseContent"></param>
		/// <returns></returns>
		protected abstract Task ResponseWriteAsync(string responseContent);

		/// <summary>
		/// 处理异常
		/// </summary>
		/// <returns></returns>
		public async Task HandleAsync()
		{
			//错误日志记录
			WriteLog();

			// 待输出错误对象
			var result = GetErrorData();

			//输出内容
			var content = JsonUtility.JsonSerializeObject(result);

			//输出到页面
			await ResponseWriteAsync(content);
		}
	}
}
