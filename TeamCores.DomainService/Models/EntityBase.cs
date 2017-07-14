using System.Collections.Generic;

namespace TeamCores.DomainService
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

		public List<TRule> GetBrokenRules()
		{
			brokenRules.Clear();
			Validate();
			return brokenRules;
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
