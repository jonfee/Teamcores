using System;
using System.Collections;
using System.ComponentModel;
using TeamCores.Common;
using TeamCores.Data.DataAccess;
using TeamCores.Data.Entity;
using TeamCores.Domain.Enums;
using TeamCores.Models.Answer;
using TeamCores.Domain.Utility;
using System.Collections.Generic;

namespace TeamCores.Domain.Models.Question
{
	/// <summary>
	/// 新题目验证错误结果枚举
	/// </summary>
	internal enum NewQuestionFailureRule
	{
		/// <summary>
		/// 题目的创建用户不存在
		/// </summary>
		[Description("题目的创建用户不存在")]
		QUESTION_CREATOR_NOT_EXISTS = 1,
		/// <summary>
		/// 所属课程不存在
		/// </summary>
		[Description("所属课程不存在")]
		COURSE_NOT_EXISTS,
		/// <summary>
		/// 不是有效的题目类型
		/// </summary>
		[Description("不是有效的题目类型")]
		QUESTION_TYPE_ERROR,
		/// <summary>
		/// 答案项不能为空
		/// </summary>
		[Description("答案项不能为空")]
		ANSWER_OPTIONS_CANNOT_EMPTY,
		/// <summary>
		/// 题目类型与答案项不匹配
		/// </summary>
		[Description("题目类型与答案项不匹配")]
		QUESTION_TYPE_AND_ANSWER_OPTIONS_REGEX_FAILURE,
		/// <summary>
		/// 单选题的备选答案项必须是两个以上，且只能有一个正确答案
		/// </summary>
		[Description("单选题的备选答案项必须是两个以上，且只能有一个正确答案")]
		SINGLE_CHOICE_ANSWER_OPTIONS_ERROR,
		/// <summary>
		/// 多选题的备选答案项必须是两个以上，且有个两个（含）以上正确答案
		/// </summary>
		[Description("多选题的备选答案项必须是两个以上，且至少有两个（含）以上正确答案")]
		MULTIPLE_CHOICE_ANSWER_OPTIONS_ERROR,
		/// <summary>
		/// 判断题必须有答案
		/// </summary>
		[Description("判断题答案设置有误")]
		TRUE_FALSE_ANSWER_OPTIONS_ERROR,
		/// <summary>
		/// 填空题必须有答案
		/// </summary>
		[Description("填空题答案设置有误")]
		GAP_FILLING_ANSWER_OPTIONS_ERROR,
		/// <summary>
		/// 题目的标题不能为空
		/// </summary>
		[Description("题目的标题不能为空")]
		TOPIC_CANNOT_EMPTY
	}

	/// <summary>
	/// 新题目业务领域模型
	/// </summary>
	internal class NewQuestion : EntityBase<long, NewQuestionFailureRule>
	{
		#region 属性

		/// <summary>
		/// 题目建立用户ID
		/// </summary>
		public long UserId { get; set; }

		/// <summary>
		/// 归属课程ID
		/// </summary>
		public long CourseId { get; set; }

		/// <summary>
		/// 科目ID
		/// </summary>
		public long SubjectId { get; private set; }

		/// <summary>
		/// 课程类型（单选，多选，对错，填空题，问答题）
		/// </summary>
		public int Type { get; set; }

		/// <summary>
		/// 是否需要阅卷
		/// </summary>
		public bool Marking
		{
			get
			{
				return QuestionTools.HasMarking(Type);
			}
		}

		/// <summary>
		/// 题目
		/// </summary>
		public string Topic { get; set; }

		/// <summary>
		/// 原始答案备选项
		/// </summary>
		public QuestionAnswer AnswerOptions { get; private set; }

		/// <summary>
		/// 备选答案集合或知识点(以JSON格式存储)
		/// </summary>
		public string Answer { get; private set; }

		/// <summary>
		/// 使用次数
		/// </summary>
		public int Count => 0;

		/// <summary>
		/// 题目状态
		/// </summary>
		public int Status => (int)QuestionStatus.ENABLED;

		/// <summary>
		/// 建立时间
		/// </summary>
		public DateTime CreateTime => DateTime.Now;

