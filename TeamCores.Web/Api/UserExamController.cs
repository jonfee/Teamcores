using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeamCores.Domain.Services;
using TeamCores.Misc;
using TeamCores.Misc.Controller;

namespace TeamCores.Web.Api
{
	/// <summary>
	/// �û�����������ط���ӿ�
	/// </summary>
	[Route("api/UserExam")]
	public class UserExamController : BaseController
	{
		UserExamService service = null;

		public UserExamController()
		{
			service = new UserExamService();
		}

		/// <summary>
		/// ����
		/// </summary>
		/// <param name="examId">����ID</param>
		/// <returns></returns>
		[HttpPost]
		[Route("test")]
		public IActionResult TestExam(long examId)
		{
			long userId = Utility.GetUserContext().UserId;
			var data = service.TakeExam(userId, examId);

			return Ok(data);
		}
	}
}