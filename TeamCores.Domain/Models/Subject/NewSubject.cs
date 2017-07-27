using System.ComponentModel;
using TeamCores.Common;
using TeamCores.Data.DataAccess;
using TeamCores.Domain.Enums;

namespace TeamCores.Domain.Models.Subject
{
	/// <summary>
	/// 新科目验证规则错误结果
	/// </summary>
	public enum NewSubjectFailureRule
    {
		/// <summary>
		/// 科目名称不能为空
		/// </summary>
		[Description("科目名称不能为空")]
		NAME_REQUIRE = 1,
		/// <summary>
		/// 科目名称已存在
		/// </summary>
		[Description("科目名称已存在")]
		NAME_EXISTS,
	}

    public class NewSubject : EntityBase<long, NewSubjectFailureRule>
    {
        #region 属性

        /// <summary>
        /// 学科名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 课程数量
        /// </summary>
        public readonly int Count;

        /// <summary>
        /// 科目状态
        /// </summary>
        public readonly int Status;

        #endregion

        public NewSubject()
        {
            this.ID = IDProvider.NewId;
            Status = (int)SubjectStatus.ENABLED;
            Count = 0;
        }

		/// <summary>
		/// 名称是否可以使用
		/// </summary>
		/// <returns></returns>
		public bool CanUseForName()
		{
			bool isExists = SubjectsAccessor.NameExists(Name);

			return !isExists;
		}

        protected override void Validate()
        {
			//科目名为空
			if (string.IsNullOrWhiteSpace(Name)) this.AddBrokenRule(NewSubjectFailureRule.NAME_REQUIRE);

			//科目是否可以使用
			if (!CanUseForName()) this.AddBrokenRule(NewSubjectFailureRule.NAME_EXISTS);
        }
    }
}
