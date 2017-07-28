﻿using System;
using System.Collections.Generic;
using System.Text;
using TeamCores.Data.Entity;

namespace TeamCores.Data.DataAccess
{
	/// <summary>
	/// 课程存储服务
	/// </summary>
	public static class CourseAccessor
	{
		/// <summary>
		/// 插入课程
		/// </summary>
		/// <param name="course"></param>
		/// <returns></returns>
		public static bool Insert(Course course)
		{
			if (course == null) return false;

			using (var db = new DataContext())
			{
				db.Course.Add(course);

				return db.SaveChanges() > 0;
			}
		}
		
		/// <summary>
		/// 获取课程
		/// </summary>
		/// <param name="courseId">课程ID</param>
		/// <returns></returns>
		public static Course Get(long courseId)
		{
			using (var db = new DataContext())
			{
				return db.Course.Find(courseId);
			}
		}

		/// <summary>
		/// 设置状态
		/// </summary>
		/// <param name="courseId">课程ID</param>
		/// <param name="status">状态</param>
		/// <returns></returns>
		public static bool SetStatus(long courseId, int status)
		{
			using (var db = new DataContext())
			{
				var item = db.Course.Find(courseId);

				if (item == null) return false;

				item.Status = status;

				db.Course.Update(item);

				return db.SaveChanges() > 0;
			}
		}

		/// <summary>
		/// 更新信息
		/// </summary>
		/// <param name="courseId">课程ID</param>
		/// <param name="subjectId">所属科目ID</param>
		/// <param name="title">课程标题</param>
		/// <param name="image">课程封面图片</param>
		/// <param name="content">内容</param>
		/// <param name="remarks">摘要</param>
		/// <param name="objective">学习目标</param>
		/// <param name="status">状态</param>
		/// <returns></returns>
		public static bool Update(long courseId, long subjectId, string title, string image, string content, string remarks, string objective, int status)
		{
			using (var db = new DataContext())
			{
				var item = db.Course.Find(courseId);

				if (item == null) return false;

				item.SubjectId = subjectId;
				item.Title = title;
				item.Image = image;
				item.Content = content;
				item.Remarks = remarks;
				item.Objective = objective;
				item.Status = status;

				db.Course.Update(item);

				return db.SaveChanges() > 0;
			}
		}
	}
}
