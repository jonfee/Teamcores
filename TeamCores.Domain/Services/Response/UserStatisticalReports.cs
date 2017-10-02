using System;

namespace TeamCores.Domain.Services.Response
{
    public class UserStatisticalReports
    {
        public UserInfo User { get; set; }

        public StudyPlanStatistics StudyPlan { get; set; }

        public MessageStatistics Message { get; set; }

        public ExamStatistics Exam { get; set; }
    }

    public class UserInfo
    {
        public long UserId { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public DateTime LastLoginTime { get; set; }
    }

    /// <summary>
    /// 学习计划数据统计
    /// </summary>
    public class StudyPlanStatistics
    {
        /// <summary>
        /// 未开始的计划数
        /// </summary>
        public int TotalNoStart { get; set; }

        /// <summary>
        /// 进行中的计划数
        /// </summary>
        public int TotalStudying { get; set; }

        /// <summary>
        /// 已完成的计划数
        /// </summary>
        public int TotalDone { get; set; }
    }

    /// <summary>
    /// 消息数据统计
    /// </summary>
    public class MessageStatistics
    {
        /// <summary>
        /// 未读消息数
        /// </summary>
        public int TotalNoRead { get; set; }
    }

    /// <summary>
    /// 考试数据统计
    /// </summary>
    public class ExamStatistics
    {
        /// <summary>
        /// 总练习/考卷数
        /// </summary>
        public int TotalExams { get; set; }

        /// <summary>
        /// 未交卷的考卷数
        /// </summary>
        public int TotalUnCommited
        {
            get
            {
                return TotalExams - TotalReviewed - TotalWaitReview;
            }
        }

        /// <summary>
        /// 已完成阅卷的数量
        /// </summary>
        public int TotalReviewed { get; set; }

        /// <summary>
        /// 待阅卷的数量
        /// </summary>
        public int TotalWaitReview { get; set; }
    }
}
