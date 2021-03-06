﻿using System.ComponentModel;

namespace TeamCores.Domain.Enums
{
    /// <summary>
    /// 题目类型枚举
    /// </summary>
    public enum QuestionType
    {
        /// <summary>
        /// 单选题
        /// </summary>
        [Description("单选题")]
        SINGLE_CHOICE = 1,
        /// <summary>
        /// 多选题
        /// </summary>
        [Description("多选题")]
        MULTIPLE_CHOICE = 2,
        /// <summary>
        /// 判断题
        /// </summary>
        [Description("判断题")]
        TRUE_OR_FALSE = 3,
        /// <summary>
        /// 填空题
        /// </summary>
        [Description("填空题")]
        GAP_FILLING = 4,
        /// <summary>
        /// 问答题
        /// </summary>
        [Description("问答题")]
        ESSAY_QUESTION = 5
    }
}
