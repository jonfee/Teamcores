using TeamCores.Data.Entity;
using TeamCores.Domain.Enums;
using TeamCores.Models.Answer;
using TeamCores.Models;
using TeamCores.Domain.Models.Question;
using TeamCores.Domain.Services.Request;
using TeamCores.Domain.Services.Response;

namespace TeamCores.Domain.Services
{
	/// <summary>
	/// 题目领域业务服务
	/// </summary>
	public class QuestionService
	{
		/// <summary>
		/// 添加题目
		/// </summary>
		/// <param name="userId">题目创建用户</param>
		/// <param name="courseId">所属课程</param>
		/// <param name="type">题目类型</param>
		/// <param name="topic">题目标题</param>
		/// <param name="answerOptions">答案选项</param>
		/// <returns></returns>
		public bool Add(long userId, long courseId, int type, string topic, QuestionAnswer answerOptions)
		{
			NewQuestion question = new NewQuestion
			{
				CourseId = courseId,
				Topic = topic,
				Type = type,
				UserId = userId
			};

			question.SetAnswerOptions(answerOptions);

			return question.Save();
		}

		/// <summary>
		/// 设置状态为“启用”
		/// </summary>
		/// <param name="questionId"></param>
		/// <returns></returns>
		public bool SetEnable(long questionId)
		{
			var question = new QuestionManage(questionId);

			return question.SetEnable();
		}

		/// <summary>
		/// 设置状态为“禁用”
		/// </summary>
		/// <param name="questionId"></param>
		/// <returns></returns>
		public bool SetDisable(long questionId)
		{
			var question = new QuestionManage(questionId);

			return question.SetDisable();
		}

		/// <summary>
		/// 删除题目
		/// </summary>
		/// <param name="questionId"></param>
		/// <returns></returns>
		public bool Delete(long questionId)
		{
			var question = new QuestionManage(questionId);

			return question.Delete();
		}

		/// <summary>
		/// 修改题目信息
		/// </summary>
		/// <param name="questionId"></param>
		/// <param name="state"></param>
		/// <returns></returns>
		public bool ModifyTo(long questionId, long courseId, int type, string topic, QuestionAnswer answerOptions, int status)
		{
			var question = new QuestionManage(questionId);

			var state = new QuestionModifyState
			{
				AnswerOptions = answerOptions,
				CourseId = courseId,
				Status = status,
				Topic = topic,
				Type = type
			};


            return question.ModifyTo(state);
		}

		/// <summary>
		/// 搜索题目信息
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public PagerModel<Questions> Search(QuestionSearchRequest request)
		{
			QuestionSearch search = new QuestionSearch(request);

			return search.Search();
		}

		/// <summary>
		/// 获取题目信息
		/// </summary>
		/// <param name="questionId">题目ID</param>
		/// <returns></returns>
		public Questions GetQuestion(long questionId)
		{
			var question = new QuestionManage(questionId);

			return question.Question;
		}
		
		/// <summary>
		/// 获取题目详细信息k
		/// </summary>
		/// <param name="questionId"></param>
		/// <returns></returns>
		public QuestionDetails GetDetails(long questionId)
		{
			var question = new QuestionManage(questionId);

			return question.ConvertToQuestionDetails();
		}
	}
}
