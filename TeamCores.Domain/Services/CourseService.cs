using TeamCores.Data.Entity;
using TeamCores.Domain.Models.Course;
using TeamCores.Domain.Services.Response;
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
		public bool Add(long userId, long subjectId, string title, string image, string content, string remarks, string objective)
		{
			NewCourse course = new NewCourse
			{
				UserId = userId,
				SubjectId = subjectId,
				Content = content,
				Image = image,
				Objective = objective,
				Remarks = remarks,
				Title = title
			};

			return course.Save();
		}
		
		/// <summary>
		/// 获取课程信息
		/// </summary>
		/// <param name="courseId"></param>
		/// <returns></returns>
		public Course GetCourse(long courseId)
		{
			var course = new CourseManage(courseId);

			return course.Course;
		}

		/// <summary>
		/// 获取课程详细信息
		/// </summary>
		/// <param name="courseId">课程ID</param>
		/// <returns></returns>
		public CourseDetails GetDetails(long courseId)
		{
			var course = new CourseManage(courseId);

			var details = course.ConvertToCourseDetails();

			return details;
		}

		/// <summary>
		/// 设置课程为“启用”状态
		/// </summary>
		/// <param name="courseId"></param>
		/// <returns></returns>
		public bool SetEnable(long courseId)
		{
			CourseManage course = new CourseManage(courseId);

			return course.SetEnable();
		}

		/// <summary>
		/// 设置课程为“禁用”状态
		/// </summary>
		/// <param name="courseId"></param>
		/// <returns></returns>
		public bool SetDisable(long courseId)
		{
			CourseManage course = new CourseManage(courseId);

			return course.SetDisable();
		}

		/// <summary>
		/// 删除课程
		/// </summary>
		/// <param name="courseId"></param>
		/// <returns></returns>
		public bool Delete(long courseId)
		{
			CourseManage course = new CourseManage(courseId);

			return course.Delete();
		}

		/// <summary>
		/// 编辑课程
		/// </summary>
		/// <param name="courseId">课程ID</param>
		/// <param name="state">编辑过的资料</param>
		/// <returns></returns>
		public bool Modify(long courseId, long subjectId, string title, string image, string content, string remarks, string objective, int status)
		{
			CourseModifiedState state = new CourseModifiedState
			{
				Content = content,
				Image = image,
				Objective = objective,
				Remarks = remarks,
				Status = status,
				SubjectId = subjectId,
				Title = title
			};

			CourseManage course = new CourseManage(courseId);

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
