using System;
using System.Collections.Generic;
using System.Text;

namespace TeamCores.Models
{
	/// <summary>
	/// 仅仅表示章节之间层级关系的模型
	/// </summary>
	public class ChapterTier
	{
		public long ChapterId { get; set; }

		public string Title { get; set; }

		public int Status { get; set; }

		public long ParentId { get; set; }
	}
}
