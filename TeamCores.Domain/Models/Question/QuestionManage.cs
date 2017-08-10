using System;
using System.Collections;
using System.ComponentModel;
using TeamCores.Data.DataAccess;
using TeamCores.Data.Entity;
using TeamCores.Domain.Enums;
using TeamCores.Domain.Services.Response;
using TeamCores.Domain.Utility;
using TeamCores.Models.Answer;

namespace TeamCores.Domain.Models.Question
{
	internal enum QuestionEditFailureRule
	{
		/// <summary>
		/// 题目对象不存在
		/// </summary>
		[Description("题目对象不存在")]
		QUESTION_NOT_EXISTS = 1,
		/// <summary>
		/// 状态不能设置为“启用”
		/// </summary>
		[Description("状态不能设置为“启用”")]
		STATUS_CANNOT_SET_TO_ENABLE,
		/// <summary>
		/// 状态不能设置为“禁用”
		/// </summary>
		[Description("状态不能设置为“禁用”")]
		STATUS_CANNOT_SET_TO_DISABLE,
		/// <summary>
		/// 删除操作不被允许
		/// </summary>
		[Description("删除操作不被允许")]
		CANNOT_DELETE,
		/// <summary>
		/// 编辑操作不被允许
		/// </summary>
		[Description("编辑操作不被允许")]
		CANNOT_EDIT,
		/// <summary>
		/// 编辑后的内容不能为空
		/// </summary>
		[Description("编辑后的内容不能为空")]
		MODIFY_STATE_CANNOT_EMPTY,
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
	/// 题目的编辑数据状态
	/// </summary>
	internal class QuestionModifyState
	{
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
		public QuestionAnswer AnswerOptions { get; set; }

		/// <summary>
		/// 备选答案集合或知识点(以JSON格式存储)
		/// </summary>
		public string Answer
		{
			get
			{
				if (AnswerOptions != null)
					return string.Empty;
				else
					return AnswerOptions.ToJson();
			}
		}

		/// <summary>
		/// 题目状态
		/// </summary>
		public int Status { get; set; }

		/// <summary>
		/// 校正对象属性
		/// </summary>
		public void ReviseProperty()
		{
			var course = CourseAccessor.Get(CourseId);

			if (course != null)
			{
				SubjectId = course.SubjectId;
			}
		}
	}

	/// <summary>
	/// 题目编辑业务领域模型
	/// </summary>
	internal class QuestionManage : EntityBase<long, QuestionEditFailureRule>
	{
		#region 属性

		public readonly Questions Question;

		#endregion

		#region 实例化构造

		public QuestionManage(long questionId)
		{
			this.ID = questionId;

			Question = QuestionsAccessor.Get(questionId);
		}

		public QuestionManage(Questions question)
		{
			if (question != null)
			{
				ID = question.QuestionId;
				Question = question;
			}
		}

		#endregion

		#region 验证

		protected override void Validate()
		{
			if (Question == null) AddBrokenRule(QuestionEditFailureRule.QUESTION_NOT_EXISTS);
		}

		#endregion

		#region 操作方法

		public bool CanSetEnable()
		{
			return Question != null && Question.Status == (int)QuestionStatus.DISABLED;
		}

		public bool CanSetDisable()
		{
			return Question != null && Question.Status == (int)QuestionStatus.ENABLED;
		}

		public bool CanDelete()
		{
			return false;
		}

		public bool CanModify()
		{
			return true;
		}

		public bool SetEnable()
		{
			ThrowExceptionIfValidateFailure(() =>
			{
				if (!CanSetEnable()) AddBrokenRule(QuestionEditFailureRule.STATUS_CANNOT_SET_TO_ENABLE);
			});

			return QuestionsAccessor.SetStatus(ID, (int)QuestionStatus.ENABLED);
		}

		public bool SetDisable()
		{
			ThrowExceptionIfValidateFailure(() =>
			{
				if (!CanSetDisable()) AddBrokenRule(QuestionEditFailureRule.STATUS_CANNOT_SET_TO_DISABLE);
			});

			return QuestionsAccessor.SetStatus(ID, (int)QuestionStatus.DISABLED);
		}

