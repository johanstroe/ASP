using Business.Model;
using Domain.Dtos;

namespace Business.Services
{
    public interface IAuthService
    {
        Task<AuthResult> SignInAsync(SignInForm formData);
        Task<AuthResult> SignOutAsync(SignUpForm formData);
        Task<AuthResult> SignUpAsync(SignUpForm formData);
    }
}