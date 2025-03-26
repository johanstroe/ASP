using Domain.Models;

namespace Business.Interface
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(MemberLogin loginForm);
        Task LogoutAsync();
        Task<bool> SignUpAsync(MemberSignUp signupForm);
    }
}