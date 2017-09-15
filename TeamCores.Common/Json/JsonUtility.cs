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
			if (data == null) return string.Empty;
			
			return JsonConvert.SerializeObject(data, Formatting.None, Settings.SerializerSettings);
		}

		/// <summary>
		/// 反序列化数据对象
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="data"></param>
		/// <returns></returns>
		public static T JsonDeserialize<T>(string data)
		{
			if (string.IsNullOrWhiteSpace(data)) return default(T);

			return JsonConvert.DeserializeObject<T>(data, Settings.SerializerSettings);
		}

		/// <summary>
		/// 反序列化为匿名类型数据对象
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="data"></param>
		/// <param name="anonymousTypeObject"></param>
		/// <returns></returns>
		public static T DeserializeAnonymousType<T>(string data, T anonymousTypeObject)
		{
			if (string.IsNullOrWhiteSpace(data)) return default(T);

			return JsonConvert.DeserializeAnonymousType(data, anonymousTypeObject);
		}
	}
}
