
using Microsoft.AspNetCore.Identity;

namespace Data.Entities;

public class MemberEntity : IdentityUser
{
    public string? ImageUrl { get; set; }

    [ProtectedPersonalData]
    public string? FirstName { get; set; }
    [ProtectedPersonalData]
    public string? LastName { get; set; }
    [ProtectedPersonalData]
    public string? JobTitle { get; set; }
   
    [ProtectedPersonalData]
    public DateOnly? DateOfBirth { get; set; } // om du vill spara det sammanställt


    public virtual MemberAddressEntity? Address { get; set; }

    public virtual ICollection<ProjectEntity> Projects { get; set; } = [];
}
