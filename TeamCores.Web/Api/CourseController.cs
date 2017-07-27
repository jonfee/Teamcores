using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
		/// Ìí¼Ó¿Î³Ì
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

	}
}