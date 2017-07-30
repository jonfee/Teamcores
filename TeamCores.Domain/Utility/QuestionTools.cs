using System;
using System.Collections.Generic;
using System.Text;
using TeamCores.Domain.Enums;

namespace TeamCores.Domain.Utility
{
    /// <summary>
    /// 题目辅助工具类
    /// </summary>
    public class QuestionTools
    {
        /// <summary>
        /// 检测指定的目类型是否需要人工阅卷
        /// </summary>
        /// <param name="type"><see cref="QuestionType" />枚举成员值</param>
        /// <returns></returns>
        public static bool HasMarking(int type)
        {
            QuestionType questionType = (QuestionType)Enum.Parse(typeof(QuestionType), type.ToString());

            switch (questionType)
            {
                case QuestionType.SINGLE_CHOICE:
                case QuestionType.MULTIPLE_CHOICE:
                case QuestionType.TRUE_OR_FALSE:
                    return false;
                case QuestionType.GAP_FILLING:
                case QuestionType.ESSAY_QUESTION:
                default:
                    return true;
            }
        }
    }
}
