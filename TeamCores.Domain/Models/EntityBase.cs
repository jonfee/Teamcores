using System;
using System.Collections.Generic;
using System.Linq;
using TeamCores.Common.Exceptions;
using TeamCores.Common.Utilities;

namespace TeamCores.Domain
{
	/// <summary>
	/// 领域基类
	/// </summary>
	/// <typeparam name="TID">领域对象ID数据类型</typeparam>
	/// <typeparam name="TRule">验证失败的规则结果枚举类型</typeparam>
	public abstract class EntityBase<TID, TRule>
	{
		#region 私有变量

		/// <summary>
		/// 校验失败的规则结果集合
		/// </summary>
		private List<TRule> brokenRules = new List<TRule>();

		#endregion

		#region 属性成员

		/// <summary>
		/// 领域对象ID
		/// </summary>
		public TID ID { get; set; }

		#endregion

		#region 公开方法

		/// <summary>
		/// 获取不符合的验证规则集合
		/// </summary>
		/// <returns></returns>
		public List<TRule> GetBrokenRules()
		{
			brokenRules.Clear();
			Validate();
			return brokenRules;
		}

		/// <summary>
		/// 检验领域数据且当有错误时抛出业务异常
		/// </summary>
		/// <param name="action"></param>
		public void ThrowExceptionIfValidateFailure(Action customChecking = null)
		{
			//校验领域对象是否存在错误的规则
			var brokenRules = GetBrokenRules();

			//存在自定义验证器，则执行
			customChecking?.Invoke();

			if (brokenRules.Count > 0)
			{
				var dicErrors = brokenRules.Select(p => p.GetEnumEntry()).ToDictionary(k => k.Name as object, v => v.Description as object);
				throw new TeamCoresException("ERROR", dicErrors);
			}
		}

		#endregion

		#region 派生类可用方法

		/// <summary>
		/// 添加检验失败规则到集合
		/// </summary>
		/// <param name="rule"></param>
		protected void AddBrokenRule(TRule rule)
		{
			brokenRules.Add(rule);
		}

		#endregion

		#region 抽象方法

		/// <summary>
		/// 规则验证
		/// </summary>
		protected abstract void Validate();

		#endregion
	}
}
