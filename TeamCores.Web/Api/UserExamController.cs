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
	/// 用户考卷及考试相关服务接口
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
		/// 考试
		/// </summary>
		/// <param name="examId">考卷ID</param>
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
		/// 提交考卷答案
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
		/// 获取用户答卷详细信息
		/// </summary>
		/// <param name="id">答卷ID</param>
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