using Microsoft.AspNetCore.Mvc;
using TeamCores.Common.Utilities;
using TeamCores.Domain.Services;
using TeamCores.Domain.Services.Request;
using TeamCores.Domain.Utility;
using TeamCores.Misc;
using TeamCores.Misc.Controller;
using TeamCores.Misc.Filters;
using TeamCores.Web.ViewModel.Question;

namespace TeamCores.Web.Api
{
	[Route("api/Question")]
	public class QuestionController : BaseController
	{
		QuestionService service = null;

		public QuestionController()
		{
			service = new QuestionService();
		}

		/// <summary>
		/// 新增题目
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("add")]
		[UserAuthorization(RequiredPermissions = "Q02")]
		public IActionResult Add(NewQuestionViewModel model)
		{
			var success = service.Add(
				Utility.GetUserContext().UserId,
				model.CourseId,
				model.Type,
				model.Topic,
				model.AnswerOptions);

			return Ok(success);
		}

		/// <summary>
		/// 启用题目
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("setenable")]
		[UserAuthorization(RequiredPermissions = "Q04")]
		public IActionResult SetEnable(long id)
		{
			var success = service.SetEnable(id);

			return Ok(success);
		}

		/// <summary>
		/// 禁用题目
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("setdisable")]
		[UserAuthorization(RequiredPermissions = "Q04")]
		public IActionResult SetDisable(long id)
		{
			var success = service.SetDisable(id);

			return Ok(success);
		}

		/// <summary>
		/// 删除题目
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("delete")]
		[UserAuthorization(RequiredPermissions = "Q03")]
		public IActionResult Delete(long id)
		{
			var success = service.Delete(id);

			return Ok(success);
		}

		/// <summary>
		/// 修改题目信息
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("modify")]
		[UserAuthorization(RequiredPermissions = "Q04")]
		public IActionResult Modify(QuestionModifyViewModel model)
		{
			var success = service.ModifyTo(
				model.QuestionId,
				model.CourseId,
				model.Type,
				model.Topic,
				model.AnswerOptions,
				model.Status);

			return Ok(success);
		}

		/// <summary>
		/// 搜索题目
		/// </summary>
		/// <param name="searcher">题目搜索器视图模型</param>
		/// <returns></returns>
		[HttpPost]
		[Route("search")]
		[UserAuthorization(RequiredPermissions = "Q01")]
		public IActionResult Search(QuestionSearcherViewModel searcher)
		{
			QuestionSearchRequest request = new QuestionSearchRequest
			{
				CourseId = searcher.CourseId,
				Keyword = searcher.Keyword,
				PageIndex = searcher.PageIndex,
				PageSize = searcher.PageSize,
				QuestionIds = searcher.QuestionIds.SplitToLongArray(),
				QuestionType = searcher.QuestionType,
				Status = searcher.Status
			};

			var result = service.Search(request);

			return Ok(result);
		}

		/// <summary>
		/// 获取题目信息
		/// </summary>
		/// <param name="id">题目ID</param>
		/// <returns></returns>
		[HttpPost]
		[Route("get")]
		[UserAuthorization(RequiredPermissions = "Q01")]
		public IActionResult GetQuestion(long id)
		{
			var data = service.GetQuestion(id);

			return Ok(data);
		}

		/// <summary>
		/// 获取题目详细信息
		/// </summary>
		/// <param name="id">题目ID</param>
		/// <returns></returns>
		[HttpPost]
		[Route("details")]
		[UserAuthorization(RequiredPermissions = "Q01")]
		public IActionResult GetDetails(long id)
		{
			var data = service.GetDetails(id);

			return Ok(data);
		}
	}
}