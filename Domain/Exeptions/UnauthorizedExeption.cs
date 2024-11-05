using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exeptions
{
    public sealed class UnauthorizedExeption(string message = "Invalid Email or Password") : Exception(message) 
    {

    }
}
