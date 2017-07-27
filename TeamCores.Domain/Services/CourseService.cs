using System;
using System.Collections.Generic;
using System.Text;
using TeamCores.Common.Exceptions;
using TeamCores.Data.DataAccess;
using TeamCores.Data.Entity;
using TeamCores.Domain.Models.Course;

namespace TeamCores.Domain.Services
{
	/// <summary>
	/// 课程相关服务
	/// </summary>
	public class CourseService
	{
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
	}
}
