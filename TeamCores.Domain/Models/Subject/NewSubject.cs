using System;
using System.Collections.Generic;
using System.Text;
using TeamCores.Common;
using TeamCores.Domain.Enums;

namespace TeamCores.Domain.Models.Subject
{
    public enum NewSubjectFailureRule
    {

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

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
