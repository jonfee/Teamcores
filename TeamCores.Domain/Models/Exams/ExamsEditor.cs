using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TeamCores.Common.Utilities;
using TeamCores.Data.DataAccess;
using TeamCores.Domain.Enums;

namespace TeamCores.Domain.Models.Exams
{
	/// <summary>
	/// 考卷编辑业务领域验证错误结果枚举
	/// </summary>
	internal enum ExamsEditorFailureRule
	{
		/// <summary>
		/// 考卷对象不存在
		/// </summary>
		[Description("考卷对象不存在")]
		EXAMS_NOT_EXISTS = 1,
		/// <summary>
		/// 考卷状态不能设置为“启用”
		/// </summary>
		[Description("考卷状态不能设置为“启用”")]
		STATUS_CANNOT_SET_ENABLED,
		/// <summary>
		/// 考卷状态不能设置为“禁用”
		/// </summary>
		[Description("考卷状态不能设置为“禁用”")]
		STATUS_CANNOT_SET_DISABLED,
		/// <summary>
		/// 考卷编辑对象为空
		/// </summary>
		[Description("考卷编辑对象为空")]
		MODIFYSTATE_OBJECT_IS_NULL = 1,
		/// <summary>
		/// 考卷标题不能为空
		/// </summary>
		[Description("考卷标题不能为空")]
		TITLE_CANNOT_EMPTY,
		/// <summary>
		/// 考卷的题库不能为空
		/// </summary>
		[Description("考卷的题库不能为空")]
		QUESTIONS_CANNOT_EMPTY,
		/// <summary>
		/// 及格分必须小于总分
		/// </summary>
		[Description("及格分必须小于总分")]
		PASS_MUST_LESS_THAN_TO_TOTAL,
		/// <summary>
		/// 总分与各题型分值之和不一致
		/// </summary>
		[Description("总分与各题型分值之和不一致")]
		TOTAL_NOT_EQUALS_SUM,
		/// <summary>
		/// 开始时间必须在结束时间之前
		/// </summary>
		[Description("开始时间必须在结束时间之前")]
		STARTTIME_MUST_BE_BEFORE_THE_ENDTIME
	}

	/// <summary>
	/// 考卷编辑状态数据
	/// </summary>
	public class ExamsModifyState
	{
		/// <summary>
		/// 考试类型（考试，练习）
		/// </summary>
		public int ExamType { get; set; }

		/// <summary>
		/// 关联的课程ID集合
		/// </summary>
		public string CourseIds { get; set; }

		/// <summary>
		/// 考卷标题
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// 考试目标备注说明
		/// </summary>
		public string Remarks { get; set; }

		/// <summary>
		/// 考题ID集合
		/// </summary>
		public string Questions { get; set; }

		/// <summary>
		/// 考试时间（单位：分钟）
		/// </summary>
		public int Time { get; set; }

		/// <summary>
		/// 总分
		/// </summary>
		public int Total { get; set; }

		/// <summary>
		/// 及格分
		/// </summary>
		public int Pass { get; set; }

		/// <summary>
		/// 单选题目数量
		/// </summary>
		public int Radio { get; set; }

		/// <summary>
		/// 单选题总分
		/// </summary>
		public int RedioTotal { get; set; }

		/// <summary>
		/// 多选题目数量
		/// </summary>
		public int Multiple { get; set; }

		/// <summary>
		/// 多选总分
		/// </summary>
		public int MultipleTotal { get; set; }

		/// <summary>
		/// 判断题
		/// </summary>
		public int Judge { get; set; }

		/// <summary>
		/// 判断题总分
		/// </summary>
		public int JudgeTotal { get; set; }

		/// <summary>
		/// 填空题
		/// </summary>
		public int Filling { get; set; }

		/// <summary>
		/// 填空题总分
		/// </summary>
		public int FillingTotal { get; set; }

		/// <summary>
		/// 问答题数
		/// </summary>
		public int Ask { get; set; }

		/// <summary>
		/// 问答题总分
		/// </summary>
		public int AskTotal { get; set; }

		/// <summary>
		/// 可使用开始时间,null不限制
		/// </summary>
		public DateTime? StartTime { get; set; }

		/// <summary>
		/// 结束使用时间,null不限制
		/// </summary>
		public DateTime? EndTime { get; set; }

		public int Status { get; set; }
	}

	/// <summary>
	/// 考卷编辑业务领域模型
	/// </summary>
	internal class ExamsEditor : EntityBase<long, ExamsEditorFailureRule>
	{
		#region 属性

		private Data.Entity.Exams _exams;

		/// <summary>
		/// 考卷信息
		/// </summary>
		public Data.Entity.Exams Exams
		{
			get
			{
				if (_exams == null)
				{
					_exams = ExamsAccessor.Get(ID);
				}

				return _exams;
			}
		}

		#endregion

		#region 构造函数

		/// <summary>
		/// 初始化一个<see cref="ExamsEditor"/>对象实例
		/// </summary>
		/// <param name="examsId">考卷ID</param>
		public ExamsEditor(long examsId)
		{
			ID = examsId;
		}

		/// <summary>
		/// 初始化一个<see cref="ExamsEditor"/>对象实例
		/// </summary>
		/// <param name="exams">考卷数据对象</param>
		public ExamsEditor(Data.Entity.Exams exams)
		{
			if (exams != null)
			{
				_exams = exams;
				ID = exams.ExamId;
			}
		}

		#endregion

		#region 验证

		protected override void Validate()
		{
			throw new NotImplementedException();
		}

		#endregion

