using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeamCores.Misc.Controller;
using TeamCores.Web.ViewModel.Question;
using TeamCores.Domain.Services;
using TeamCores.Misc;

namespace TeamCores.Web.Api
{
    [Route("api/Question")]
    public class QuestionController : BaseController
    {
        QuestionService service = null;

        public QuestionController()
        {
            service = new QuestionService();
        }

        /// <summary>
        /// 新增题目
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        public IActionResult Add(NewQuestionViewModel model)
        {
            var success = service.Add(
                Utility.GetUserContext().UserId,
                model.CourseId,
                model.SubjectId,
                model.Type,
                model.Marking,
                model.Topic,
                model.AnswerOptions);

            return Ok(success);
        }
    }
}