
using Business.Interface;
using Business.Model;
using Data.Entities;
using Domain.Dtos;
using Domain.Extentions;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ASP_Presentation.Controllers;

public class AuthController(IAuthService authService, SignInManager<MemberEntity> signInManager, UserManager<MemberEntity> userManager) : Controller
{
    private readonly IAuthService _authService = authService;
    private readonly UserManager<MemberEntity> _userManager = userManager;
    private readonly SignInManager<MemberEntity> _signInManager = signInManager;

    #region SignUp
    [HttpGet]
    public IActionResult SignUp()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SignUp(MemberSignUp model)
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
    #endregion

    #region SignIn
    public IActionResult SignIn(string returnUrl = "~/")
    {
        ViewBag.ReturnUrl = returnUrl;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SignIn(MemberLogin model, string returnUrl = "~/")
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
        return View(model);
    }

    #endregion

    #region Logout
    public async Task<IActionResult> LogOut()
    {
        await _authService.SignOutAsync();
        return LocalRedirect("~/");
    }
    #endregion

    #region External Authentication

    [HttpPost, HttpGet]
    public IActionResult ExternalSignIn(string provider, string returnUrl = null!)
    {
        Console.WriteLine($" ExternalSignIn: provider={provider}, returnUrl={returnUrl}");
        if (string.IsNullOrEmpty(provider))
        {
            ModelState.AddModelError("", "Invalid provider");
            return View("SignIn");
        }

        var redirectUrl = Url.Action("ExternalSignInCallBack", "Auth", new { returnUrl })!;
        var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        return Challenge(properties, provider);
    }


    public async Task<IActionResult> ExternalSignInCallBack(string returnUrl = null!, string remoteError = null!)
    {
        returnUrl ??= Url.Content("~/");

        if (!string.IsNullOrEmpty(remoteError))
        {
            ModelState.AddModelError("", $"Error from external provider: {remoteError}");
            return View("SignIn");
        }

        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info == null)
            return RedirectToAction("SignIn");

        var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
        if (signInResult.Succeeded)
        {
            return LocalRedirect(returnUrl);
        }
        else
        {
            string firstName = string.Empty;
            string lastName = string.Empty;

            try
            {
                firstName = info.Principal?.FindFirstValue(ClaimTypes.GivenName) ?? info.Principal?.FindFirstValue(ClaimTypes.Email)!;
                lastName = info.Principal?.FindFirstValue(ClaimTypes.Surname) ?? "";
            }
            catch {}

            string email = info.Principal?.FindFirstValue(ClaimTypes.Email)!;
            string userName = $"ext_{info.LoginProvider.ToLower()}_{email}";

            var user = new MemberEntity { UserName = userName, Email = email, FirstName = firstName, LastName = lastName };

            var identityResult = await _userManager.CreateAsync(user);
            if (identityResult.Succeeded)
            {
                await _userManager.AddLoginAsync(user, info);
                await _signInManager.SignInAsync(user,isPersistent: false);
                return LocalRedirect(returnUrl);
            }
            foreach(var error in identityResult.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View("SignIn");
        }
    }


    #endregion

}