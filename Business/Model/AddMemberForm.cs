﻿
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Business.Model;

public class AddMemberForm
{
    [Display(Name = "Member Image", Prompt = "Upload image")]
    [DataType(DataType.Upload)]
    public IFormFile? MemberImage { get; set; }


    [Display(Name = "Member Name", Prompt = "Enter member name")]
    [DataType(DataType.Text)]
    [Required(ErrorMessage = "Required")]
    public string MemberName { get; set; } = null!;

    [Display(Name = "Email", Prompt = "Enter email address")]
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Required")]
    public string Email { get; set; } = null!;


    [Display(Name = "Location", Prompt = "Enter your location")]
    [DataType(DataType.Text)]
    public string? Location { get; set; }


    [Display(Name = "Phone", Prompt = "Enter phone number")]
    [DataType(DataType.PhoneNumber)]
    public string? Phone { get; set; }
}


