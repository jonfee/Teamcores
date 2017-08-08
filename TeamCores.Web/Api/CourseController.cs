using Microsoft.AspNetCore.Mvc;
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

            var success = service.Add(
                Utility.GetUserContext().UserId,
                model.SubjectId,
                model.Title,
                model.Image,
                model.Content,
                model.Remarks,
                model.Objective);

            return Ok(success);
        }

		/// <summary>
		/// ��ȡ�γ���Ϣ
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("get")]
		public IActionResult GetCourse(long id)
		{
			var data = service.GetCourse(id);

			return Ok(data);
		}

		/// <summary>
		/// ��ȡ�γ���ϸ��Ϣ
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("details")]
		public IActionResult GetDetails(long id)
		{
			var data = service.GetDetails(id);

			return Ok(data);
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
            var success = service.Modify(
                model.CourseId,
                model.SubjectId,
                model.Title,
                model.Image,
                model.Content,
                model.Remarks,
                model.Objective,
                model.Status);

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

            var result = service.Search(searcher.PageSize, searcher.PageIndex, searcher.Keyword, searcher.SubjectId, searcher.Status);

            return Ok(result);
        }
    }
}