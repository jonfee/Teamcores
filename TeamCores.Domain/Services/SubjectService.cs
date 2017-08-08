using TeamCores.Data.Entity;
using TeamCores.Domain.Models.Subject;
using TeamCores.Domain.Services.Response;
using TeamCores.Models;

namespace TeamCores.Domain.Services
{
    /// <summary>
    /// 科目相关服务
    /// </summary>
    public class SubjectService
	{
		/// <summary>
		/// 添加新科目
		/// </summary>
		/// <param name="name"></param>
		public bool AddSubject(string name)
		{
            NewSubject subject = new NewSubject
            {
                Name = name
            };

            return subject.Save();
		}

		/// <summary>
		/// 删除科目
		/// </summary>
		/// <param name="subjectId"></param>
		/// <returns></returns>
		public bool Delete(long subjectId)
		{
			SubjectManage subject = new SubjectManage(subjectId);

			return subject.Delete();
		}

		/// <summary>
		/// 设置科目为”启用“状态
		/// </summary>
		/// <param name="subjectId"></param>
		/// <returns></returns>
		public bool SetEnable(long subjectId)
		{
			SubjectManage subject = new SubjectManage(subjectId);

			return subject.SetEnable();
		}

		/// <summary>
		/// 设置科目为”禁用“状态
		/// </summary>
		/// <param name="subjectId"></param>
		/// <returns></returns>
		public bool SetDisable(long subjectId)
		{
			SubjectManage subject = new SubjectManage(subjectId);

			return subject.SetDisable();
		}

		/// <summary>
		/// 修改科目名称
		/// </summary>
		/// <param name="subjectId"></param>
		/// <param name="newName"></param>
		/// <returns></returns>
		public bool Rename(long subjectId, string newName)
		{
			SubjectManage subject = new SubjectManage(subjectId);

			return subject.Rename(newName);
		}

		/// <summary>
		/// 搜索科目信息
		/// </summary>
		/// <param name="pageSize"></param>
		/// <param name="pageIndex"></param>
		/// <param name="keyword"></param>
		/// <param name="status"></param>
		/// <returns></returns>
		public PagerModel<Subjects> Search(int pageSize, int pageIndex, string keyword, int? status)
		{
			SubjectSearch search = new SubjectSearch(pageIndex, pageSize, keyword, status);

			return search.Search();
		}

		/// <summary>
		/// 获取科目信息
		/// </summary>
		/// <param name="subjectId"></param>
		/// <returns></returns>
		public Subjects GetSubject(long subjectId)
		{
			SubjectManage subject = new SubjectManage(subjectId);

			return subject.Subject;
		}

		/// <summary>
		/// 获取科目详细信息
		/// </summary>
		/// <param name="subjectId"></param>
		/// <returns></returns>
		public SubjectDetails GetDetails(long subjectId)
		{
			SubjectManage subject = new SubjectManage(subject);

			return subject.ConvertToSubjectDetails();
		}
	}
}