		#region 操作方法

		/// <summary>
		/// 检测是否能被删除
		/// </summary>
		/// <returns></returns>
		public bool CanDelete()
		{
			return false;
		}

		/// <summary>
		/// 检测是否能被启用
		/// </summary>
		/// <returns></returns>
		public bool CanSetEnable()
		{
			return Exams != null && Exams.Status == (int)ExamsStatus.DISABLED;
		}

		/// <summary>
		/// 检测是否能被禁用
		/// </summary>
		/// <returns></returns>
		public bool CanSetDisable()
		{
			return Exams != null && Exams.Status == (int)ExamsStatus.ENABLED;
		}

		/// <summary>
		/// 检测是否能被编辑
		/// </summary>
		/// <returns></returns>
		public bool CanModify()
		{
			return true;
		}

		/// <summary>
		/// 设置状态为启用
		/// </summary>
		/// <returns></returns>
		public bool SetEnable()
		{
			ThrowExceptionIfValidateFailure(() =>
			{
				if (!CanSetEnable()) AddBrokenRule(ExamsEditorFailureRule.STATUS_CANNOT_SET_ENABLED);
			});

			return ExamsAccessor.SetStatus(ID, (int)ExamsStatus.ENABLED);
		}

		/// <summary>
		/// 设置状态为禁用
		/// </summary>
		/// <returns></returns>
		public bool SetDisable()
		{
			ThrowExceptionIfValidateFailure(() =>
			{
				if (!CanSetEnable()) AddBrokenRule(ExamsEditorFailureRule.STATUS_CANNOT_SET_DISABLED);
			});

			return ExamsAccessor.SetStatus(ID, (int)ExamsStatus.DISABLED);
		}

		/// <summary>
		/// 从<see cref="ExamsModifyState"/>对象更新考卷
		/// </summary>
		/// <param name="state"></param>
		/// <returns></returns>
		public bool ModifyTo(ExamsModifyState state)
		{
			ThrowExceptionIfValidateFailure(() =>
			{
				if (state == null)
				{
					AddBrokenRule(ExamsEditorFailureRule.MODIFYSTATE_OBJECT_IS_NULL);
				}
				else
				{
					//考卷标题为Empty
					if (string.IsNullOrWhiteSpace(state.Title)) AddBrokenRule(ExamsEditorFailureRule.TITLE_CANNOT_EMPTY);

					//该考卷未指定题库
					if (QuestionCount(state.Questions) < 1) AddBrokenRule(ExamsEditorFailureRule.QUESTIONS_CANNOT_EMPTY);

					//及格分不能大于等于总分是不允许的
					if (state.Pass >= state.Total) AddBrokenRule(ExamsEditorFailureRule.PASS_MUST_LESS_THAN_TO_TOTAL);

					//总分及各题分是否一致
					var totalSum = state.RedioTotal + state.MultipleTotal + state.JudgeTotal + state.FillingTotal + state.AskTotal;
					if (totalSum != state.Total) AddBrokenRule(ExamsEditorFailureRule.TOTAL_NOT_EQUALS_SUM);

					//如果有设置考卷的使用有效时间，则开始时间必须小于结束时间
					if (state.StartTime.HasValue && state.EndTime.HasValue && state.StartTime.Value >= state.EndTime.Value)
					{
						AddBrokenRule(ExamsEditorFailureRule.STARTTIME_MUST_BE_BEFORE_THE_ENDTIME);
					}
				}
			});

			//映射数据实体对象后存储
			var editExams = TransferNewFor(state);

			return ExamsAccessor.Update(editExams);
		}

		/// <summary>
		/// 变更考卷信息
		/// </summary>
		/// <param name="state"></param>
		private Data.Entity.Exams TransferNewFor(ExamsModifyState state)
		{
			var editExams = new Data.Entity.Exams();
			editExams = Exams.CopyTo(editExams);

			editExams.ExamType = state.ExamType;
			editExams.CourseIds = state.CourseIds;
			editExams.Title = state.Title;
			editExams.Remarks = state.Remarks;
			editExams.Questions = state.Questions;
			editExams.Time = state.Time;
			editExams.Total = state.Total;
			editExams.Pass = state.Pass;
			editExams.Radio = state.Radio;
			editExams.RedioTotal = state.RedioTotal;
			editExams.Multiple = state.Multiple;
			editExams.MultipleTotal = state.MultipleTotal;
			editExams.Judge = state.Judge;
			editExams.JudgeTotal = state.JudgeTotal;
			editExams.Filling = state.Filling;
			editExams.FillingTotal = state.FillingTotal;
			editExams.Ask = state.Ask;
			editExams.AskTotal = state.AskTotal;
			editExams.StartTime = state.StartTime;
			editExams.EndTime = state.EndTime;

			return editExams;
		}

		/// <summary>
		/// 获取题库中的题目数量
		/// </summary>
		/// <param name="questionIds"></param>
		/// <returns></returns>
		private int QuestionCount(string questionIds)
		{
			var ids = GetQuestions(questionIds);

			return ids != null ? ids.Count() : 0;
		}

		/// <summary>
		/// 获取考卷的题库集合
		/// </summary>
		/// <returns></returns>
		private IEnumerable<long> GetQuestions(string questionIds)
		{
			if (string.IsNullOrWhiteSpace(questionIds)) yield break;

			var strIds = questionIds.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

			long tempId = 0;

			foreach (var str in strIds)
			{
				if (long.TryParse(str, out tempId))
				{
					yield return tempId;
				}
			}
		}

		#endregion
	}
}
