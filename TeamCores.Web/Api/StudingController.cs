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
		/// ѧԱѧϰ�γ��½�
		/// </summary>
		/// <param name="chapterId">ѧϰ�Ŀγ��½�ID</param>
		/// <returns></returns>
		[HttpPost]
		[Route("chapter")]
		public IActionResult StudyChapter(long chapterId)
		{
			var studentId = Utility.GetUserContext().UserId;

			var data = service.StudentStuding(studentId, chapterId);

			if (data != null)
			{
				//��¼���ογ�ѧϰʱ��
				Utility.GetUserContext().UpdateStudingTime();
			}

			return Ok(data);
		}

		/// <summary>
		/// �ϱ�ѧϰ״̬������
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