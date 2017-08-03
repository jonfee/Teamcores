using TeamCores.Data.Entity;

namespace TeamCores.Domain.Utility
{
	/// <summary>
	/// 课程章节辅助操作工具类
	/// </summary>
	internal static class ChapterTools
    {
		/// <summary>
		/// 生成章节层级路径
		/// </summary>
		/// <param name="parent">父章节</param>
		/// <param name="currentChapterId">当前章节ID</param>
		/// <returns></returns>
		public static string CreateParentPath(Chapter parent,long currentChapterId)
		{
			string path = currentChapterId.ToString();

			if (parent != null)
			{
				path = $"{parent.ParentPath},{currentChapterId}";
			}

			return path;
		}
	}
}
