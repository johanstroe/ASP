


using Business.Interface;
using Business.Model;
using Business.Services;
using Domain.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ASP_Presentation.Controllers;

[Authorize]
//[Route("admin")]
public class AdminController : Controller

{

    private readonly IMemberService _memberService;
    private readonly IProjectService _projectService;
    private readonly IStatusService _statusService;

    public AdminController(IMemberService memberService, IProjectService projectService, IStatusService statusService)
    {
        _memberService = memberService;
        _projectService = projectService;
        _statusService = statusService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [Route("projects")]
    public async Task <IActionResult> Projects()
    {
        var response = await _projectService.GetProjectsAsync();
        
        return View(response.Result);
    }
   
    [Route("members")]
    
    public async Task <IActionResult> Members()
    {
        var members = await _memberService.GetMembersAsync();
        return View(members.Result);
    }

    [HttpGet("")]
    public IActionResult Clients()
    {
        return View("clients");
    }

    [HttpPost]

    public IActionResult AddClient(AddClientForm form)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Where(x => x.Value?.Errors.Count > 0)
                .ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value?.Errors.Select(x => x.ErrorMessage).ToArray()
                );

            return BadRequest(new { success = false, errors });
        }

        //  Send data to clientService

        return Ok(new { success = true, redirectUrl = "/members" });


    }

    [HttpPost]
    public IActionResult EditClient(EditClientForm form)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Where(x => x.Value?.Errors.Count > 0)
                .ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value?.Errors.Select(x => x.ErrorMessage).ToArray()
                );

            return BadRequest(new { success = false, errors });
        }

        //  Send data to clientService


        return Ok(new { success = true });
    }


    [HttpPost]

    public async Task <IActionResult> AddMember([FromForm] AddMemberForm form)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Where(x => x.Value?.Errors.Count > 0)
                .ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value?.Errors.Select(x => x.ErrorMessage).ToArray()
                );

            return BadRequest(new { success = false, errors });
        }

        //  Send data to memberService

        var member = new SignUpForm
        { 
        FirstName = form.FirstName,
        LastName = form.LastName,
        Email = form.Email,

        };

       await _memberService.CreateMemberAsync(member);
        
        return Ok(new { success = true, redirectUrl = "/members" });
    }

    [HttpPost]
    public IActionResult EditMember(EditMemberForm form)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Where(x => x.Value?.Errors.Count > 0)
                .ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value?.Errors.Select(x => x.ErrorMessage).ToArray()
                );

            return BadRequest(new { success = false, errors });
        }

        //  Send data to clientService


        return Ok(new { success = true });
    }

    [HttpPost]
    public async Task <IActionResult> AddProject([FromForm]AddProjectForm form)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Where(x => x.Value?.Errors.Count > 0)
                .ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value?.Errors.Select(x => x.ErrorMessage).ToArray()
                );

            return BadRequest(new { success = false, errors });
        }


        var result = await _projectService.CreateProjectAsync(form);
        

        //  Send data to clientService

        return Ok(new { success = true, redirectUrl = "/projects" });
        
    }

    [HttpPost]
    public IActionResult EditProject(EditProjectForm form)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Where(x => x.Value?.Errors.Count > 0)
                .ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value?.Errors.Select(x => x.ErrorMessage).ToArray()
                );

            return BadRequest(new { success = false, errors });
        }

        //  Send data to clientService


        return Ok(new { success = true });
    }


}
