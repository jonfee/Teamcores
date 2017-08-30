using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TeamCores.Data.DataAccess;
using TeamCores.Domain.Enums;
using TeamCores.Domain.Services.Response;

namespace TeamCores.Domain.Models.StudyPlan
{
	internal enum StudyPlanEditFailureRule
	{
		/// <summary>
		/// 学习计划不存在
		/// </summary>
		[Description("学习计划不存在")]
		STUDYPLAN_NOT_EXISTS = 1,
		/// <summary>
		/// 状态不能设置为“启用”
		/// </summary>
		[Description("状态不能设置为“启用”")]
		STATUS_CANNOT_SET_TO_ENABLE,
		/// <summary>
		/// 状态不能设置为“禁用”
		/// </summary>
		[Description("状态不能设置为“禁用”")]
		STATUS_CANNOT_SET_TO_DISABLE,
		/// <summary>
		/// 删除操作不被允许
		/// </summary>
		[Description("删除操作不被允许")]
		CANNOT_DELETE,
		/// <summary>
		/// 编辑操作不被允许
		/// </summary>
		[Description("编辑操作不被允许")]
		CANNOT_EDIT,
		/// <summary>
		/// 该学员不在当前学习计划内
		/// </summary>
		[Description("该学员不在当前学习计划内")]
		STUDENT_NOT_EXISTS
	}

	/// <summary>
	/// 学员信息
	/// </summary>
	public class Student
	{
		public long UserId { get; set; }

		/// <summary>
		/// 账户名
		/// </summary>
		public string Username { get; set; }

		/// <summary>
		/// 电子邮件
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// 电话号码
		/// </summary>
		public string Mobile { get; set; }

		/// <summary>
		/// 用户名称
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 头衔
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// 学习状态
		/// </summary>
		public int StudyStatus { get; set; }

		/// <summary>
		/// 学习进度
		/// </summary>
		public float Progress { get; set; }

		/// <summary>
		/// 最后一次开始学习的时间
		/// </summary>
		public DateTime? LastStudyTime { get; set; }
	}

	/// <summary>
	/// 学习计划的课程信息
	/// </summary>
	public class CourseInfo
	{
		public long CourseId { get; set; }

		/// <summary>
		/// 归属科目
		/// </summary>
		public long SubjectId { get; set; }

		/// <summary>
		/// 课程标题
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// 课程封面图片
		/// </summary>
		public string Image { get; set; }

		/// <summary>
		/// 摘要
		/// </summary>
		public string Remarks { get; set; }

		/// <summary>
		/// 内容
		/// </summary>
		public string Content { get; set; }

		/// <summary>
		/// 学习目标
		/// </summary>
		public string Objective { get; set; }

		/// <summary>
		/// 课程状态
		/// </summary>
		public int Status { get; set; }

		/// <summary>
		/// 在学习计划课程中的排序（表示建议学习的顺序）
		/// </summary>
		public int Sort { get; set; }
	}

	internal class StudyPlanManage : EntityBase<long, StudyPlanEditFailureRule>
	{
		#region 属性

		/// <summary>
		/// 学习计划数据对象
		/// </summary>
		public readonly Data.Entity.StudyPlan StudyPlan;

		private Data.Entity.Users creator;
		/// <summary>
		/// 计划制定者信息
		/// </summary>
		public Data.Entity.Users Creator
		{
			get
			{
				if (creator == null && StudyPlan != null)
				{
					creator = UsersAccessor.Get(StudyPlan.UserId);
				}

				return creator;
			}
		}

		private List<Student> students;
		/// <summary>
		/// 学员集合
		/// </summary>
		public List<Student> Students
		{
			get
			{
				if (students == null)
				{
					students = GetStudents();
				}

				return students;
			}
		}

		private List<CourseInfo> courses;
		/// <summary>
		/// 课程集合
		/// </summary>
		public List<CourseInfo> Courses
		{
			get
			{
				if (courses == null)
				{
					courses = GetCourses();
				}

				return courses;
			}
		}

		#endregion

		#region 构造函数

		public StudyPlanManage(long planId)
		{
			ID = planId;
			StudyPlan = StudyPlanAccessor.Get(planId);
		}

		public StudyPlanManage(Data.Entity.StudyPlan studyPlan)
		{
			if (studyPlan != null)
			{
				ID = studyPlan.PlanId;
				StudyPlan = studyPlan;
			}
		}

		#endregion

		#region 验证
		protected override void Validate()
		{
			//学习计划不存在
			if (StudyPlan == null) AddBrokenRule(StudyPlanEditFailureRule.STUDYPLAN_NOT_EXISTS);
		}

		#endregion

		#region 操作方法

		public bool CanSetEnable()
		{
			return StudyPlan != null && StudyPlan.Status == (int)StudyPlanStatus.DISABLED;
		}

		public bool CanSetDisable()
		{
			return StudyPlan != null && StudyPlan.Status == (int)StudyPlanStatus.ENABLED;
		}

		public bool CanModify()
		{
			return false;
		}

		public bool CanDelete()
		{
			return false;
		}

