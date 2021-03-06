﻿using TeamCores.Domain.Enums;
using TeamCores.Models.Answer;

namespace TeamCores.Web.ViewModel.Question
{
    /// <summary>
    /// 新题目视图模型
    /// </summary>
    public class NewQuestionViewModel
    {
        /// <summary>
        /// 归属课程ID
        /// </summary>
        public long CourseId { get; set; }

        /// <summary>
        /// 题目类型类型（单选，多选，对错，填空题，问答题）
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 题目
        /// </summary>
        public string Topic { get; set; }

        /// <summary>
        /// AnswerOptionsJson
        /// </summary>
        public string AnswerOptionsJson
        {
            get;
            set;
        }

        /// <summary>
        /// 原始答案备选项
        /// </summary>
        public QuestionAnswer AnswerOptions
        {
            get
            {
                try
                {
                    switch (Type)
                    {
                        case (int)QuestionType.SINGLE_CHOICE:
                            return Newtonsoft.Json.JsonConvert.DeserializeObject<SingleChoiceAnswer>(AnswerOptionsJson);
                        case (int)QuestionType.MULTIPLE_CHOICE:
                            return Newtonsoft.Json.JsonConvert.DeserializeObject<MultipleChoiceAnswer>(AnswerOptionsJson);
                        case (int)QuestionType.ESSAY_QUESTION:
                            return Newtonsoft.Json.JsonConvert.DeserializeObject<EssayQuestionAnswer>(AnswerOptionsJson);
                        case (int)QuestionType.GAP_FILLING:
                            return Newtonsoft.Json.JsonConvert.DeserializeObject<GapFillingAnswer>(AnswerOptionsJson);
                        case (int)QuestionType.TRUE_OR_FALSE:
                            return Newtonsoft.Json.JsonConvert.DeserializeObject<TrueFalseAnswer>(AnswerOptionsJson);
                        default:
                            break;
                    }
                }
                catch (System.Exception)
                {
                    return null;
                }
                return null;
            }
        }
    }
}
