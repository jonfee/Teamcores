using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TeamCores.Domain.Services;
using TeamCores.Domain.Services.Request;
using TeamCores.Domain.Services.Response;
using TeamCores.Misc;
using TeamCores.Misc.Controller;
using TeamCores.Misc.Filters;
using TeamCores.Web.ViewModel.UserEvam;
using TeamCores.Web.ViewModel.UserExam;

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
		/// 获取考卷并开始考试
		/// </summary>
		/// <param name="examId">考卷ID</param>
		/// <returns></returns>
		[HttpPost]
		[Route("test")]
		[UserAuthorization]
		public IActionResult TestExam(long examId)
		{
			var user = Utility.GetUserContext();
			var newExam = service.TakeExam(user.UserId, examId);

			var data = new
			{
				//考生信息
				Testee = new
				{
					UserId = user.UserId,
					Username = user.Username,
					Name = user.Name,
					Title = user.Title,
					Email = user.Email,
					Mobile = user.Mobile
				},
				//考卷信息
				NewExam = newExam
			};

			return Ok(data);
		}

		/// <summary>
		/// 提交考卷答案
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("answer")]
		[UserAuthorization]
		public IActionResult SubmitAnswer(UserExamSubmitViewModel model)
		{
			long userId = Utility.GetUserContext().UserId;
			
			bool success = service.SubmitExamAnswer(userId, model.UserExamId, model.AnswersDictionary);

			return Ok(success);
		}

		/// <summary>
		/// 获取用户答卷详细信息
		/// </summary>
		/// <param name="id">答卷ID</param>
		/// <returns></returns>
		[HttpGet]
		[Route("details/{id}")]
		[UserAuthorization(RequiredPermissions = "T01")]
		public IActionResult GetDetails(long id)
		{
			var data = service.GetDetails(id);

			return Ok(data);
		}

		/// <summary>
		/// 获取当前登录用户的答卷详细信息
		/// </summary>
		/// <param name="id">答卷ID</param>
		/// <returns></returns>
		[HttpGet]
		[Route("myexam/{id}")]
		[UserAuthorization]
		public IActionResult GetMyExamDetails(long id)
		{
			long userId = Utility.GetUserContext().UserId;

			var data = service.GetDetails(id);

			if (data != null && (data.Student == null || data.Student.StudentId != userId))
			{
				return NoAccess();
			}
			else
			{
				return Ok(data);
			}
		}

		/// <summary>
		/// 提交阅卷结果
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("marking")]
		[UserAuthorization(RequiredPermissions = "T10")]
		public IActionResult SubmitMarkingResult(UserExamMarkingResultViewModel model)
		{
			var success = service.SubmitMarkingResult(model.UserExamId, model.ResultDictionary);

			return Ok(success);
		}

		/// <summary>
		/// 用户考卷搜索
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("search")]
		[UserAuthorization(RequiredPermissions = "T01")]
		public IActionResult Search(UserExamSearcherViewModel model)
		{
			UserExamSearchRequest request = new UserExamSearchRequest
			{
				PageIndex = model.PageIndex,
				PageSize = model.PageSize,
				StudentId = model.StudentId,
				ExamId = model.ExamId,
				Status = model.Status
			};

			var data = service.Search(request);

			return Ok(data);
		}

		/// <summary>
		/// 当前登录用户的考卷搜索
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("myexams")]
		[UserAuthorization]
		public IActionResult SearchMyExams(MyExamSearcherViewModel model)
		{
			long userId = Utility.GetUserContext().UserId;

			UserExamSearchRequest request = new UserExamSearchRequest
			{
				PageIndex = model.PageIndex,
				PageSize = model.PageSize,
				StudentId = userId,
				ExamId = model.ExamId,
				Status = model.Status
			};

			var data = service.Search(request);

			return Ok(data);
		}
	}
}