using System;
using System.ComponentModel;
using TeamCores.Data.DataAccess;

namespace TeamCores.Domain.Models.UserStuding
{
	/// <summary>
	/// 在线学习时间监测业务领域验证错误结果枚举
	/// </summary>
	internal enum StudyTimeMonitorFailureRule
	{
		/// <summary>
		/// 学员不存在
		/// </summary>
		[Description("学员不存在")]
		STUDENT_NOT_EXISTS = 1
	}

	/// <summary>
	/// 在线学习时间监测业务领域模型
	/// </summary>
	internal class StudyTimeMonitor : EntityBase<long, StudyTimeMonitorFailureRule>
	{
		#region 属性

		/// <summary>
		/// 学员ID
		/// </summary>
		public readonly long StudentId;

		/// <summary>
		/// 最后上报的学习时间
		/// </summary>
		public readonly DateTime LastReportStudyTime;

		/// <summary>
		/// 接收到消息的时间
		/// </summary>
		public readonly DateTime ReceivedTime;

		/// <summary>
		/// 最大允许间隔上报的时间，超过此时间视为无效
		/// </summary>
		public readonly int MaxIntervalMinutes;

		#endregion

		#region 构造函数

		/// <summary>
		/// 实始化<see cref="StudyTimeMonitor"/>实例对象
		/// </summary>
		/// <param name="studentId">学员ID</param>
		/// <param name="lastReportTime">最后上报的学习时间</param>
		/// <param name="maxIntervalMinutes">最大允许间隔上报的时间，超过此时间将视为无效</param>
		public StudyTimeMonitor(long studentId, DateTime lastReportTime, int maxIntervalMinutes)
		{
			StudentId = studentId;
			LastReportStudyTime = lastReportTime;
			MaxIntervalMinutes = maxIntervalMinutes;
			ReceivedTime = DateTime.Now;
		}

		#endregion

		#region 验证

		protected override void Validate()
		{
			//empty
		}

		#endregion

		#region 操作方法

		/// <summary>
		/// 增加学习时间
		/// </summary>
		public void AddStudingTime()
		{
			ThrowExceptionIfValidateFailure();

			//计算本次上报后新增的学习时间
			if (LastReportStudyTime >= ReceivedTime) return;

			//时间差
			TimeSpan timespan = ReceivedTime.Subtract(LastReportStudyTime);

			//具体相差的总分钟数
			int totalMinutes = unchecked((int)Math.Floor(timespan.TotalMinutes));

			//更新到数据库
			UserStudyAccessor.UpdateStudyTime(StudentId, totalMinutes);
		}

		#endregion
	}
}
