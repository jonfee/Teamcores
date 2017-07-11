﻿using System;

namespace TeamCores.Data.Entity
{
    /// <summary>
    /// 用户学习计划
    /// </summary>
    public class UserStudyPlan
    {
        /// <summary>
        /// 计划ID
        /// </summary>
        public long PlanId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 学习状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 学习进度
        /// </summary>
        public float Progress { get; set; }

        /// <summary>
        /// 建立时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 最后更新状态时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
