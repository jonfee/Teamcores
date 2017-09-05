using System;
using System.Collections.Generic;
using System.Linq;
using TeamCores.Data.DataAccess;
using TeamCores.Domain.Infrastructure.StudyProgress;
using TeamCores.Domain.Models.StudyPlan;
using TeamCores.Domain.Models.UserStuding;
using TeamCores.Domain.Services.Response;
using TeamCores.Models;

namespace TeamCores.Domain.Services
{
    public class UserStudyPlanService
    {
        /// <summary>
        /// 搜索用户的学习计划信息
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="studentId">学员ID</param>
        /// <param name="studyStatus">学习状态，为NULL时忽略</param>
        /// <returns></returns>
        public PagerModel<UserStudyPlanSearchResultItem> Search(int pageSize, int pageIndex, long? studentId, int? studyStatus)
        {
            UserStudyPlanSearch search = new UserStudyPlanSearch(pageIndex, pageSize, studentId, studyStatus);

            var searchPager = search.Search();

            var data = TransferData(searchPager.Table);

            return new PagerModel<UserStudyPlanSearchResultItem>
            {
                Index = searchPager.Index,
                Size = searchPager.Size,
                Count = searchPager.Count,
                Table = data
            };
        }

        /// <summary>
        /// 获取用户学习计划执行详细
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="planId">计划ID</param>
        /// <returns></returns>
        public UserStudyPlanDetails GetPlanDetails(long userId, long planId)
        {
            //学习计划
            var plan = new StudyPlanManage(planId);

            //指定学习对该计划的实施信息
            var userPlan = new UserStudyPlanManage(userId, planId);

            //计算用户课程学习进度
            foreach(var course in plan.Courses)
            {
                var calc = new CourseStudingProgressComputer(userId, course.CourseId);
                
                course.Progress = calc.Calculate();
            }

            return new UserStudyPlanDetails
            {
                UserId = userId,
                PlanId = plan.StudyPlan.PlanId,
                Title = plan.StudyPlan.Title,
                Content = plan.StudyPlan.Content,
                CreatorId = plan.Creator.UserId,
                CreatorName = plan.Creator.Username,
                PlanStatus = plan.StudyPlan.Status,
                StudentCount = plan.StudyPlan.Student,
                CreateTime = plan.StudyPlan.CreateTime,
                StudyStatus = userPlan.Plan.Status,
                Progress = userPlan.Plan.Progress,
                LastStudyTime = userPlan.Plan.UpdateTime,
                Courses = plan.Courses,
                Students = plan.Students
            };
        }

        /// <summary>
        /// 转换数据
        /// </summary>
        /// <param name="userPlans">用户学习计划情况集合</param>
        /// <returns></returns>
        private List<UserStudyPlanSearchResultItem> TransferData(IEnumerable<Data.Entity.UserStudyPlan> userPlans)
        {
            if (userPlans == null || userPlans.Count() < 1) return new List<UserStudyPlanSearchResultItem>();

            //用户学习计划ID集合
            var planIds = userPlans.Select(p => p.PlanId).ToArray();

            //获取所有的学习计划
            var plans = StudyPlanAccessor.GetList(planIds);

            if (plans == null || plans.Count() < 1) return null;

            //计划制定者ID集合
            var creatorIds = plans.Select(p => p.UserId);
            //获取制定者名称集合
            var creatorNames = UsersAccessor.GetUsernames(creatorIds);

            var list = new List<UserStudyPlanSearchResultItem>();

            foreach (var plan in plans)
            {
                //用户学习计划执行情况
                var userPlan = userPlans.FirstOrDefault(p => p.PlanId == plan.PlanId);
                //制定者名称
                var creatorName = creatorNames[plan.UserId];

                list.Add(new UserStudyPlanSearchResultItem
                {
                    UserId = userPlan.UserId,
                    PlanId = plan.PlanId,
                    Title = plan.Title,
                    Content = plan.Content,
                    CreatorId = plan.UserId,
                    Creator = creatorName,
                    StudentCount = plan.Student,
                    PlanStatus = plan.Status,
                    CreateTime = plan.CreateTime,
                    StudyStatus = userPlan.Status,
                    Progress = userPlan.Progress,
                    LastStudyTime = userPlan.UpdateTime
                });
            }

            return list;
        }
    }
}
