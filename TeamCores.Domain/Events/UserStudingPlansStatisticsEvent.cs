using System;
using System.Collections.Generic;
using TeamCores.Data.DataAccess;
using TeamCores.Domain.Enums;
using TeamCores.Domain.Models.UserStuding;
using System.Linq;
using TeamCores.Common.Utilities;

namespace TeamCores.Domain.Events
{
    /// <summary>
    /// 用户正在进行中的学习计划统计事件数据状态
    /// </summary>
    internal class UserStudingPlansStatisticsEventState : DomainEventState
    {
        /// <summary>
        /// 学员ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 学习计划
        /// </summary>
        public long PlanId { get; set; }
    }

    /// <summary>
    /// 用户正在进行中的学习计划统计事件
    /// </summary>
    internal class UserStudingPlansStatisticsEvent : DomainEvent
    {
        /// <summary>
        /// 实例化<see cref="UserStudingPlansStatisticsEvent"/>对象实例
        /// </summary>
        /// <param name="state"></param>
        public UserStudingPlansStatisticsEvent(UserStudingPlansStatisticsEventState state) : base(state) { }

        public override void Execute()
        {
            Validate();

            var state = State as UserStudingPlansStatisticsEventState;

            //学习中的计划状态，指：未开始，学习中的状态
            var status = EnumUtility.GetEnumValues(typeof(UserStudyPlanStatus)).ToList();

            //排除已学习完成的
            status.Remove((int)UserStudyPlanStatus.COMPLETE);
            status.TrimExcess();

            //学员 学习计划数量 
            int count = UserStudyPlanAccessor.GetPlansCount(state.UserId, status);

            //更新仓储数据
            UserStudyAccessor.UpdateStudyPlans(state.UserId, count);
        }
    }
}
