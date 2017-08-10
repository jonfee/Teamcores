using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeamCores.Domain.Services;
using TeamCores.Misc;
using TeamCores.Misc.Controller;
using TeamCores.Web.ViewModel.UserEvam;

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

		/// <summary>
		/// �ύ�����
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("submit")]
		public IActionResult SubmitAnswer(UserExamSubmitViewModel model)
		{
			bool success = service.SubmitExamAnswer(model.UserId, model.UserExamId, model.Answers);

			return Ok(success);
		}

		/// <summary>
		/// ��ȡ�û������ϸ��Ϣ
		/// </summary>
		/// <param name="id">���ID</param>
		/// <returns></returns>
		[HttpPost]
		[Route("details")]
		public IActionResult GetDetails(long id)
		{
			var data = service.GetDetails(id);

			return Ok(data);
		}
	}
}