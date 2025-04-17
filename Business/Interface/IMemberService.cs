using Business.Model;
using Domain.Dtos;
using Domain.Models;

namespace Business.Interface
{
    public interface IMemberService
    {
        Task<MemberResult> AddMemberToRole(string memberId, string roleName);
        Task<MemberResult> CreateMemberAsync(SignUpForm formData, string? roleName = "User");
        Task<Member?> GetMemberByEmailAsync(string email);
        Task<MemberResult> GetMembersAsync();
    }
}