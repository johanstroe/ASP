using Business.Interface;
using Business.Services;
using Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Presentation.Controllers
{
    [Route("projects")]
    public class ProjectsController(IProjectService projectService) : Controller
    {

        private readonly IProjectService _projectService = projectService;

        [Route("")]
        public async IActionResult Index()
        {
            var model = new ProjectsViewModel
            {
                ProjectsController = await _projectService.GetProjectsAsync(),
            };

            return View(model);
        }
        [HttpPost]
        public async IActionResult Add(AddProjectViewModel model)
        {
            var addProjectForm = model.MapTo<AddProjectForm>();

            var result = await _projectService.CreateProjectAsync(addProjectForm);

            // return Json(new {});

            return View();
        }

        [HttpPost]
        public IActionResult Update(EditProjectViewModel)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            return View();
        }

    }
}