		public bool SetEnable()
		{
			ThrowExceptionIfValidateFailure(() =>
			{
				if (!CanSetEnable()) AddBrokenRule(StudyPlanEditFailureRule.STATUS_CANNOT_SET_TO_ENABLE);
			});

			return StudyPlanAccessor.SetStatus(ID, (int)StudyPlanStatus.ENABLED);
		}

		public bool SetDisable()
		{
			ThrowExceptionIfValidateFailure(() =>
			{
				if (!CanSetDisable()) AddBrokenRule(StudyPlanEditFailureRule.STATUS_CANNOT_SET_TO_DISABLE);
			});

			return StudyPlanAccessor.SetStatus(ID, (int)StudyPlanStatus.DISABLED);
		}

		/// <summary>
		/// 获取并转换为<see cref="StudyPlanDetails"/>类型数据对象
		/// </summary>
		/// <returns></returns>
		public StudyPlanDetails ConvertToStudyPlanDetails()
		{
			if (StudyPlan == null) return null;

			var details = new StudyPlanDetails
			{
				PlanId = ID,
				Title = StudyPlan.Title,
				Content = StudyPlan.Content,
				Status = StudyPlan.Status,
				StudentCount = StudyPlan.Student,
				UserId = StudyPlan.UserId,
				UserName = Creator.Username,
				CreateTime = StudyPlan.CreateTime,
				Students = Students,
				Courses = Courses
			};

			return details;
		}

		/// <summary>
		/// 获取指定用户对计划的学习情况
		/// </summary>
		/// <param name="studentId">学员ID</param>
		/// <returns></returns>
		public StudentPlanStudingDetails GetStudentStudingDetails(long studentId)
		{
			if (StudyPlan == null) return null;

			var student = GetStudent(studentId);

			var details = new StudentPlanStudingDetails
			{
				PlanId = ID,
				Title = StudyPlan.Title,
				Content = StudyPlan.Content,
				Status = StudyPlan.Status,
				StudentCount = StudyPlan.Student,
				UserId = StudyPlan.UserId,
				CreateTime = StudyPlan.CreateTime,
				Student = student,
				Courses = Courses
			};

			return details;
		}

		/// <summary>
		/// 获取指定学员的学习进度信息
		/// </summary>
		/// <returns></returns>
		private Student GetStudent(long studentId)
		{
			Student student = null;

			if (Students != null)
			{
				student = Students.FirstOrDefault(p => p.UserId == studentId);
			}
			else
			{
				//该学员的学习计划
				var userPlan = UserStudyPlanAccessor.Get(ID, studentId);

				//学习信息
				var user = UsersAccessor.Get(studentId);

				ThrowExceptionIfValidateFailure(() =>
				{
					if (userPlan == null) AddBrokenRule(StudyPlanEditFailureRule.STUDENT_NOT_EXISTS);
				});

				return new Student
				{
					UserId = studentId,
					Email = user.Email,
					Mobile = user.Mobile,
					Username = user.Username,
					Name = user.Name,
					Title = user.Title,
					StudyStatus = userPlan.Status,
					Progress = userPlan.Progress,
					LastStudyTime = userPlan.UpdateTime
				};
			}

			return student;
		}

		/// <summary>
		/// 获取学员集合
		/// </summary>
		/// <returns></returns>
		private List<Student> GetStudents()
		{
			ThrowExceptionIfValidateFailure();

			var tempStudents = new List<Student>();

            //获取学员学习计划集合
            var studentPlans = UserStudyPlanAccessor.GetStudentStudyPlans(ID);

            //学员ID集合
            var studentIds = studentPlans.Select(p => p.UserId).ToArray();

            //根据学员ID集合获取学员信息
            var students = UsersAccessor.GetUserList(studentIds);

            foreach (var user in students)
            {
                var plan = studentPlans.FirstOrDefault(p => p.UserId == user.UserId);

                tempStudents.Add(new Student
                {
                    UserId = user.UserId,
                    Email = user.Email,
                    Mobile = user.Mobile,
                    Title = user.Title,
                    Name = user.Name,
                    Username = user.Username,
                    StudyStatus = plan.Status,
                    Progress = plan.Progress,
                    LastStudyTime = plan.UpdateTime
                });
            }

            return tempStudents;
		}

		/// <summary>
		/// 获取计划中的课程集合
		/// </summary>
		/// <returns></returns>
		private List<CourseInfo> GetCourses()
		{
			ThrowExceptionIfValidateFailure();

			var tempCourses = new List<CourseInfo>();

            //学习计划中的课程集合
            var planCourses = StudyPlanCourseAccessor.GetCourseList(ID);

            //课程ID集合
            var courseIds = planCourses.Select(p => p.CourseId).ToArray();

            //获取课程信息集合
            var coursesList = CourseAccessor.GetList(courseIds);

            foreach (var course in coursesList)
            {
                var planCourse = planCourses.FirstOrDefault(p => p.CourseId == course.CourseId);

                tempCourses.Add(new CourseInfo
                {
                    CourseId = course.CourseId,
                    Title = course.Title,
                    Content = course.Content,
                    Image = course.Image,
                    Objective = course.Objective,
                    Remarks = course.Remarks,
                    Sort = planCourse.Sort,
                    Status = course.Status,
                    SubjectId = course.SubjectId
                });
            }

            return tempCourses;
		}

		#endregion

	}
}
