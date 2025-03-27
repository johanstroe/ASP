
using Business.Interface;
using Business.Model;
using Data.Contexts;
using Data.Entities;
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
        var members = await _memberService.GetAllMembers();
        return View(members);
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

    public IActionResult AddMember([FromForm]AddMemberForm form)
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

        var member = new MemberEntity();
        member.FirstName = form.MemberName;
        member.Email = form.Email;
        member.LastName = form.MemberName;

        _memberService.CreateUser(member);
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
}
