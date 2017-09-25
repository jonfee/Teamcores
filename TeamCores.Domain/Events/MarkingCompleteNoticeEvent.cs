using System;
using System.Collections.Generic;
using System.Text;
using TeamCores.Common;
using TeamCores.Data.DataAccess;
using TeamCores.Domain.Enums;

namespace TeamCores.Domain.Events
{
	internal class MarkingCompleteNoticeEventState : DomainEventState
	{
		/// <summary>
		/// 阅卷用户
		/// </summary>
		public long ReviewUser { get; set; }

		/// <summary>
		/// 试卷的原模板信息
		/// </summary>
		public Data.Entity.Exams Exam { get; set; }

		/// <summary>
		/// 阅卷的宿主试卷
		/// </summary>
		public Data.Entity.ExamUsers UserExam { get; set; }
	}


	internal class MarkingCompleteNoticeEvent : DomainEvent
	{
		public MarkingCompleteNoticeEvent(MarkingCompleteNoticeEventState state) : base(state)
		{
		}

		public override void Execute()
		{
			Validate();

			var state = State as MarkingCompleteNoticeEventState;

			if (state == null) return;

			if (state.UserExam == null) return;

			//考试的类型描述
			var examType = state.Exam.ExamType == (int)ExamType.TEST_EXAM ? "练习" : "考试";
			//得分情况文字标注颜色
			var color = "green";
			var passTip = "及格";
			if (state.UserExam.Total < state.Exam.Pass)
			{
				color = "red";
				passTip = "未及格";
			}

			var message = new Data.Entity.Messages
			{
				MessageId = IDProvider.NewId,
				Title = "你的练习(考）卷已阅卷完成，点击查看详情。",
				Content = $@"你在{state.UserExam.CreateTime}参加的《{state.Exam.Title}》{examType}已完成阅卷，最后得分{state.UserExam.Total}分<span style='color:{color}'>（{passTip}）</span>，<a href=""/exams/testdetails/{state.UserExam.Id}"">点击查看详细结果</a>。",
				CreateTime = DateTime.Now,
				Readed = false,
				ReadTime = null,
				Receiver = state.UserExam.UserId,
				Sender = state.ReviewUser
			};

			MessagesAccessor.Add(message);
		}
	}
}
