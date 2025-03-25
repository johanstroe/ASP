using Domain.Models;

namespace Business.Interface
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(MemberLogin loginForm);
    }
}