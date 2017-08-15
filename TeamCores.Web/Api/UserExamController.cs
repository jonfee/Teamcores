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
		/// ��ȡ������ʼ����
		/// </summary>
		/// <param name="examId">����ID</param>
		/// <returns></returns>
		[HttpPost]
		[Route("test")]
		[UserAuthorization]
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
		[Route("answer")]
		[UserAuthorization]
		public IActionResult SubmitAnswer(UserExamSubmitViewModel model)
		{
			long userId = Utility.GetUserContext().UserId;
			bool success = service.SubmitExamAnswer(userId, model.UserExamId, model.Answers);

			return Ok(success);
		}

		/// <summary>
		/// ��ȡ�û������ϸ��Ϣ
		/// </summary>
		/// <param name="id">���ID</param>
		/// <returns></returns>
		[HttpPost]
		[Route("details")]
		[UserAuthorization(RequiredPermissions = "T01")]
		public IActionResult GetDetails(long id)
		{
			var data = service.GetDetails(id);

			return Ok(data);
		}

		/// <summary>
		/// ��ȡ��ǰ��¼�û��Ĵ����ϸ��Ϣ
		/// </summary>
		/// <param name="id">���ID</param>
		/// <returns></returns>
		[HttpPost]
		[Route("myexam")]
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
		/// �ύ�ľ���
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("marking")]
		[UserAuthorization(RequiredPermissions = "T10")]
		public IActionResult SubmitMarkingResult(UserExamMarkingResultViewModel model)
		{
			var success = service.SubmitMarkingResult(model.UserExamId, model.Result);

			return Ok(success);
		}

		/// <summary>
		/// �û���������
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
		/// ��ǰ��¼�û��Ŀ�������
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