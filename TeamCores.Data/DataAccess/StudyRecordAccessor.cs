using System;
using System.Collections.Generic;
using System.Text;
using TeamCores.Data.Entity;
using System.Linq;

namespace TeamCores.Data.DataAccess
{
	/// <summary>
	/// 学习记录仓储服务
	/// </summary>
	public static class StudyRecordAccessor
	{
		/// <summary>
		/// 获取用户针对指定课程章节的学习记录
		/// </summary>
		/// <param name="userId">用户ID</param>
		/// <param name="chapterId">课程章节ID</param>
		/// <returns></returns>
		public static StudyRecord GetStudyRecord(long userId, long chapterId)
		{
			StudyRecord record = null;

			using (var db = new DataContext())
			{
				record = (from p in db.StudyRecord
						  where p.UserId == userId && p.ChapterId == chapterId
						  select p).FirstOrDefault();
			}

			return record;
		}

		/// <summary>
		/// 获取用户指定课程集合中学习过的章节ID集合
		/// </summary>
		/// <param name="userId">用户ID</param>
		/// <param name="courseId">课程ID</param>
		/// <returns>章节ID集合</returns>
		public static long[] GetChapterIdsFor(long userId, long courseId)
		{
			return GetChapterIdsFor(userId, new[] { courseId });
		}

		/// <summary>
		/// 获取用户指定课程集合中学习过的章节ID集合
		/// </summary>
		/// <param name="userId">用户ID</param>
		/// <param name="courseIds">课程ID集合</param>
		/// <returns>章节ID集合</returns>
		public static long[] GetChapterIdsFor(long userId, IEnumerable<long> courseIds)
		{
			long[] chapterIds = null;

			using (var db = new DataContext())
			{
				var query = from p in db.StudyRecord
							where p.UserId == userId
							select p;

				if (courseIds != null && courseIds.Count() > 0)
				{
					query = from p in query
							where courseIds.Contains(p.CourseId)
							select p;
				}

				chapterIds = (from p in query
							  select p.ChapterId).ToArray();
			}

			return chapterIds;
		}

		/// <summary>
		/// 插入一条用户学习记录
		/// </summary>
		/// <param name="record"></param>
		/// <returns></returns>
		public static bool Insert(StudyRecord record)
		{
			if (record == null) return false;

			bool success = false;

			using (var db = new DataContext())
			{
				db.StudyRecord.Add(record);

				success = db.SaveChanges() > 0;
			}

			return success;
		}

		/// <summary>
		/// 新增一次学习次数
		/// </summary>
		/// <param name="recordId">学习记录ID</param>
		/// <returns></returns>
		public static bool AddOnceTimesForStudy(long recordId)
		{
			bool success = false;

			using (var db = new DataContext())
			{
				var item = db.StudyRecord.Find(recordId);

				if (item != null)
				{
					item.ReadCount += 1;

					db.StudyRecord.Update(item);

					success = db.SaveChanges() > 0;
				}
			}

			return success;
		}

		/// <summary>
		/// 新增一次学习次数
		/// </summary>
		/// <param name="userId">学员用户ID</param>
		/// <param name="chapterIds">章节ID集合</param>
		/// <returns></returns>
		public static bool AddOnceTimesForStudy(long userId, IEnumerable<long> chapterIds)
		{
			if (chapterIds == null || chapterIds.Count() < 1) return false;

			bool success = false;

			using (var db = new DataContext())
			{
				var list = (from p in db.StudyRecord
							where p.UserId == userId && chapterIds.Contains(p.ChapterId)
							select p).ToList();

				foreach (var item in list)
				{
					if (item != null)
					{
						item.ReadCount += 1;

						db.StudyRecord.Update(item);
					}
				}

				success = db.SaveChanges() > 0;
			}

			return success;
		}
	}
}
