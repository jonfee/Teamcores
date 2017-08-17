using Microsoft.AspNetCore.Mvc;

namespace TeamCores.Web.Component
{
	public class SidebarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}

