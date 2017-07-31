using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamCores.Data.Entity
{
	[Table("StudyPlanCourse")]
	public class StudyPlanCourse
    {
		/// <summary>
		/// 计划ID
		/// </summary>
		[Key]
		public long PlanId { get; set; }

		/// <summary>
		/// 课程ID
		/// </summary>
		[Key]
		public long CourseId { get; set; }

		/// <summary>
		/// 学习排序（表示建议学习的顺序）
		/// </summary>
		public int Sort { get; set; }

		[Column(TypeName = "datetime")]
		public DateTime CreateTime { get; set; }
	}
}
