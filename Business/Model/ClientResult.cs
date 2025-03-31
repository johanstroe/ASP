

using Domain.Models;

namespace Business.Model;

public class ClientResult : ServiceResult
{
        public IEnumerable<Client>? Result { get; set; }
    
}
