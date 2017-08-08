using System;
using System.Collections.Generic;
using System.Text;
using TeamCores.Domain.Models.Chapter;
using TeamCores.Domain.Models.UserStuding;
using TeamCores.Domain.Services.Response;
using TeamCores.Domain.Utility;

namespace TeamCores.Domain.Services
{
	/// <summary>
	/// 用户学习领域业务服务
	/// </summary>
	public class UserStudingService
	{
		/// <summary>
		/// 学员学习课程章节
		/// </summary>
		/// <param name="studentId">学员用户ID</param>
		/// <param name="chapterId">学习的章节ID</param>
		/// <returns></returns>
		public ChapterDetails StudentStuding(long studentId, long chapterId)
		{
			var chapter = new ChapterManage(chapterId);

			var details = ChapterTools.TransferFor(chapter);

			if (details != null)
			{
				var study = new CourseStudy(studentId, chapter.Chapter);
				study.Studing();
			}

			return details;
		}

		/// <summary>
		/// 上报学员学习心跳包
		/// </summary>
		/// <param name="studentId">学员ID</param>
		/// <param name="lastReportTime">最后上报的学习时间</param>
		/// <param name="maxIntervalMinutes">最大允许间隔上报的时间，超过此时间将视为无效</param>
		public void ReportStudingHeartbeat(long studentId,DateTime lastReportTime,int maxIntervalMinutes)
		{
			var monitor = new StudyTimeMonitor(studentId, lastReportTime, maxIntervalMinutes);

			monitor.AddStudingTime();
		}
	}
}
