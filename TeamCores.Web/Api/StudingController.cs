using System;
using Microsoft.AspNetCore.Mvc;
using TeamCores.Domain.Services;
using TeamCores.Misc;
using TeamCores.Misc.Controller;
using TeamCores.Misc.Filters;

namespace TeamCores.Web.Api
{
	[Route("api/Studing")]
	public class StudingController : BaseController
	{
		UserStudingService service = null;

		public StudingController()
		{
			service = new UserStudingService();
		}

		/// <summary>
		/// 学员学习课程章节
		/// </summary>
		/// <param name="chapterId">学习的课程章节ID</param>
		/// <returns></returns>
		[HttpPost]
		[Route("chapter")]
		[UserAuthorization]
		public IActionResult StudyChapter(long chapterId)
		{
			var studentId = Utility.GetUserContext().UserId;

			var data = service.StudentStuding(studentId, chapterId);

			//if (data != null)
			//{
			//	//记录本次课程学习时间
			//	Utility.GetUserContext().UpdateStudingTime();
			//}

			return Ok(data);
		}

		/// <summary>
		/// 上报学习状态心跳包
		/// </summary>
		/// <param name="cycleSeconds">上报周期（单位：秒）</param>
		/// <returns></returns>
		[HttpPost]
		[Route("heartbeat")]
		[UserAuthorization]
		public IActionResult ReportStudingTime(int cycleSeconds)
		{
			if (cycleSeconds > 0)
			{
				var studentId = Utility.GetUserContext().UserId;

				//上次上报时间默认视为当前时间减去上报周期（秒）
				var lastReportTime = DateTime.Now.AddSeconds(-cycleSeconds);

				service.ReportStudingHeartbeat(studentId, lastReportTime, 5);

				//Utility.GetUserContext().UpdateStudingTime();
			}

			return Ok(true);
		}
	}
}