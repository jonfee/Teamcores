﻿using TeamCores.Data.Entity;
using TeamCores.Domain.Models.Exams;
using TeamCores.Domain.Services.Request;
using TeamCores.Domain.Services.Response;
using TeamCores.Models;

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

		/// <summary>
		/// 设置考卷状态为启用
		/// </summary>
		/// <param name="examsId"></param>
		/// <returns></returns>
		public bool SetEnable(long examsId)
		{
			var exams = new ExamsManage(examsId);

			return exams.SetEnable();
		}

		/// <summary>
		/// 设置考卷状态为禁用
		/// </summary>
		/// <param name="examsId"></param>
		/// <returns></returns>
		public bool SetDisable(long examsId)
		{
			var exams = new ExamsManage(examsId);

			return exams.SetDisable();
		}

		/// <summary>
		/// 更新考卷信息
		/// </summary>
		/// <param name="examsId">考卷ID</param>
		/// <param name="state">要更新的数据</param>
		/// <returns></returns>
		public bool ModifyTo(long examsId, ExamsModifyState state)
		{
			var exams = new ExamsManage(examsId);

			return exams.ModifyTo(state);
		}

		/// <summary>
		/// 搜索考卷信息
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public PagerModel<Data.Entity.Exams> Search(ExamsSearchRequest request)
		{
			var searcher = new ExamsSearch(request);

			return searcher.Search();
		}

		/// <summary>
		/// 获取考卷信息
		/// </summary>
		/// <param name="examsId">考卷ID</param>
		/// <returns></returns>
		public Exams GetExams(long examsId)
		{
			var exams = new ExamsManage(examsId);

			return exams.Exams;
		}

		/// <summary>
		/// 获取考卷详细信息
		/// </summary>
		/// <param name="examsId">考卷ID</param>
		/// <returns></returns>
		public ExamsDetails GetDetails(long examsId)
		{
			var exams = new ExamsManage(examsId);

			return exams.ConvertToExamsDetails();
		}
	}
}
