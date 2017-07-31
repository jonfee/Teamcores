using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeamCores.Domain.Services;
using TeamCores.Misc;
using TeamCores.Misc.Controller;
using TeamCores.Web.ViewModel.StudyPlan;

namespace TeamCores.Web.Api
{
	[Route("api/StudyPlan")]
	public class StudyPlanController : BaseController
	{
		StudyPlanService service = null;

		public StudyPlanController()
		{
			service = new StudyPlanService();
		}

		/// <summary>
		/// 添加新学习计划
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("add")]
		public IActionResult Add(NewStudyPlanViewModel model)
		{
			var success = service.Add(
				 Utility.GetUserContext().UserId,
				 model.Title,
				 model.Content,
				 model.Courses,
				 model.Students);

			return Ok(success);
		}
	}
}