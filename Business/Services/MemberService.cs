using Business.Interface;
using Business.Model;
using Data.Entities;
using Data.Repositories;
using Domain.Dtos;
using Domain.Extentions;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Business.Services;

public class MemberService(IMemberRepository memberRepository, UserManager<MemberEntity> userManager, RoleManager<IdentityRole> roleManager) : IMemberService
{
    private readonly IMemberRepository _memberRepository = memberRepository;
    private readonly UserManager<MemberEntity> _userManager = userManager;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;

    public async Task<MemberResult> GetMembersAsync()
    {
        var result = await _memberRepository.GetAllAsync();
        return result.MapTo<MemberResult>();
    }

    //VG DEL

    public async Task<MemberResult> AddMemberToRole(string memberId, string roleName)
    {
        if (!await _roleManager.RoleExistsAsync(roleName))
            return new MemberResult { Succeeded = false, Error = "Role doesn't exist" };

        var member = await _userManager.FindByIdAsync(memberId);
        if (member == null)
            return new MemberResult { Succeeded = false, Error = "Member doesn't exist" };

        var result = await _userManager.CreateAsync(member, roleName);
        return result.Succeeded
            ? new MemberResult { Succeeded = true, StatusCode = 200 }
            : new MemberResult { Succeeded = false, StatusCode = 500, Error = "Unable to add member to role" };

    }
    public async Task<MemberResult> CreateMemberAsync(SignUpForm formData, string? roleName = "User")
    {
        if (formData == null)
            return new MemberResult { Succeeded = false, StatusCode = 400, Error = "form data cannot be null" };

        var existsResult = await _memberRepository.ExistsAsync(x => x.Email == formData.Email);
        if (existsResult.Succeeded)
            return new MemberResult { Succeeded = false, StatusCode = 409, Error = "Email already exists" };

        try
        {
            var memberEntity = new MemberEntity
            {
                FirstName = formData.FirstName,
                LastName = formData.LastName,
                Email = formData.Email,
                UserName = formData.Email
            };

            var result = await _userManager.CreateAsync(memberEntity, "Sommar123!");
            if (result.Succeeded)
            {
                var addToRoleResult = await AddMemberToRole(memberEntity.Id, roleName!);
                return result.Succeeded
                ? new MemberResult { Succeeded = true, StatusCode = 201 }
                : new MemberResult { Succeeded = false, StatusCode = 201, Error = "User created but no role added" };
            }
            return new MemberResult { Succeeded = false, StatusCode = 500, Error = "Unable to add member to role" };

        }
        catch (Exception ex)
        {
            {
                Debug.WriteLine(ex.Message);
                return new MemberResult { Succeeded = false, StatusCode = 500, Error = ex.Message };
            }
        }
    }
}
