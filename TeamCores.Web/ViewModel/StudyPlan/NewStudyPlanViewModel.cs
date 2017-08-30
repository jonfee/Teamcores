using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamCores.Web.ViewModel.StudyPlan
{
	/// <summary>
	/// 新学习计划视图模型
	/// </summary>
    public class NewStudyPlanViewModel
    {
		/// <summary>
		/// 学习计划标题
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// 计划说明
		/// </summary>
		public string Content { get; set; }

		/// <summary>
		/// 关联的学员ID集合
		/// </summary>
		public string Students { get; set; }

		/// <summary>
		/// 关联的课程ID集合
		/// </summary>
		public string Courses { get; set; }
	}
}
