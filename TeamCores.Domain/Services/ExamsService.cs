using System;
using System.Collections.Generic;
using System.Text;
using TeamCores.Domain.Models.Exams;
using TeamCores.Domain.Services.Request;

namespace TeamCores.Domain.Services
{
	/// <summary>
	/// 考卷领域业务服务
	/// </summary>
	public class ExamsService
	{
		/// <summary>
		/// 新增考卷
		/// </summary>
		/// <param name="request">新考卷请求</param>
		/// <returns></returns>
		public bool Add(NewExamsRequest request)
		{
			var exams = new NewExams(request);

			return exams.Save();
		}
	}
}
