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
	/// ������ؽӿڿ�����
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
		/// ��ӿ�����Ϣ
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