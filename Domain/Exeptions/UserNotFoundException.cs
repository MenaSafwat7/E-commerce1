using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exeptions
{
    public class UserNotFoundException(string email) : NotFoundExeption($"no user with Email {email} was found!.")
    {
    }
}
