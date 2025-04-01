
using Business.Interface;
using Business.Model;
using Domain.Dtos;
using Domain.Extentions;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Presentation.Controllers;

public class AuthController(IAuthService authService) : Controller
{
    private readonly IAuthService _authService = authService;

    [HttpGet]
    public IActionResult SignUp()
    {
        return View();
    }

    [HttpPost]
    public async Task <IActionResult> SignUp(MemberSignUp model)
    {
        ViewBag.ErrorMessage = null;

        if (!ModelState.IsValid)
            return View(model);

        var signUpForm = model.MapTo<SignUpForm>();

        var result = await _authService.SignUpAsync(signUpForm);
        if (result.Succeeded)
        {
            return RedirectToAction("SignIn", "Auth");
        }
        ViewBag.ErrorMessage = result.Error;
        return View();
    }



    public IActionResult SignIn(string returnUrl = "~/")
    {
        ViewBag.ReturnUrl = returnUrl;
        return View();
    }

    [HttpPost]
    public async Task <IActionResult> SignIn(MemberLogin model, string returnUrl = "~/")
    {
        ViewBag.ErrorMessage = null;
        ViewBag.ReturnUrl = returnUrl;

        if (!ModelState.IsValid)
            return View(model);

        var signInForm = model.MapTo<SignInForm>();
        var result = await _authService.SignInAsync(signInForm);
        
        if (result.Succeeded)
        {
            return LocalRedirect(returnUrl);
        }
        ViewBag.ErrorMessage = result.Error;
        return View();
    }


    public async Task<IActionResult> LogOut()
    {
        await _authService.SignOutAsync();
        return LocalRedirect("~/");
    }
}
