﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamCores.Data.Entity
{
    /// <summary>
    /// 考卷使用用户
    /// </summary>
    [Table("ExamUsers")]
    public class ExamUsers
    {
        /// <summary>
        /// 数据ID
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 考试题目ID
        /// </summary>
        public long ExamId { get; set; }

        /// <summary>
        /// 答卷总计时间
        /// </summary>
        public int Times { get; set; }

        /// <summary>
        /// 得分
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 答案，JSON格式
        /// </summary>
        [Column(TypeName = "nvarchar(max)")]
        public string Answer { get; set; }

        /// <summary>
        /// 创建时间，也是考试开始时间
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 交卷时间
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? PostTime { get; set; }

        /// <summary>
        /// 阅卷状态
        /// </summary>
        public int MarkingStatus { get; set; }

        /// <summary>
        /// 阅卷时间
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? MarkingTime { get; set; }
    }
}
