using Microsoft.AspNetCore.Mvc;
using TeamCores.Domain.Services;
using TeamCores.Misc;
using TeamCores.Misc.Controller;

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
		public IActionResult StudyChapter(long chapterId)
		{
			var studentId = Utility.GetUserContext().UserId;

			var data = service.StudentStuding(studentId, chapterId);

			if (data != null)
			{
				//记录本次课程学习时间
				Utility.GetUserContext().UpdateStudingTime();
			}

			return Ok(data);
		}

		/// <summary>
		/// 上报学习状态心跳包
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		[Route("heartbeat")]
		public IActionResult ReportStudingTime()
		{
			var studentId = Utility.GetUserContext().UserId;
			var lastReportTime = Utility.GetUserContext().LastReportStudyTime;

			if (lastReportTime.HasValue)
			{
				service.ReportStudingHeartbeat(studentId, lastReportTime.Value, 5);
			}

			Utility.GetUserContext().UpdateStudingTime();

			return Ok(true);
		}
	}
}