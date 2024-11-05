using Microsoft.AspNetCore.Mvc;
using Servises.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    
    public class BasketController(IServiceManager serviceManager) : ApiController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<BasketDTO>> Get(String Id)
        {
            var Basket = await serviceManager.basketServise.GetBasketAsync(Id);
            return Ok(Basket);
        }

        [HttpPost]
        public async Task<ActionResult<BasketDTO>> Update(BasketDTO basketDTO)
        {
            var Basket = await serviceManager.basketServise.UpdateBasketAsync(basketDTO);
            return Ok(Basket);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string Id)
        {
            var Basket = await serviceManager.basketServise.DeleteBasketAsync(Id);
            return NoContent();
        }
    }
}
