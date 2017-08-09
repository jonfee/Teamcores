using System;
using System.Collections.Generic;
using System.Text;
using TeamCores.Domain.Models.Exams;
using TeamCores.Domain.Models.UserExam;
using TeamCores.Domain.Services.Request;
using TeamCores.Domain.Services.Response;

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

			//将新参考试卷到数据库
			UserExamInitRequest request = null;

			var userExamInit = new UserExamInit(request);
			bool success = userExamInit.Save();

			if (success)
			{
				return newExamPaper;
			}

			return null;
		}
	}
}
