using Microsoft.AspNetCore.Mvc;
using TeamCores.Common.Utilities;
using TeamCores.Domain.Models.Course;
using TeamCores.Domain.Services;
using TeamCores.Misc;
using TeamCores.Misc.Controller;
using TeamCores.Web.ViewModel.Course;

namespace TeamCores.Web.Api
{
	[Route("api/Course")]
	public class CourseController : BaseController
	{
		CourseService service = null;

		public CourseController()
		{
			service = new CourseService();
		}

		/// <summary>
		/// ��ӿγ�
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("add")]
		public IActionResult Add(NewCourseViewModel model)
		{
			if (model == null)
			{
				model = new NewCourseViewModel();
			}

			NewCourse course = new NewCourse
			{
				Content = model.Content,
				Image = model.Image,
				Objective = model.Objective,
				Remarks = model.Remarks,
				SubjectId = model.SubjectId,
				Title = model.Title,
				UserId = Utility.GetUserContext().UserId
			};

			var success = service.Add(course);

			return Ok(success);
		}

		/// <summary>
		/// ���ÿγ�Ϊ����״̬
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("setenable")]
		public IActionResult SetEnable(long id)
		{
			var success = service.SetEnable(id);

			return Ok(success);
		}

		/// <summary>
		/// ���ÿγ�Ϊ����״̬
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("setdisable")]
		public IActionResult SetDisable(long id)
		{
			var success = service.SetDisable(id);

			return Ok(success);
		}

		/// <summary>
		/// ɾ���γ�
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("delete")]
		public IActionResult Delete(long id)
		{
			var success = service.Delete(id);

			return Ok(success);
		}

		/// <summary>
		/// �༭�γ���Ϣ
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("modify")]
		public IActionResult Modify(ModifyCourseViewModel model)
		{
			CourseModifiedState state = null;
			model.CopyTo(state);

			var success = service.Modify(model.CourseId, state);

			return Ok(success);
		}

		/// <summary>
		/// �����γ�
		/// </summary>
		/// <param name="searcher">�γ���������ͼģ��</param>
		/// <returns></returns>
		[HttpPost]
		[Route("search")]
		public IActionResult Search(CourseSearcherViewModel searcher)
		{
			if (searcher == null)
			{
				searcher = new CourseSearcherViewModel
				{
					PageIndex = 1,
					PageSize = 10
				};
			}

			var result = service.Search(searcher.PageSize, searcher.PageIndex, searcher.Keyword, searcher.Status);

			return Ok(result);
		}
	}
}