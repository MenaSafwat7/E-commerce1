using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exeptions
{
    public class DeliveryMethodNotFoundExeption(int id) : NotFoundExeption($"No Delivery Method with Id {id} was found!")
    {
    }
}
