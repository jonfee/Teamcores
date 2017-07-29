using System;
using System.ComponentModel;
using TeamCores.Common;
using TeamCores.Data.DataAccess;
using TeamCores.Data.Entity;
using TeamCores.Domain.Enums;

namespace TeamCores.Domain.Models.Subject
{
    /// <summary>
    /// 新科目验证规则错误结果
    /// </summary>
    internal enum NewSubjectFailureRule
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

    internal class NewSubject : EntityBase<long, NewSubjectFailureRule>
    {
        #region 属性

        /// <summary>
        /// 学科名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 课程数量
        /// </summary>
        public int Count => 0;

        /// <summary>
        /// 科目状态
        /// </summary>
        public int Status => (int)SubjectStatus.ENABLED;

        #endregion

        public NewSubject()
        {
            this.ID = IDProvider.NewId;
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

        public bool Save()
        {
            ThrowExceptionIfValidateFailure();

            //数据仓储对象
            Subjects subject = new Subjects
            {
                Count = Count,
                CreateTime = DateTime.Now,
                Name = Name,
                Status = Status,
                SubjectId = ID
            };

            //保存新科目到仓储
            return SubjectsAccessor.Insert(subject);
        }
    }
}
