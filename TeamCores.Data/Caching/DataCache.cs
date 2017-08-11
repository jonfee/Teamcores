using System.Collections;
using System.Collections.Generic;

namespace TeamCores.Data.Caching
{
	/// <summary>
	/// 数据缓存抽象基类
	/// </summary>
	public abstract class DataCache<T> where T : class, new()
	{
		protected static readonly object my_locker = new object();

		private Hashtable _data;
		/// <summary>
		/// 已缓存的数据
		/// </summary>
		protected Hashtable Data
		{
			get
			{
				if (_data == null)
				{
					lock (my_locker)
					{
						if (_data == null)
						{
							_data = Hashtable.Synchronized(new Hashtable());

							var initData = GetInitData();

							Set(initData);
						}
					}
				}

				return _data;
			}
		}

		/// <summary>
		/// 设置缓存数据
		/// </summary>
		private void Set(Dictionary<object, T> data)
		{
			data.Clear();

			foreach (var item in data)
			{
				data.Add(item.Key, item.Value);
			}
		}

		/// <summary>
		/// 缓存的数据总数
		/// </summary>
		/// <returns></returns>
		public int Count()
		{
			return Data.Count;
		}

		/// <summary>
		/// 获取一条数据
		/// </summary>
		/// <param name="Key"></param>
		/// <returns></returns>
		public T Get(object key)
		{
			return Data[key] as T;
		}

		/// <summary>
		/// 获取所有数据
		/// </summary>
		/// <returns></returns>
		public List<T> GetAll()
		{
			List<T> list = new List<T>();

			foreach (var item in Data.Values)
			{
				list.Add((T)item);
			}

			return list;
		}

		/// <summary>
		/// 移除缓存中所有元素
		/// </summary>
		public void Clear()
		{
			lock (my_locker)
			{
				_data.Clear();
			}
		}

		/// <summary>
		/// 获得需要缓存的数据
		/// </summary>
		/// <returns></returns>
		protected abstract Dictionary<object, T> GetInitData();
	}
}
