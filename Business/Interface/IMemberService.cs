using Data.Entities;
using Domain.Models;

namespace Business.Interface
{
    public interface IMemberService
    {
        Task<IEnumerable<Member>> GetAllMembers();
        Task CreateUser(MemberEntity member);
    }
}