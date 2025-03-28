using Domain.Models;

namespace Business.Model;

public class StatusResult
{
    public bool Succeeded { get; set; }
    public int StatusCode { get; set; }
    public string? Error { get; set; }
    public IEnumerable<Status>? Result { get; set; }
}


