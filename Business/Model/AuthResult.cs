using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Model;

public class AuthResult : ServiceResult
{
}

public class AuthResult<T> : ServiceResult
{
    public T? Result { get; set; }
}
