﻿

using Business.Interface;
using Data.Contexts;
using Data.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Business.Services;

public class MemberService(UserManager<MemberEntity> userManager, DataContext db) : IMemberService
{
    private readonly UserManager<MemberEntity> _userManager = userManager;
    
    public async Task<IEnumerable<Member>> GetAllMembers()
    {

        var list = await _userManager.Users.ToListAsync();
        var members = list.Select(x => new Member
        {
            Id = x.Id,
            FirstName = x.FirstName,
            LastName = x.LastName,
            Email = x.Email,
            Phone = x.PhoneNumber,
            JobTitle = x.JobTitle,

        });
        return members;
    }

    public async Task CreateUser(MemberEntity member)
    {
        db.Users.Add(member);
        db.SaveChanges();
        return;
    }


}
