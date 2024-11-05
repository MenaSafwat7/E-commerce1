using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exeptions
{
    public class OrderNotFoundException(Guid id) : NotFoundExeption($"No order with id {id} was found!")
    {
    }
}
