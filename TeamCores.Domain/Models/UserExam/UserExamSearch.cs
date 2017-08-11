using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using TeamCores.Data.DataAccess;
using TeamCores.Data.Entity;
using TeamCores.Domain.Services.Request;
using TeamCores.Domain.Services.Response;
using TeamCores.Models;

namespace TeamCores.Domain.Models.UserExam
{
	/// <summary>
	/// 用户考卷搜索领域业务验证失败结果枚举
	/// </summary>
	internal enum UserExamSearchFailureRule
	{
		/// <summary>
		/// 页码不是有效范围值
		/// </summary>
		[Description("页码不是有效范围值")]
		PAGE_INDEX_OUTRANGE = 1,
		/// <summary>
		/// 每页展示数不是有效范围值
		/// </summary>
		[Description("每页展示数不是有效范围值")]
		PAGE_SIZE_OUTRANGE,
	}

	/// <summary>
	/// 用户考卷搜索业务领域模型
	/// </summary>
	internal class UserExamSearch : EntityBase<long, UserExamSearchFailureRule>
	{
		#region 属性

		/// <summary>
		/// 考生ID
		/// </summary>
		public long? StudentId { get; set; }

		/// <summary>
		/// 考卷模板ID
		/// </summary>
		public long? ExamId { get; set; }

		/// <summary>
		/// 阅卷状态
		/// </summary>
		public int? Status { get; set; }

		public int PageIndex { get; set; }

		public int PageSize { get; set; }

		#endregion

		#region 构造函数

		public UserExamSearch(UserExamSearchRequest request)
		{
			if (request != null)
			{
				StudentId = request.StudentId;
				ExamId = request.ExamId;
				Status = request.Status;
				PageIndex = request.PageIndex;
				PageSize = request.PageSize;
			}
		}

		#endregion

		#region 验证

		protected override void Validate()
		{
			if (PageIndex < 1) AddBrokenRule(UserExamSearchFailureRule.PAGE_INDEX_OUTRANGE);

			if (PageSize < 1) AddBrokenRule(UserExamSearchFailureRule.PAGE_SIZE_OUTRANGE);
		}

		#endregion

		#region 操作方法

		public PagerModel<UserExamSearchResultItem> Search()
		{
			ThrowExceptionIfValidateFailure();

			PagerModel<ExamUsers> searchPager = new PagerModel<ExamUsers>
			{
				Index = PageIndex,
				Size = PageSize
			};

			//搜索结果
			searchPager = ExamUsersAccessor.GetList(searchPager,
													userId: StudentId,
													examId: ExamId,
													markingStatus: Status);

			//转换后的结果数据
			var resultData = ConvertToSearchResult(searchPager.Table);

			//处理并输出结果
			return new PagerModel<UserExamSearchResultItem>
			{
				Count = searchPager.Count,
				Index = searchPager.Index,
				Size = searchPager.Size,
				Table = resultData != null ? resultData.ToList() : new List<UserExamSearchResultItem>()
			};
		}

		/// <summary>
		/// 将<see cref="ExamUsers"/>类型转换为<see cref="UserExamSearchResultItem"/>类型数据
		/// </summary>
		/// <param name="sourceData"><see cref="ExamUsers"/>源数据</param>
		/// <returns></returns>
		public IEnumerable<UserExamSearchResultItem> ConvertToSearchResult(IEnumerable<ExamUsers> sourceData)
		{
			if (sourceData == null) yield break;

			//得到用户集合
			long[] userIds = sourceData.Select(p => p.UserId).ToArray();
			var users = UsersAccessor.GetSimpleUsers(userIds);

			//得到考卷模板集合
			long[] examIds = sourceData.Select(p => p.ExamId).ToArray();
			var exams = ExamsAccessor.GetSimpleExams(examIds);

			//循环处理
			foreach (var item in sourceData)
			{
				var user = users.FirstOrDefault(p => p.UserId == item.UserId);
				var exam = exams.FirstOrDefault(p => p.ExamId == item.ExamId);

				var result = new UserExamSearchResultItem
				{
					UserExamId = item.Id,
					UserId = item.UserId,
					ExamId = item.ExamId,
					ActualTime = item.Times,
					MarkingStatus = item.MarkingStatus,
					MarkingTime = item.MarkingTime,
					CreateTime = item.CreateTime,
					PostTime = item.PostTime,
					Score = item.Total
				};

				if (user != null)
				{
					result.UserMobile = user.Mobile;
					result.UserName = user.Name;
					result.UserTitle = user.Title;
				}

				if (exam != null)
				{
					result.ExamTitle = exam.Title;
					result.MaxTime = exam.Time;
					result.Total = exam.Total;
					result.Pass = exam.Pass;
				}

				yield return result;
			}
		}

		#endregion
	}
}
