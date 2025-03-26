
using Business.Interface;
using Data.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Business.Services;



public class AuthService(SignInManager<MemberEntity> signInManager, UserManager<MemberEntity> userManager) : IAuthService
{
    private readonly SignInManager<MemberEntity> _signInManager = signInManager;
    private readonly UserManager<MemberEntity> _userManager = userManager;
    public async Task<bool> LoginAsync(MemberLogin loginForm)
    {
        var result = await _signInManager.PasswordSignInAsync(loginForm.Email, loginForm.Password, false, false);
        return result.Succeeded;
    }

    public async Task<bool> SignUpAsync(MemberSignUp signupForm)
    {
        var memberEntity = new MemberEntity
        {
            UserName = signupForm.Email,
            FirstName = signupForm.FirstName,
            LastName = signupForm.LastName,
            Email = signupForm.Email,
            PhoneNumber = signupForm.Phone,
        };

        var result = await _userManager.CreateAsync(memberEntity, signupForm.Password);
        return result.Succeeded;
    }

    public async Task LogoutAsync()
    {
        
            await _signInManager.SignOutAsync();
            
    }
}
