

using System.ComponentModel.DataAnnotations;

namespace Business.Dtos
{
    public class UserRegistrationForm
    {
        [Required]
        public string FullName { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

    }

    public class UserUpdateForm
    {
        public int Id { get; private set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
    }
}
