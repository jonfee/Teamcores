using System.Collections.Generic;
using System.Linq;
using TeamCores.Data.DataAccess;
using TeamCores.Domain.Infrastructure.StudyProgress;
using TeamCores.Models;

namespace TeamCores.Domain.Events
{
	/// <summary>
	/// 用户学习计划的学习进度更新事件数据状态
	/// </summary>
	internal class UserStudyProgressUpdateEventState : DomainEventState
	{
		/// <summary>
		/// 课程ID
		/// </summary>
		public long CourseId { get; set; }

		/// <summary>
		/// 学员ID
		/// </summary>
		public long? StudentId { get; set; }
	}

	/// <summary>
	/// 用户学习计划的学习进度更新事件
	/// </summary>
	internal class UserStudyProgressUpdateEvent : DomainEvent
	{
		UserStudyProgressUpdateEventState state;

		/// <summary>
		/// 实例化<see cref="UserStudyProgressUpdateEvent"/>对象实例
		/// </summary>
		/// <param name="state"></param>
		public UserStudyProgressUpdateEvent(UserStudyProgressUpdateEventState state) : base(state)
		{
			this.state = state;
		}

		public override void Execute()
		{
			Validate();

			//需要计算学习进度的学员及计划
			var studentsPlans = GetStuentPlans();

			//获取计划中的所有课程ID
			var allCourses = GetCourseIdsFor(studentsPlans?.Values);

			//所有课程对应的章节集合
			var courseChapters = CourseAccessor.GetCourseChaptersFor(allCourses);

			List<UserStudyPlanProgressModel> studentProgressList = new List<UserStudyPlanProgressModel>();

            if(studentsPlans==null)
                return;

			foreach (var item in studentsPlans)
			{
				//用户该课程相关的学习计划进度计算结果
				Dictionary<long, float> planProgressResult = CalculatePlanProgress(item.Key, item.Value, courseChapters);

				foreach (var result in planProgressResult)
				{
					studentProgressList.Add(new UserStudyPlanProgressModel
					{
						StudentId = item.Key,
						PlanId = result.Key,
						Progress = result.Value
					});
				}
			}

			//更新学习计划进度
			UserStudyPlanAccessor.UpdateProgress(studentProgressList);
		}

		/// <summary>
		/// 从学习计划关联的课程信息中读取所有课程ID
		/// </summary>
		/// <param name="plansList"></param>
		/// <returns></returns>
		private IEnumerable<long> GetCourseIdsFor(IEnumerable<List<PlanCoursesModel>> plansList)
		{
			List<long> tempIds = new List<long>();

		    if (plansList == null)
		        return tempIds;
            
			foreach (var plan in plansList)
			{
				foreach (var item in plan)
				{
					tempIds.AddRange(item.Courses);
				}
			}
			return tempIds.Distinct();
		}

		/// <summary>
		/// 获取学员及计划
		/// </summary>
		/// <returns></returns>
		private Dictionary<long, List<PlanCoursesModel>> GetStuentPlans()
		{
			Dictionary<long, List<PlanCoursesModel>> dic = new Dictionary<long, List<PlanCoursesModel>>();

			if (state.StudentId.HasValue || state.StudentId > 0)
			{
				//当前学员持有该课程的计划
				var studentPlans = StudyPlanCourseAccessor.GetStudyPlansCoursesFor(state.StudentId.Value, state.CourseId);

				dic.Add(state.StudentId.Value, studentPlans);
			}
			else
			{
				//所有持有该课程的计划
				var plans = StudyPlanCourseAccessor.GetStudyPlansCoursesFor(state.CourseId);

				var planIds = plans.Select(p => p.PlanId).ToArray();

				//啊其他取持有这些计划的学员
				var studentPlanIdsDic = UserStudyPlanAccessor.GetStudentIdsFor(planIds);

			    if (studentPlanIdsDic == null)
			        return null;

				foreach (var item in studentPlanIdsDic)
				{
					var studentId = item.Key;
					var itemPlanIds = item.Value;

					var itemPlans = plans.Where(p => itemPlanIds.Contains(p.PlanId)).ToList();

					dic.Add(studentId, itemPlans);
				}
			}

			return dic;
		}

		/// <summary>
		/// 计算出学习计划的学习进度
		/// </summary>
		/// <param name="studentId">学员用户ID</param>
		/// <param name="plans">学员的学习计划</param>
		/// <param name="chapters">学员学习过的课程章节</param>
		/// <returns></returns>
		private Dictionary<long, float> CalculatePlanProgress(long studentId, List<PlanCoursesModel> plans, Dictionary<CourseStatusModel, List<ChapterStatusModel>> chapters)
		{
			Dictionary<long, float> planProgress = new Dictionary<long, float>();

			//当前用户计划下的课程ID集合
			var studentCourseIds = GetCourseIdsFor(new[] { plans });

			//获取用户相关课程下学习过的章节ID集合
			var studiedChapterIds = StudyRecordAccessor.GetChapterIdsFor(studentId, studentCourseIds);

			//进度计算器
			StudyProgressComputer computer = null;

			//遍历计划，计算对应的计划学习进度
			foreach (var plan in plans)
			{
				var planCourseIds = plan.Courses;
				var planCourseChapters = chapters.Where(p => planCourseIds.Contains(p.Key.CourseId)).ToDictionary(k => k.Key, v => v.Value);

				computer = new StudyProgressComputer(new StudyProgressComputerState
				{
					PlanId = plan.PlanId,
					CourseChapters = planCourseChapters,
					StudiedChapterIds = studiedChapterIds
				});

				var progress = computer.Calculate();

				planProgress.Add(plan.PlanId, progress);
			}

			return planProgress;
		}
	}
}
