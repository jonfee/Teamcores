using System;
using System.Collections.Generic;
using System.Text;
using TeamCores.Domain.Enums;
using TeamCores.Domain.Services.Request;

namespace TeamCores.Domain.Models.Exams
{
	/// <summary>
	/// 新试卷模板领域业务验证错误结果枚举
	/// </summary>
	internal enum NewExamsFailureRule
	{
		REQUEST_OBJECT_IS_NULL=1,
		TITLE_CANNOT_EMPTY,
		QUESTIONS_CANNOT_EMPTY
	}

	/// <summary>
	/// 新试卷模板业务领域模型
	/// </summary>
	internal class NewExams : EntityBase<long, NewExamsFailureRule>
	{
		#region 属性

		/// <summary>
		/// 新考卷请求对象
		/// </summary>
		public NewExams Request { get; set; }
		
		/// <summary>
		/// 考卷状态
		/// </summary>
		public int Status => (int)ExamsStaus.ENABLED;

		#endregion

		#region 构造函数

		public NewExams(NewExamsRequest request)
		{

		}

		#endregion

		#region 验证

		protected override void Validate()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
