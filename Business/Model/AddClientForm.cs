

using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Business.Model;

public class AddClientForm
{
    [Display(Name = "Client Image", Prompt = "Upload image")]
    [DataType(DataType.Upload)]
    public IFormFile? ClientImage { get; set; }


    [Display(Name = "Client Name", Prompt = "Enter client name")]
    [DataType(DataType.Text)]
    [Required(ErrorMessage = "Required")]
    public string ClientName { get; set; } = null!;

}
