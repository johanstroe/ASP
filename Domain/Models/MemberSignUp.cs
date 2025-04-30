

using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class MemberSignUp
{
   

    [Required]
    [DataType(DataType.Text)]
    [Display(Name = "Firstname", Prompt = "Enter first name")]
    public string FirstName { get; set; } = null!;

    [Required]
    [DataType(DataType.Text)]
    [Display(Name = "Lastname", Prompt = "Enter your last name")]
    public string LastName { get; set; } = null!;

    [Required]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email", Prompt = "Enter email address")]
    public string Email { get; set; } = null!;

    
    [DataType(DataType.PhoneNumber)]
    [Display(Name = "Phone", Prompt = "Enter phonenumber")]
    public string? Phone { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password", Prompt = "Enter password")]
    public string Password { get; set; } = null!;


    [Compare(nameof(Password), ErrorMessage = "Passwords don't match!")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password", Prompt = "Confirm password")]
    public string ConfirmPassword { get; set; } = null!;

    [Required(ErrorMessage = "You must accept the terms.")]
    [Display(Name = "I accept Terms and Conditions")]
    public bool AcceptTerms { get; set; }

}
