using System;
using TeamCores.Common.Exceptions;
using TeamCores.Data.DataAccess;
using TeamCores.Data.Entity;
using TeamCores.Domain.Models.Course;
using TeamCores.Models;

namespace TeamCores.Domain.Services
{
	/// <summary>
	/// 课程相关服务
	/// </summary>
	public class CourseService
	{
		/// <summary>
		/// 添加新课程
		/// </summary>
		/// <param name="newCourse">新课程信息</param>
		/// <returns></returns>
		public bool Add(NewCourse newCourse)
		{
			if (newCourse == null) throw new TeamCoresException(nameof(newCourse), "新课程对象不能为NULL。");

			newCourse.ThrowExceptionIfValidateFailure();

			Course course = new Course
			{
				CourseId = newCourse.ID,
				UserId = newCourse.UserId,
				SubjectId = newCourse.SubjectId,
				Title = newCourse.Title,
				Content = newCourse.Content,
				Objective = newCourse.Objective,
				Image = newCourse.Image,
				Remarks = newCourse.Remarks,
				Status = newCourse.Status,
				CreateTime = DateTime.Now
			};

			return CourseAccessor.Insert(course);
		}

		/// <summary>
		/// 设置课程为“启用”状态
		/// </summary>
		/// <param name="courseId"></param>
		/// <returns></returns>
		public bool SetEnable(long courseId)
		{
			CourseEditor course = new CourseEditor(courseId);

			return course.SetEnable();
		}

		/// <summary>
		/// 设置课程为“禁用”状态
		/// </summary>
		/// <param name="courseId"></param>
		/// <returns></returns>
		public bool SetDisable(long courseId)
		{
			CourseEditor course = new CourseEditor(courseId);

			return course.SetDisable();
		}

		/// <summary>
		/// 删除课程
		/// </summary>
		/// <param name="courseId"></param>
		/// <returns></returns>
		public bool Delete(long courseId)
		{
			CourseEditor course = new CourseEditor(courseId);

			return course.Delete();
		}

		/// <summary>
		/// 编辑课程
		/// </summary>
		/// <param name="courseId">课程ID</param>
		/// <param name="state">编辑过的资料</param>
		/// <returns></returns>
		public bool Modify(long courseId, CourseModifiedState state)
		{
			CourseEditor course = new CourseEditor(courseId);

			return course.ModifyTo(state);
		}

		/// <summary>
		/// 搜索课程信息
		/// </summary>
		/// <param name="pageSize"></param>
		/// <param name="pageIndex"></param>
		/// <param name="keyword"></param>
		/// <param name="subjectId"></param>
		/// <param name="status"></param>
		/// <returns></returns>
		public PagerModel<Course> Search(int pageSize, int pageIndex, string keyword, long? subjectId, int? status)
		{
			CourseSearch search = new CourseSearch(pageIndex, pageSize, keyword, subjectId, status);

			return search.Search();
		}
	}
}
