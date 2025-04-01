using Business.Dtos;
using Business.Interface;
using Business.Model;
using Data.Entities;
using Domain.Dtos;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Business.Services;



public class AuthService(IMemberService memberService, SignInManager<MemberEntity> signInManager) : IAuthService
{
    private readonly IMemberService _memberService = memberService;
    private readonly SignInManager<MemberEntity> _signInManager = signInManager;

    public async Task<AuthResult> SignInAsync(SignInForm formData)
    {
        if (formData == null)
            return new AuthResult { Succeeded = false, StatusCode = 400, Error = "Not all required fields are supplied" };

        var result = await _signInManager.PasswordSignInAsync(formData.Email, formData.Password, formData.IsPersistent, false);
        return result.Succeeded
          ? new AuthResult { Succeeded = true, StatusCode = 201 }
          : new AuthResult { Succeeded = false, StatusCode = 401, Error = "Invalid email or password" };
    }

    public async Task<AuthResult> SignUpAsync(SignUpForm formData)
    {
        if (formData == null)
            return new AuthResult { Succeeded = false, StatusCode = 400, Error = "Not all required fields are supplied" };

        var result = await _memberService.CreateMemberAsync(formData);

        return result.Succeeded
          ? new AuthResult { Succeeded = true, StatusCode = 201 }
          : new AuthResult { Succeeded = false, StatusCode = result.StatusCode, Error = result.Error };
    }

    public async Task<AuthResult> SignOutAsync()
    {
        await _signInManager.SignOutAsync();
        return new AuthResult { Succeeded = true, StatusCode = 201 };
    }
}
