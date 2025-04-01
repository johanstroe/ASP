
using Business.Interface;
using Business.Model;
using Business.Services;
using Data.Contexts;
using Data.Entities;
using Domain.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Presentation.Controllers;

[Authorize]
//[Route("admin")]
public class AdminController : Controller

{

    private readonly IMemberService _memberService;

    public AdminController(IMemberService memberService)
    {
        _memberService = memberService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Projects()
    {
        return View();
    }
   
    [Route("members")]
    
    public async Task <IActionResult> Members()
    {
        var members = await _memberService.GetMembersAsync();
        return View(members.Result);
    }

    [Route("clients")]
    public IActionResult Clients()
    {
        return View();
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
    public IActionResult EditClient(AddClientForm form)
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
        
        return Ok(new { success = true, redirectUrl = "~/" });
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
}
