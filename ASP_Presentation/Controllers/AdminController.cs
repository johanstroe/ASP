


using Business.Interface;
using Business.Model;
using Business.Services;
using Domain.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace ASP_Presentation.Controllers;

[Authorize]
//[Route("admin")]
public class AdminController : Controller

{

    private readonly IMemberService _memberService;
    private readonly IProjectService _projectService;
    private readonly IStatusService _statusService;
    private readonly IClientService _clientService;

    public AdminController(IMemberService memberService, IProjectService projectService, IStatusService statusService, IClientService clientService)
    {
        _memberService = memberService;
        _projectService = projectService;
        _statusService = statusService;
        _clientService = clientService;
    }

    public async Task<IActionResult> Index()
    {
        var email = User?.Identity?.Name;
        if (email != null)
        {
            var member = await _memberService.GetMemberByEmailAsync(email);
            ViewBag.FullName = $"{member?.FirstName} {member?.LastName}";
        }
       
        return View();
    }
    
    [HttpGet("")]
    [Route("projects")]
    public async Task <IActionResult> Projects()
    {
        var response = await _projectService.GetProjectsAsync();
        var clients = await _clientService.GetClientsAsync();
        ViewBag.Clients = new SelectList(clients.Result, "Id", "ClientName");

        var members = await _memberService.GetMembersAsync();
        
        ViewBag.Members = new SelectList(
        members.Result?.Select(m => new {
        Id = m.Id,
        FullName = $"{m.FirstName} {m.LastName}"
             }),
              "Id",
              "FullName"
                );

        return View(response.Result);
    }
   
    [Route("members")]
    
    public async Task <IActionResult> Members()
    {
        var members = await _memberService.GetMembersAsync();
        return View(members.Result);
    }


    [HttpGet]
    [Route("clients")]
    public async Task<IActionResult> Clients()
    {
        var response = await _clientService.GetClientsAsync();
        return View(response.Result);
    }

    [HttpPost]

    public async Task<IActionResult> AddClient(AddClientForm form)
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

        var result = await _clientService.CreateClientAsync(form);

        if (!result.Succeeded)
        {
            return StatusCode(result.StatusCode, new { success = false, error = result.Error });
        }


        //  Send data to clientService

        return Ok(new { success = true, redirectUrl = "/clients" });


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
        ProfileImage = form.MemberImage,
        
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
    public async Task <IActionResult> EditProject(EditProjectForm form)
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

        var result = await _projectService.UpdateProjectAsync(form);

        //  Send data to clientService


        return Ok(new { success = true, redirectUrl = "/projects" });
    }


}
