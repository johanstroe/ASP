using System.ComponentModel.DataAnnotations;

namespace ASP_Presentation.ViewModels;

public class SignUpViewModel
{
    [Required]
    [Display(Name ="First Name", Prompt = "Enter first name")]
    [DataType(DataType.Text)]
    public string FirstName { get; set; } = null!;
    [Required]
    [DataType(DataType.Text)]
    [Display(Name = "Last Name", Prompt = "Enter last name")]
    public string LastName { get; set; } = null!;
    
    
    [Required]
    [RegularExpression(@"")]
    [Display(Name = "Email", Prompt = "Enter email")]
    public string Email { get; set; } = null!;
   
    
    [Required]
    [RegularExpression(@"")]
    [Display(Name = "Password", Prompt ="Enter password")]
    public string Password { get; set; } = null!;
    [Required]
    [Compare(nameof(Password))]
    [Display(Name = "Confirm Password", Prompt = "Confirm password")]
    public string ConfirmPassword { get; set; } = null!;

    [Range(typeof(bool), "true", "true")]
    public bool TermsAndConditions { get; set; } 

}
