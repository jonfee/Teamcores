using System;
using System.ComponentModel;
using TeamCores.Common;
using TeamCores.Data.DataAccess;
using TeamCores.Data.Entity;
using TeamCores.Domain.Enums;

namespace TeamCores.Domain.Models.UserStuding
{
	/// <summary>
	/// 学习记录领域业务验证错误结果枚举
	/// </summary>
	internal enum StudingRecordFailureRule
	{
		/// <summary>
		/// 课程章节不存在
		/// </summary>
		[Description("课程章节不存在")]
		CHAPTER_NOT_EXISTS,
		/// <summary>
		/// 课程章节已被禁用
		/// </summary>
		[Description("课程章节已被禁用")]
		CHAPTER_STATUS_IS_DISABLED
	}

	/// <summary>
	/// 学习记录领域业务模型
	/// </summary>
	internal class StudingRecord : EntityBase<long, StudingRecordFailureRule>
	{
		#region  属性

		/// <summary>
		/// 学员ID
		/// </summary>
		public readonly long StudentId;

		/// <summary>
		/// 章节信息
		/// </summary>
		public readonly Data.Entity.Chapter Chapter;

		/// <summary>
		/// 课程章节的学习情况
		/// </summary>
		public StudyRecord Record { get; private set; }

		#endregion

		#region  构造函数

		/// <summary>
		/// 实例化<see cref="StudingRecord"/>对象实例
		/// </summary>
		/// <param name="studentId">学员ID</param>
		/// <param name="chapterId">学习的课程章节ID</param>
		public StudingRecord(long studentId, long chapterId)
		{
			StudentId = studentId;

			//获取章节信息
			Chapter = ChapterAccessor.Get(chapterId);

			InitData();
		}

		/// <summary>
		/// 实例化<see cref="StudingRecord"/>对象实例
		/// </summary>
		/// <param name="studentId">学员ID</param>
		/// <param name="chapter">学习的课程章节</param>
		public StudingRecord(long studentId, Data.Entity.Chapter chapter)
		{
			StudentId = studentId;
			Chapter = chapter;

			InitData();
		}

		private void InitData()
		{
			if (Chapter != null)
			{
				//尝试获取该课程章节的学习记录
				Record = StudyRecordAccessor.GetStudyRecord(StudentId, Chapter.ChapterId);
				ID = Record != null ? Record.RecordId : IDProvider.NewId;
			}
		}

		#endregion

		#region 验证

		protected override void Validate()
		{
			if (Chapter == null)
			{
				AddBrokenRule(StudingRecordFailureRule.CHAPTER_NOT_EXISTS);
			}
			else if (Chapter.Status == (int)ChapterStatus.DISABLED)
			{
				AddBrokenRule(StudingRecordFailureRule.CHAPTER_STATUS_IS_DISABLED);
			}
		}

		#endregion

		#region 操作方法

		/// <summary>
		/// 保存学习记录
		/// </summary>
		/// <returns></returns>
		public bool Save()
		{
			bool success = false;

			bool isNew = Record == null;

			if (isNew)
			{
				success = Add();
			}
			else
			{
				success = Update();
			}

			return success;
		}

		/// <summary>
		/// 添加学习记录
		/// </summary>
		/// <returns></returns>
		private bool Add()
		{
			var record = new StudyRecord
			{
				RecordId = ID,
				UserId = StudentId,
				CourseId = Chapter.CourseId,
				ChapterId = Chapter.ChapterId,
				ReadCount = 1,
				CreateTime = DateTime.Now,
				UpdateTime = DateTime.Now
			};

			return StudyRecordAccessor.Insert(record);
		}

		/// <summary>
		/// 更新学习次数
		/// </summary>
		/// <returns></returns>
		private bool Update()
		{
			return StudyRecordAccessor.AddOnceTimesForStudy(ID);
		}

		#endregion
	}
}
