using System;
using System.Collections.Generic;
using System.Text;
using TeamCores.Common;

namespace TeamCores.Domain.Models.Exams
{
	/// <summary>
	/// 用户新考卷业务领域验证失败结果枚举
	/// </summary>
	internal enum NewUserExamsFailureRule
	{

	}

	/// <summary>
	/// 用户新考卷业务领域模型
	/// </summary>
	internal class NewUserExams : EntityBase<long, NewUserExamsFailureRule>
	{
		#region 属性

		/// <summary>
		/// 当前要考试的用户
		/// </summary>
		public long UserId { get; set; }

		/// <summary>
		/// 当前的考试模板卷
		/// </summary>
		public long ExamsId { get; set; }

		#endregion

		#region 构造函数

		/// <summary>
		/// 初始化一个<see cref="NewUserExams"/>对象实例
		/// </summary>
		/// <param name="userId">当前要考试的用户ID</param>
		/// <param name="examsId">当前的考试模板卷ID</param>
		public NewUserExams(long userId, long examsId)
		{
			ID= IDProvider.NewId;
			UserId = UserId;
			ExamsId = examsId;
		}

		#endregion

		#region 验证

		protected override void Validate()
		{
			throw new NotImplementedException();
		}

		#endregion

		#region 操作方法



		#endregion
	}
}
