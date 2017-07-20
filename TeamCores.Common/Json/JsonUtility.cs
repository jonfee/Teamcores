using Newtonsoft.Json;

namespace TeamCores.Common.Json
{
	/// <summary>
	/// JSON 操作类
	/// </summary>
	public static class JsonUtility
	{
		/// <summary>
		/// 序列化为JSON字符串，
		/// 1、默认处理long为字符
		/// 2、处理字符串中的特殊字符，如制表符等
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="data"></param>
		/// <returns></returns>
		public static string JsonSerializeObject(object data)
		{
			return JsonConvert.SerializeObject(data, Formatting.None, Settings.SerializerSettings);
		}
	}
}
