﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamCores.Data.Entity
{
    /// <summary>
    /// 考卷
    /// </summary>
    [Table("Exams")]
    public class Exams
    {
		/// <summary>
		/// 考试题目ID
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public long ExamId { get; set; }

        /// <summary>
        /// 考试类型（考试，练习）
        /// </summary>
        public int ExamType { get; set; }

		/// <summary>
		/// 关联的课程ID集合
		/// </summary>
		[Column(TypeName ="varchar(max)")]
		public string CourseIds { get; set; }

        /// <summary>
        /// 考卷标题
        /// </summary>
        [Column(TypeName = "nvarchar(50)")]
        public string Title { get; set; }

        /// <summary>
        /// 考试目标备注说明
        /// </summary>
        [Column(TypeName = "nvarchar(250)")]
        public string Remarks { get; set; }

        /// <summary>
        /// 考题ID集合
        /// </summary>
        [Column(TypeName = "nvarchar(max)")]
        public string Questions { get; set; }

        /// <summary>
        /// 考试时间（单位：分钟）
        /// </summary>
        public int Time { get; set; }

        /// <summary>
        /// 总分
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 及格分
        /// </summary>
        public int Pass { get; set; }

        /// <summary>
        /// 考卷状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 考卷创建用户
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 考卷使用次数
        /// </summary>
        public int UseCount { get; set; }

        /// <summary>
        /// 答卷数量
        /// </summary>
        public int Answers { get; set; }

        /// <summary>
        /// 单选题目数量
        /// </summary>
        public int Radio { get; set; }

        /// <summary>
        /// 单选题总分
        /// </summary>
        public int RedioTotal { get; set; }

        /// <summary>
        /// 多选题目数量
        /// </summary>
        public int Multiple { get; set; }

        /// <summary>
        /// 多选总分
        /// </summary>
        public int MultipleTotal { get; set; }

        /// <summary>
        /// 判断题
        /// </summary>
        public int Judge { get; set; }

        /// <summary>
        /// 判断题总分
        /// </summary>
        public int JudgeTotal { get; set; }

        /// <summary>
        /// 填空题
        /// </summary>
        public int Filling { get; set; }

        /// <summary>
        /// 填空题总分
        /// </summary>
        public int FillingTotal { get; set; }

        /// <summary>
        /// 问答题数
        /// </summary>
        public int Ask { get; set; }

        /// <summary>
        /// 问答题总分
        /// </summary>
        public int AskTotal { get; set; }

        /// <summary>
        /// 建立时间
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 可使用开始时间,null不限制
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束使用时间,null不限制
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? EndTime { get; set; }
    }
}
