using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exeptions
{
    public sealed class BasketNotFoundExeption(string Id) : NotFoundExeption($"Basket with Id {Id} not found")
    {

    }
}
