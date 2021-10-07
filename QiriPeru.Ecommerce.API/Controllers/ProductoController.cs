using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QiriPeru.Ecommerce.API.Dtos;
using QiriPeru.Ecommerce.API.Errors;
using QiriPeru.Ecommerce.Core.Entities;
using QiriPeru.Ecommerce.Core.Interfaces;
using QiriPeru.Ecommerce.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QiriPeru.Ecommerce.API.Controllers
{

   
    public class ProductoController : BaseApiController
    {
        private readonly IGenericRepository<Producto> _productoRepository;
        private readonly IMapper _mapper;

        public ProductoController(IGenericRepository<Producto> productoRepository, IMapper mapper)
        {
            _productoRepository = productoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Paginaton<ProductoDto>>> GetProductos([FromQuery]ProductoSpecificationParams productoParams)
        {            
            var spec = new ProductoWithCategoriasAndMaterialSpecification(productoParams);

            var productos = await _productoRepository.GetAllWithSpecAsync(spec);

            var specCount = new ProductoForCountingSpecification(productoParams);
            var totalProductos = await _productoRepository.CountAsync(specCount);      
            
            var totalPages = 0;

            if (totalProductos % productoParams.PageSize != 0)
            {
                totalPages = (totalProductos / productoParams.PageSize) + 1;
            }
            else
            {
                totalPages = totalProductos / productoParams.PageSize;
            }

            var data = _mapper.Map<IReadOnlyList<Producto>, IReadOnlyList<ProductoDto>>(productos);

            return Ok(
                new Paginaton<ProductoDto>
                {
                    Count = totalProductos,
                    Data = data,
                    PageCount = totalPages,
                    PageIndex = productoParams.PageIndex,
                    PageSize = productoParams.PageSize
                }
            );          
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDto>> GetProducto(int id)
        {
            var spec = new ProductoWithCategoriasAndMaterialSpecification(id);
            var producto = await _productoRepository.GetByIdWithSpecAsync(spec);

            if (producto == null)
            {
                return NotFound(new CodeErrorResponse(404, "El producto no existe"));
            }

            return _mapper.Map<Producto, ProductoDto>(producto);
            
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public async Task<ActionResult<Producto>> Post(Producto producto)
        {
            var resultado = await _productoRepository.Add(producto);

            if(resultado == 0)
            {
                throw new Exception("No se insertó el producto");
            }

            return Ok(producto);
        }


        [Authorize(Roles = "ADMIN")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Producto>> Put (int id, Producto producto)
        {
            producto.Id = id;
            var resultado = await _productoRepository.Update(producto);

            if(resultado == 0)
            {
                throw new Exception("No se pudo actualizar el producto");
            }

            return Ok(producto);
        }
    }
}
