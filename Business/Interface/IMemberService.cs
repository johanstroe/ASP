using Business.Model;
using Domain.Dtos;

namespace Business.Interface;

public interface IMemberService
{
    Task<MemberResult> AddMemberToRole(string memberId, string roleName);
    Task<MemberResult> CreateMemberAsync(SignUpForm formData, string roleName = "User");
    Task<MemberResult> GetMembersAsync();
}
