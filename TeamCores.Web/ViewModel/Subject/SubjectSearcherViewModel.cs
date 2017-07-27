using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamCores.Web.ViewModel.Subject
{
	/// <summary>
	/// 科目搜索器模型
	/// </summary>
    public class SubjectSearcherViewModel
    {
		public string Keyword { get; set; }

		public int? Status { get; set; }

		public int PageIndex { get; set; }

		public int PageSize { get; set; }
    }
}
