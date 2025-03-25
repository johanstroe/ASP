
using Business.Interface;
using Data.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Business.Services;

public class AuthService(SignInManager<MemberEntity> signInManager) : IAuthService
{
    private readonly SignInManager<MemberEntity> _signInManager = signInManager;

    public async Task<bool> LoginAsync(MemberLogin loginForm)
    {
        var result = await _signInManager.PasswordSignInAsync(loginForm.Email, loginForm.Password, false, false);
        return result.Succeeded;
    }
}
