﻿

using Domain.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Business.Model;

public class EditProjectForm
 
{
    public string Id { get; set; } = null!;

    [Display(Name = "Project Image", Prompt = "Upload image")]
    [DataType(DataType.Upload)]
    public IFormFile? ProjectImage { get; set; }

    [DataType(DataType.ImageUrl)]
    public string? ExistingImage { get; set; }

    [Display(Name = "Project Name", Prompt = "Enter project name")]
    [DataType(DataType.Text)]
    [Required(ErrorMessage = "Required")]
    public string ProjectName { get; set; } = null!;


    [Display(Name = "Client Name", Prompt = "Enter client name")]
    [DataType(DataType.Text)]
    [Required(ErrorMessage = "Required")]
    public string ClientId { get; set; } = null!;

    [Display(Name = "Description", Prompt = "Type something")]
    [DataType(DataType.Text)]

    public string? Description { get; set; }


    [Display(Name = "Start Date", Prompt = "Enter start date")]
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }


    [Display(Name = "End Date", Prompt = "Enter end date")]
    [DataType(DataType.Date)]
    public DateTime? EndDate { get; set; }

    [Display(Name = "Budget", Prompt = "Enter budget")]
    [DataType(DataType.Currency)]
    public decimal Budget { get; set; }

    public List<Status> Statuses { get; set; } = [];

    public int StatusId { get; set; }

    //public List<Status> Statuses { get; set; } = [];

    [Display(Name = "Member Name", Prompt = "Enter member name")]
    [DataType(DataType.Text)]
    [Required(ErrorMessage = "Required")]
    public string MemberId { get; set; } = null!;

}
