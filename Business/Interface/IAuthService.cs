using Business.Model;
using Domain.Dtos;

namespace Business.Interface
{
    public interface IAuthService
    {
        Task<AuthResult> SignInAsync(SignInForm formData);
        Task<AuthResult> SignOutAsync();
        Task<AuthResult> SignUpAsync(SignUpForm formData);
    }
}