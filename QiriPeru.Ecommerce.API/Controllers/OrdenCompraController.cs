using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QiriPeru.Ecommerce.API.Dtos;
using QiriPeru.Ecommerce.API.Errors;
using QiriPeru.Ecommerce.Core.Entities.OrdenCompra;
using QiriPeru.Ecommerce.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QiriPeru.Ecommerce.API.Controllers
{
    [Authorize]
    public class OrdenCompraController : BaseApiController
    {
        private readonly IOrdenCompraService _ordenCompraService;
        private readonly IMapper _mapper;

        public OrdenCompraController(IOrdenCompraService ordenCompraService, IMapper mapper)
        {
            _ordenCompraService = ordenCompraService;
            _mapper = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<OrdenCompras>> AddCordenCompra(OrdenCompraDto ordenCompraDto)
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            var direccion = _mapper.Map<DireccionDto, Direccion>(ordenCompraDto.DireccionEnvio);
            var ordenCompra = await _ordenCompraService.AddOrdenCompraAsync(email, ordenCompraDto.TipoEnvio, ordenCompraDto.CarritoCompraId, direccion);

            if(ordenCompra == null)
            {
                return BadRequest(new CodeErrorResponse(400, "Error creando la orden de compra"));
            }

            return Ok(ordenCompra);
        }



    }
}
