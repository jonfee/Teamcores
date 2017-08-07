using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeamCores.Domain.Services;
using TeamCores.Domain.Services.Request;
using TeamCores.Misc.Controller;

namespace TeamCores.Web.Api
{
	/// <summary>
	/// 考卷相关接口控制器
	/// </summary>
    [Route("api/Exams")]
    public class ExamsController : BaseController
    {
		ExamsService service = null;

		public ExamsController()
		{
			service = new ExamsService();
		}

		/// <summary>
		/// 添加考卷信息
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public IActionResult Add(NewExamsRequest request)
		{
			var success = service.Add(request);

			return Ok(success);
		}
    }
}