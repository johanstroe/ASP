using Domain.Models;

namespace Business.Model;

public class MemberResult : ServiceResult
{
    public IEnumerable<Member>? Result { get; set; }

}




