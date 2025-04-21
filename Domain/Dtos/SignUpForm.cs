

using Microsoft.AspNetCore.Http;

namespace Domain.Dtos;

public class SignUpForm
{
    
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public IFormFile? ProfileImage { get; set; }

}
