using Microsoft.AspNetCore.Mvc;
using QiriPeru.Ecommerce.Core.Entities;
using QiriPeru.Ecommerce.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QiriPeru.Ecommerce.API.Controllers
{
    public class CarritoCompraController : BaseApiController
    {
        private readonly ICarritoCompraRepository _carritoCompra;

        public CarritoCompraController(ICarritoCompraRepository carritoCompra)
        {
            _carritoCompra = carritoCompra;
        }


        [HttpGet]
        public async Task<ActionResult<CarritoCompra>> GetCarritoById(string id)
        {
            var carrito = await _carritoCompra.GetCarritoCompraAsync(id);

            return Ok(carrito ?? new CarritoCompra(id));
        }

        [HttpPost]
        public async Task<ActionResult<CarritoCompra>> UpdateCarritoCompra(CarritoCompra carritoParametro)
        {
            var carritoActualizado = await _carritoCompra.UpdateCarritoCompraAsync(carritoParametro);

            return Ok(carritoActualizado);
        }

        [HttpDelete]
        public async Task DeleteCarritoCompra(string id)
        {
            await _carritoCompra.DeleteCarritoCompraAsync(id);
        }
    }
}
