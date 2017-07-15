using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamCores.Data.Entity
{
    /// <summary>
    /// 用户学习情况
    /// </summary>
    [Table("UserStudy")]
    public class UserStudy
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long UserId { get; set; }

        /// <summary>
        /// 学完课程总数
        /// </summary>
        public int ReadCount { get; set; }

        /// <summary>
        /// 学习时长，单位分钟
        /// </summary>
        public int StudyTimes { get; set; }

        /// <summary>
        /// 答题数（考试和练习都算）
        /// </summary>
        public int Answers { get; set; }

        /// <summary>
        /// 练习考卷次数
        /// </summary>
        public int TestExams { get; set; }

        /// <summary>
        /// 进行中的学习计划数量
        /// </summary>
        public int StudyPlans { get; set; }

        /// <summary>
        /// 平均得分
        /// </summary>
        public int Average { get; set; }
    }
}
