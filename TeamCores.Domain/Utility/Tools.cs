using System;
using System.Collections.Generic;
using System.Text;

namespace TeamCores.Domain.Utility
{
	/// <summary>
	/// 工具类
	/// </summary>
	public static class Tools
	{
		/// <summary>
		/// 将长整型集合的字符串数据转换成集合类型
		/// </summary>
		/// <param name="strIds">一个或用指定分隔符分隔的多个ID字符串，如："100,200,300"</param>
		/// <param name="separator">分隔符</param>
		/// <returns></returns>
		public static IEnumerable<long> TransferToLongArray(string strIds, char separator = ',')
		{
			if (string.IsNullOrWhiteSpace(strIds)) yield break;

			var arr = strIds.Split(separator);

			long tempId = 0;

			foreach (var str in arr)
			{
				if (long.TryParse(str, out tempId))
				{
					yield return tempId;
				}
			}
		}
	}
}
