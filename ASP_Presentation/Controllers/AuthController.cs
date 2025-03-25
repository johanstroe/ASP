using Business.Interface;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Presentation.Controllers;

public class AuthController(IAuthService authService) : Controller
{
    private readonly IAuthService _authService = authService;

    [HttpGet]
    public IActionResult Login()
    {
        ViewBag.ErrorMessage = null!;
        
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(MemberLogin form, string returnUrl = "~/")
    {
        ViewBag.ErrorMessage = null!;
        
        if (ModelState.IsValid)
        {
            var result = await _authService.LoginAsync(form);
            if (result)
                return Redirect(returnUrl);
        }

        TempData["Error"] = "Test";

        ViewBag.ErrorMessage = "Incorrect email or password";
        return View(form);

    }
}
