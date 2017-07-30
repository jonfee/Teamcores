using Microsoft.AspNetCore.Mvc;
using TeamCores.Domain.Services;
using TeamCores.Misc;
using TeamCores.Misc.Controller;
using TeamCores.Web.ViewModel.Question;

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
                model.Type,
                model.Topic,
                model.AnswerOptions);

            return Ok(success);
        }

        /// <summary>
        /// 启用题目
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
        /// 禁用题目
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
        /// 删除题目
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
        /// 修改题目信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("modify")]
        public IActionResult Modify(QuestionModifyViewModel model)
        {
            var success = service.ModifyTo(
                model.QuestionId,
                model.CourseId,
                model.Type,
                model.Topic,
                model.AnswerOptions,
                model.Status);

            return Ok(success);
        }

        /// <summary>
        /// 搜索题目
        /// </summary>
        /// <param name="searcher">题目搜索器视图模型</param>
        /// <returns></returns>
        [HttpPost]
        [Route("search")]
        public IActionResult Search(QuestionSearcherViewModel searcher)
        {
            if (searcher == null)
            {
                searcher = new QuestionSearcherViewModel
                {
                    PageIndex = 1,
                    PageSize = 10
                };
            }

            var result = service.Search(
                searcher.PageSize, 
                searcher.PageIndex, 
                searcher.Keyword,
                searcher.QuestionType, 
                searcher.CourseId, 
                searcher.Status);

            return Ok(result);
        }
    }
}