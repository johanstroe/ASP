using Business.Interface;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Presentation.Controllers;

public class AuthController(IAuthService authService) : Controller
{
    private readonly IAuthService _authService = authService;

    
    public IActionResult Login(string returnUrl = "~/")
    {
        ViewBag.ErrorMessage = "";
        ViewBag.ReturnUrl = returnUrl;

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(MemberLogin form, string returnUrl = "~/")
    {
        ViewBag.ErrorMessage = "";

        if(ModelState.IsValid)
        {
          var result =  await _authService.LoginAsync(form);
            if (result)
                return LocalRedirect(returnUrl);
        }

        ViewBag.ErrorMessage = "Incorrect email or password.";
        return View(form);

    }

    public IActionResult SignUp()
    {
        ViewBag.ErrorMessage = "";

        return View();
    }


    [HttpPost]
    public async Task<IActionResult> SignUp(MemberSignUp form)
    {

        if (ModelState.IsValid)
        {
            var result = await _authService.SignUpAsync(form);
            if (result)
                return LocalRedirect("~/");
        }

        ViewBag.ErrorMessage = "";
        return View(form);

    }


    public async Task <IActionResult> Logout()
    {
        await _authService.LogoutAsync();
        return LocalRedirect("~/");
    }
}
