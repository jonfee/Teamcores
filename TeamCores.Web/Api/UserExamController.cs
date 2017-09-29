using Microsoft.AspNetCore.Mvc;
using TeamCores.Domain.Services;
using TeamCores.Domain.Services.Request;
using TeamCores.Misc;
using TeamCores.Misc.Controller;
using TeamCores.Misc.Filters;
using TeamCores.Web.ViewModel.UserEvam;
using TeamCores.Web.ViewModel.UserExam;

namespace TeamCores.Web.Api
{
	/// <summary>
	/// 鐢ㄦ埛鑰冨嵎鍙婅€冭瘯鐩稿叧鏈嶅姟鎺ュ彛
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
		/// 鑾峰彇鑰冨嵎骞跺紑濮嬭€冭瘯
		/// </summary>
		/// <param name="examId">鑰冨嵎ID</param>
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
				//鑰冪敓淇℃伅
				Testee = new
				{
					UserId = user.UserId,
					Username = user.Username,
					Name = user.Name,
					Title = user.Title,
					Email = user.Email,
					Mobile = user.Mobile
				},
				//鑰冨嵎淇℃伅
				NewExam = newExam
			};

			return Ok(data);
		}

		/// <summary>
		/// 鎻愪氦鑰冨嵎绛旀
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
		/// 鑾峰彇鐢ㄦ埛绛斿嵎璇︾粏淇℃伅
		/// </summary>
		/// <param name="id">绛斿嵎ID</param>
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
		/// 鑾峰彇褰撳墠鐧诲綍鐢ㄦ埛鐨勭瓟鍗疯缁嗕俊鎭?
		/// </summary>
		/// <param name="id">绛斿嵎ID</param>
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
		/// 鎻愪氦闃呭嵎缁撴灉
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("marking")]
		[UserAuthorization(RequiredPermissions = "T10")]
		public IActionResult SubmitMarkingResult(UserExamMarkingResultViewModel model)
		{
			//当前阅卷用户ID(即当前登录用户)
			long reviewUserId = Utility.GetUserContext().UserId;

			var success = service.SubmitMarkingResult(reviewUserId,model.UserExamId, model.ResultDictionary);

			return Ok(success);
		}

		/// <summary>
		/// 鐢ㄦ埛鑰冨嵎鎼滅储
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
		/// 褰撳墠鐧诲綍鐢ㄦ埛鐨勮€冨嵎鎼滅储
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

		/// <summary>
		/// 删除用户考卷信息
		/// </summary>
		/// <param name="id">用户考卷ID</param>
		/// <returns></returns>
		[HttpPost]
		[Route("delete")]
		[UserAuthorization]
		public IActionResult Delete(long id)
		{
			//当前执行操作的用户ID
			long userId = Utility.GetUserContext().UserId;

			bool success = service.Delete(userId, id);

			return Ok(success);
		}
	}
}