		public bool Delete()
		{
			ThrowExceptionIfValidateFailure(() =>
			{
				if (!CanDelete()) AddBrokenRule(QuestionEditFailureRule.CANNOT_DELETE);
			});

			return QuestionsAccessor.Delete(ID);
		}

		public bool ModifyTo(QuestionModifyState state)
		{
			ThrowExceptionIfValidateFailure(() =>
			{
				if (state == null)
				{
					AddBrokenRule(QuestionEditFailureRule.MODIFY_STATE_CANNOT_EMPTY);
				}
				else if (CanModify())
				{
					//题目所属课程不存在
					if (!CourseAccessor.Exists(state.CourseId))
					{
						AddBrokenRule(QuestionEditFailureRule.COURSE_NOT_EXISTS);
					}

					//题目类型无效
					if (!((IList)Enum.GetValues(typeof(QuestionType))).Contains(state.Type))
					{
						AddBrokenRule(QuestionEditFailureRule.QUESTION_TYPE_ERROR);
					}

					//题目标题不能为空
					if (string.IsNullOrWhiteSpace(state.Topic))
					{
						AddBrokenRule(QuestionEditFailureRule.TOPIC_CANNOT_EMPTY);
					}

					// 如果->非问答题且答案项为NULL，则抛出错误；
					// 否则->题目类型与答案项进能校验，校验规则如下：
					//  1、匹配，则验证答案项设置是否有效
					//  2、不匹配，则添加业务错误
					if (state.Type != (int)QuestionType.ESSAY_QUESTION && state.AnswerOptions == null)
					{
						AddBrokenRule(QuestionEditFailureRule.ANSWER_OPTIONS_CANNOT_EMPTY);
					}
					else if (!QuestionTools.CheckAnswerOptionsType(state.AnswerOptions, state.Type))
					{
						//题目的答案项验证失败
						if (!state.AnswerOptions.Validate())
						{
							if (state.AnswerOptions is SingleChoiceAnswer)
							{
								AddBrokenRule(QuestionEditFailureRule.SINGLE_CHOICE_ANSWER_OPTIONS_ERROR);
							}
							else if (state.AnswerOptions is MultipleChoiceAnswer)
							{
								AddBrokenRule(QuestionEditFailureRule.MULTIPLE_CHOICE_ANSWER_OPTIONS_ERROR);
							}
							else if (state.AnswerOptions is TrueFalseAnswer)
							{
								AddBrokenRule(QuestionEditFailureRule.TRUE_FALSE_ANSWER_OPTIONS_ERROR);
							}
							else if (state.AnswerOptions is GapFillingAnswer)
							{
								AddBrokenRule(QuestionEditFailureRule.GAP_FILLING_ANSWER_OPTIONS_ERROR);
							}
						}
					}
					else
					{
						AddBrokenRule(QuestionEditFailureRule.QUESTION_TYPE_AND_ANSWER_OPTIONS_REGEX_FAILURE);
					}
				}
				else
				{
					AddBrokenRule(QuestionEditFailureRule.CANNOT_EDIT);
				}
			});

			if (state.SubjectId == 0) state.ReviseProperty();

			return QuestionsAccessor.Update(
				ID,
				state.CourseId,
				state.SubjectId,
				state.Type,
				state.Marking,
				state.Topic,
				state.Answer,
				state.Status);
		}

		/// <summary>
		/// 获取并转换为<see cref="QuestionDetails"/>类型数据对象
		/// </summary>
		/// <returns></returns>
		public QuestionDetails ConvertToQuestionDetails()
		{
			if (Question == null) return null;

			//科目名称
			var subjectName = SubjectsAccessor.GetName(Question.SubjectId);
			//课程标题
			var courseTitle = CourseAccessor.GetTitle(Question.CourseId);

			var details = new QuestionDetails
			{
				QuestionId = Question.QuestionId,
				Answer = QuestionTools.DeserializeAnswers(Question.Answer, Question.Type),
				Count = Question.Count,
				CourseId = Question.CourseId,
				CourseTitle = courseTitle,
				CreateTime = Question.CreateTime,
				LastTime = Question.LastTime,
				Marking = Question.Marking,
				Status = Question.Status,
				SubjectId = Question.SubjectId,
				SubjectName = subjectName,
				Topic = Question.Topic,
				Type = Question.Type,
				UserId = Question.UserId
			};

			return details;
		}

		#endregion
	}
}
