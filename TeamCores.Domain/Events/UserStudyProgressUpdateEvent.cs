using System.Collections.Generic;
using System.Linq;
using TeamCores.Data.DataAccess;
using TeamCores.Domain.Computer.StudyProgress;

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
        public long StudentId { get; set; }
    }

    /// <summary>
    /// 用户学习计划的学习进度更新事件
    /// </summary>
    internal class UserStudyProgressUpdateEvent : DomainEvent
    {
        public UserStudyProgressUpdateEvent(UserStudyProgressUpdateEventState state) : base(state) { }

        public override void Execute()
        {
            var state = State as UserStudyProgressUpdateEventState;

            Validate();

            //该课程相关的用户学习计划进度计算结果
            Dictionary<long, float> planProgressResult = CalculatePlanProgress(state.StudentId, state.CourseId);

            //更新学习计划进度
            UserStudyPlanAccessor.UpdateProgress(state.StudentId, planProgressResult);
        }

        /// <summary>
        /// 从学习计划关联的课程信息中读取所有课程章节
        /// </summary>
        /// <param name="planCourseDic"></param>
        /// <returns></returns>
        private IEnumerable<long> GetCourseIdsFor(Dictionary<long, long[]> planCourseDic)
        {
            List<long> tempIds = new List<long>();
            foreach (var ids in planCourseDic.Values)
            {
                tempIds.AddRange(ids);
            }
            return tempIds.Distinct();
        }

        /// <summary>
        /// 计算出学习计划的学习进度
        /// </summary>
        /// <param name="studentId">学员用户ID</param>
        /// <param name="courseId">当前课程ID</param>
        /// <returns></returns>
        private Dictionary<long, float> CalculatePlanProgress(long studentId, long courseId)
        {
            //进度计算器
            StudyProgressComputer computer = null;

            Dictionary<long, float> planProgress = new Dictionary<long, float>();

            //找出学习计划中包含该课程的计划下的关联课程集合
            var planCourseDic = StudyPlanCourseAccessor.GetStudyPlansCoursesFor(studentId, courseId);

            //所有课程ID
            var courseIds = GetCourseIdsFor(planCourseDic);

            //所有课程对应的章节集合
            var courseChapters = CourseAccessor.GetCourseChaptersFor(courseIds);

            //获取用户相关课程下学习过的章节ID集合
            var studiedChapterIds = StudyRecordAccessor.GetChapterIdsFor(studentId, courseIds);

            //遍历计划，计算对应的计划学习进度
            foreach (var planId in planCourseDic.Keys)
            {
                var planCourseIds = planCourseDic[planId];
                var planCourseChapters = courseChapters.Where(p => planCourseIds.Contains(p.Key.CourseId)).ToDictionary(k => k.Key, v => v.Value);

                computer = new StudyProgressComputer(new StudyProgressComputerState
                {
                    PlanId = planId,
                    CourseChapters = planCourseChapters,
                    StudiedChapterIds = studiedChapterIds
                });

                var progress = computer.Calculate();

                planProgress.Add(planId, progress);
            }

            return planProgress;
        }
    }
}
