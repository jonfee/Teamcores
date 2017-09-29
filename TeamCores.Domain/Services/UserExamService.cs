using System.Collections.Generic;
using System.Linq;
using TeamCores.Domain.Models.Exams;
using TeamCores.Domain.Models.UserExam;
using TeamCores.Domain.Services.Request;
using TeamCores.Domain.Services.Response;
using TeamCores.Models;

namespace TeamCores.Domain.Services
{
	/// <summary>
	/// 用户考卷及考试相关领域业务服务
	/// </summary>
	public class UserExamService
	{
		/// <summary>
		/// 用户参加考试，返回根据考卷模板生成最终的考试卷
		/// </summary>
		/// <param name="userId">用户ID</param>
		/// <param name="examId">准备参考的试卷</param>
		/// <remarks></remarks>
		public NewExamPaper TakeExam(long userId, long examId)
		{
			var examManage = new ExamsManage(examId);
			var newExamPaper = examManage.CreateNewExamPaper();

			if (newExamPaper != null)
			{
				//将题目信息初始化为作答信息
				var results = newExamPaper.Questions.Select(p => new UserExamQuestionResult(p)).ToList();

				//将新参考试卷到数据库
				UserExamInitRequest request = new UserExamInitRequest
				{
					UserExamId = newExamPaper.PaperId,
					ExamId = examId,
					UserId = userId,
					CreateTime=newExamPaper.CreateTime,
					QuestionsResults = results
				};

				var userExamInit = new UserExamInit(request);
				bool success = userExamInit.Save();

				if (success)
				{
					return newExamPaper;
				}
			}

			return null;
		}

		/// <summary>
		/// 用户提交考卷答案
		/// </summary>
		/// <param name="userId">用户ID</param>
		/// <param name="userExamId">考卷ID</param>
		/// <param name="answers">答案<see cref="Dictionary{TKey, TValue}"/>（TKey表示题目ID,TValue表示题目答案）</param>
		/// <returns></returns>
		public bool SubmitExamAnswer(long userId, long userExamId, Dictionary<long, string> answers)
		{
			var request = new ExamPagerSubmitRequest
			{
				AnswerResults = answers,
				UserExamId = userExamId,
				UserId = userId
			};

			var examPaper = new SubmitExamPaper(request);

			return examPaper.SubmitResult();
		}

		/// <summary>
		/// 获取用户答卷详细信息
		/// </summary>
		/// <param name="userExamId">考(答）卷ID</param>
		/// <returns></returns>
		public UserExamPaperMarkingDetails GetDetails(long userExamId)
		{
			var manage = new UserExamManage(userExamId);

			var details = manage.GetDetails();

			return details;
		}

		/// <summary>
		/// 提交用户考卷阅卷结果
		/// </summary>
		/// <param name="reviewUserId">阅卷用户ID</param>
		/// <param name="userExamId">用户考卷ID</param>
		/// <param name="questionScores">题目对应的得分集合</param>
		/// <returns></returns>
		public bool SubmitMarkingResult(long reviewUserId, long userExamId, Dictionary<long, int> questionScores)
		{
			var manager = new UserExamManage(userExamId);

			bool success = manager.SubmitMarking(reviewUserId, questionScores);

			return success;
		}

		/// <summary>
		/// 搜索学员考卷信息
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public PagerModel<UserExamSearchResultItem> Search(UserExamSearchRequest request)
		{
			var searcher = new UserExamSearch(request);

			return searcher.Search();
		}

		/// <summary>
		/// 删除用户考卷信息
		/// </summary>
		/// <param name="deleteUser">执行删除操作的用户</param>
		/// <param name="userExamId">用户考卷ID</param>
		/// <returns></returns>
		public bool Delete(long deleteUser,long userExamId)
		{
			var manage = new UserExamManage(userExamId);

			return manage.Delete(deleteUser);
		}
	}
}
