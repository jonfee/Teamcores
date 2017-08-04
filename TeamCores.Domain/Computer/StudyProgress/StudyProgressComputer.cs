using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamCores.Domain.Enums;
using TeamCores.Models;

namespace TeamCores.Domain.Computer.StudyProgress
{
	/// <summary>
	/// 计算学习进度提供的数据
	/// </summary>
	internal class StudyProgressComputerState
	{
		/// <summary>
		/// 计划ID
		/// </summary>
		public long PlanId { get; set; }

		/// <summary>
		/// 当前计划下的课程及对应的章节
		/// </summary>
		public Dictionary<CourseStatusModel, List<ChapterStatusModel>> CourseChapters { get; set; }

		/// <summary>
		/// 用户学习过的课程章节
		/// </summary>
		public long[] StudiedChapterIds { get; set; }
	}

	/// <summary>
	/// 学习进度计算器
	/// </summary>
	internal class StudyProgressComputer
	{
		#region 属性

		/// <summary>
		/// 为计算学习进度提供的数据
		/// </summary>
		public readonly StudyProgressComputerState State;

		#endregion

		/// <summary>
		/// 初始化<see cref="StudyProgressComputer"/>实例
		/// </summary>
		/// <param name="state"></param>
		public StudyProgressComputer(StudyProgressComputerState state)
		{
			State = state;
		}

		/// <summary>
		/// 计算进度
		/// </summary>
		/// <returns></returns>
		public float Calculate()
		{
			if (State == null) return 0;
			if (State.StudiedChapterIds == null || State.StudiedChapterIds.Length < 1) return 0;
			if (State.CourseChapters == null || State.CourseChapters.Count() < 1) return 0;

			//筛选出状态正常的课程
			var data = from p in State.CourseChapters
					   where p.Key.Status == (int)CourseStatus.ENABLED
					   select p.Value;

			//筛选出状态正常的课程下的有效章节
			var chapters = new Func<List<long>>(() =>
			{
				List<long> chapterIds = new List<long>();

				foreach (var cha in data)
				{
					if (cha != null)
					{
						chapterIds.AddRange(cha.Where(p => p.Status == (int)ChapterStatus.ENABLED).Select(p => p.ChapterId));
					}
				}

				return chapterIds;
			}).Invoke();

			//用户学习过的章节
			var studiedChapterIds = State.StudiedChapterIds.Where(p => chapters.Contains(p));

			float total = chapters.Count();
			float studied = studiedChapterIds != null ? studiedChapterIds.Count() : 0;

			if (total > 0)
			{
				return (float)Math.Round(studied / total, 2, MidpointRounding.ToEven);
			}
			else
			{
				return 0F;
			}
		}
	}
}
