using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeamCores.Domain.Models.Subject;
using TeamCores.Domain.Services;
using TeamCores.Misc.Controller;
using TeamCores.Web.ViewModel.Subject;

namespace TeamCores.Web.Api
{
	[Produces("application/json")]
	[Route("api/Subject")]
	public class SubjectController : BaseController
	{
		/// <summary>
		/// 新增科目
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("add")]
		public IActionResult Add(NewSubjectViewModel model)
		{
			if (model == null)
			{
				model = new NewSubjectViewModel();
			}

			NewSubject newSubject = new NewSubject();
			newSubject.Name = model.Name;

			var success = new SubjectService().AddSubject(newSubject);

			return Ok(success);
		}
	}
}