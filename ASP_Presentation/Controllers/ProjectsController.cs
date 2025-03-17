using Microsoft.AspNetCore.Mvc;

namespace ASP_Presentation.Controllers
{
    [Route("projects")]
    public class ProjectsController : Controller
    {
        [Route("")]
        public IActionResult Projects()
        {
            return View();
        }
    }
}
