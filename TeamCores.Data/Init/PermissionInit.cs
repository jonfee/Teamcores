using System.Collections.Generic;
using TeamCores.Common;
using TeamCores.Data.DataAccess;
using TeamCores.Data.Entity;

namespace TeamCores.Data.Init
{
	/// <summary>
	/// 权限初始化
	/// </summary>
	internal sealed class PermissionInit
	{
		private List<Permission> _permissionList;
		/// <summary>
		/// 初始的权限数据
		/// </summary>
		public List<Permission> PermissionList
		{
			get
			{
				if (_permissionList.Count < 1)
				{
					AddAll();
				}

				return _permissionList;
			}
		}

		public PermissionInit()
		{
			_permissionList = new List<Permission>();
		}

		/// <summary>
		/// 保存初始化的权限数据
		/// </summary>
		public void Save()
		{
			if (PermissionList != null && PermissionList.Count > 0)
			{
				PermissionAccessor.Insert(PermissionList);
			}
		}

		/// <summary>
		/// 添加所有模块权限
		/// </summary>
		private void AddAll()
		{
			//用户相关权限
			AddUserPermissions();
			//科目相关权限
			AddSubjectPermissions();
			//课程相关权限
			AddCoursePermissions();
			//题目相关权限
			AddQuestionPermissions();
			//学习计划相关权限
			AddStudyPlanPermissions();
			//考卷模板相关权限
			AddExamPermissions();
			//用户考卷相关权限
			AddUserExamPermissions();
		}

		/// <summary>
		/// 添加用户模块权限
		/// </summary>
		private void AddUserPermissions()
		{
			_permissionList.Add(new Permission { Id = IDProvider.NewId, Module = "用户管理", Code = "U01", Name = "查看用户资料" });
			_permissionList.Add(new Permission { Id = IDProvider.NewId, Module = "用户管理", Code = "U02", Name = "添加用户" });
			_permissionList.Add(new Permission { Id = IDProvider.NewId, Module = "用户管理", Code = "U03", Name = "删除用户" });
			_permissionList.Add(new Permission { Id = IDProvider.NewId, Module = "用户管理", Code = "U04", Name = "编辑用户资料" });
			_permissionList.Add(new Permission { Id = IDProvider.NewId, Module = "用户管理", Code = "U05", Name = "重置用户密码" });
		}

		/// <summary>
		/// 添加科目模块权限
		/// </summary>
		private void AddSubjectPermissions()
		{
			_permissionList.Add(new Permission { Id = IDProvider.NewId, Module = "科目管理", Code = "K99", Name = "科目管理（所有）" });
		}

		/// <summary>
		/// 添加课程模块权限
		/// </summary>
		private void AddCoursePermissions()
		{
			_permissionList.Add(new Permission { Id = IDProvider.NewId, Module = "课程管理", Code = "C01", Name = "查看课程信息" });
			_permissionList.Add(new Permission { Id = IDProvider.NewId, Module = "课程管理", Code = "C02", Name = "添加课程" });
			_permissionList.Add(new Permission { Id = IDProvider.NewId, Module = "课程管理", Code = "C03", Name = "编辑课程设置" });
			_permissionList.Add(new Permission { Id = IDProvider.NewId, Module = "课程管理", Code = "C04", Name = "添加课程章节" });
			_permissionList.Add(new Permission { Id = IDProvider.NewId, Module = "课程管理", Code = "C05", Name = "编辑课程章节" });
			_permissionList.Add(new Permission { Id = IDProvider.NewId, Module = "课程管理", Code = "C06", Name = "删除课程章节" });
		}

		/// <summary>
		/// 添加题目模块权限
		/// </summary>
		private void AddQuestionPermissions()
		{
			_permissionList.Add(new Permission { Id = IDProvider.NewId, Module = "题库管理", Code = "Q01", Name = "查看题目" });
			_permissionList.Add(new Permission { Id = IDProvider.NewId, Module = "题库管理", Code = "Q02", Name = "添加题目" });
			_permissionList.Add(new Permission { Id = IDProvider.NewId, Module = "题库管理", Code = "Q03", Name = "删除题目" });
			_permissionList.Add(new Permission { Id = IDProvider.NewId, Module = "题库管理", Code = "Q04", Name = "编辑题目" });
		}

		/// <summary>
		/// 添加学习计划模块权限
		/// </summary>
		private void AddStudyPlanPermissions()
		{
			_permissionList.Add(new Permission { Id = IDProvider.NewId, Module = "学习计划管理", Code = "P01", Name = "查看学习计划" });
			_permissionList.Add(new Permission { Id = IDProvider.NewId, Module = "学习计划管理", Code = "P02", Name = "添加学习计划" });
			_permissionList.Add(new Permission { Id = IDProvider.NewId, Module = "学习计划管理", Code = "P03", Name = "删除学习计划" });
			_permissionList.Add(new Permission { Id = IDProvider.NewId, Module = "学习计划管理", Code = "P04", Name = "编辑学习计划" });
			_permissionList.Add(new Permission { Id = IDProvider.NewId, Module = "学习计划管理", Code = "S01", Name = "查看用户学习计划" });
		}

		/// <summary>
		/// 添加考卷模板模块权限
		/// </summary>
		private void AddExamPermissions()
		{
			_permissionList.Add(new Permission { Id = IDProvider.NewId, Module = "考卷模板管理", Code = "E01", Name = "查看考卷模板" });
			_permissionList.Add(new Permission { Id = IDProvider.NewId, Module = "考卷模板管理", Code = "E02", Name = "添加考卷模板" });
			_permissionList.Add(new Permission { Id = IDProvider.NewId, Module = "考卷模板管理", Code = "E03", Name = "编辑考卷模板" });
		}

		/// <summary>
		/// 添加用户考卷模块权限
		/// </summary>
		private void AddUserExamPermissions()
		{
			_permissionList.Add(new Permission { Id = IDProvider.NewId, Module = "用户考卷管理", Code = "T01", Name = "查看用户考卷" });
			_permissionList.Add(new Permission { Id = IDProvider.NewId, Module = "用户考卷管理", Code = "T10", Name = "阅卷" });
		}
	}
}