		/// <summary>
		/// 最后使用时间
		/// </summary>
		public DateTime LastTime => CreateTime;

		#endregion

		#region 构造实例

		public NewQuestion()
		{
			ID = IDProvider.NewId;

			ReviseProperty();
		}

		#endregion

		#region 验证

		protected override void Validate()
		{
			//题目创建用户不存在
			if (!UsersAccessor.Exists(UserId))
			{
				AddBrokenRule(NewQuestionFailureRule.QUESTION_CREATOR_NOT_EXISTS);
			}

			//题目所属课程不存在
			if (!CourseAccessor.Exists(CourseId))
			{
				AddBrokenRule(NewQuestionFailureRule.COURSE_NOT_EXISTS);
			}

			//题目类型无效
			if (!((IList<int>)Enum.GetValues(typeof(QuestionType))).Contains(Type))
			{
				AddBrokenRule(NewQuestionFailureRule.QUESTION_TYPE_ERROR);
			}

			//题目标题不能为空
			if (string.IsNullOrWhiteSpace(Topic))
			{
				AddBrokenRule(NewQuestionFailureRule.TOPIC_CANNOT_EMPTY);
			}

			// 如果->非问答题且答案项为NULL，则抛出错误；
			// 否则->题目类型与答案项进能校验，校验规则如下：
			//  1、匹配，则验证答案项设置是否有效
			//  2、不匹配，则添加业务错误
			if (Type != (int)QuestionType.ESSAY_QUESTION && AnswerOptions == null)
			{
				AddBrokenRule(NewQuestionFailureRule.ANSWER_OPTIONS_CANNOT_EMPTY);
			}
			else if (QuestionTools.CheckAnswerOptionsType(AnswerOptions, Type))
			{
				//题目的答案项验证失败
				if (!AnswerOptions.Validate())
				{
					if (AnswerOptions is SingleChoiceAnswer)
					{
						AddBrokenRule(NewQuestionFailureRule.SINGLE_CHOICE_ANSWER_OPTIONS_ERROR);
					}
					else if (AnswerOptions is MultipleChoiceAnswer)
					{
						AddBrokenRule(NewQuestionFailureRule.MULTIPLE_CHOICE_ANSWER_OPTIONS_ERROR);
					}
					else if (AnswerOptions is TrueFalseAnswer)
					{
						AddBrokenRule(NewQuestionFailureRule.TRUE_FALSE_ANSWER_OPTIONS_ERROR);
					}
					else if (AnswerOptions is GapFillingAnswer)
					{
						AddBrokenRule(NewQuestionFailureRule.GAP_FILLING_ANSWER_OPTIONS_ERROR);
					}
				}
			}
			else
			{
				AddBrokenRule(NewQuestionFailureRule.QUESTION_TYPE_AND_ANSWER_OPTIONS_REGEX_FAILURE);
			}
		}

		#endregion

		#region 操作方法

		/// <summary>
		/// 校正对象属性
		/// </summary>
		private void ReviseProperty()
		{
			var course = CourseAccessor.Get(CourseId);

			if (course != null)
			{
				SubjectId = course.SubjectId;
			}
		}

		/// <summary>
		/// 设置答案或知识点
		/// </summary>
		/// <param name="answerOptions"></param>
		public void SetAnswerOptions(QuestionAnswer answerOptions)
		{
			AnswerOptions = answerOptions;

			if (answerOptions != null)
			{
				this.Answer = answerOptions.ToJson();
			}
		}

		/// <summary>
		/// 保存题目
		/// </summary>
		/// <returns></returns>
		public bool Save()
		{
			ThrowExceptionIfValidateFailure();

			Questions question = new Questions
			{
				QuestionId = ID,
				Answer = Answer,
				Count = Count,
				CourseId = CourseId,
				CreateTime = CreateTime,
				LastTime = LastTime,
				Marking = Marking,
				Status = Status,
				SubjectId = SubjectId,
				Topic = Topic,
				Type = Type,
				UserId = UserId
			};

			return QuestionsAccessor.Insert(question);
		}

		#endregion
	}
}
