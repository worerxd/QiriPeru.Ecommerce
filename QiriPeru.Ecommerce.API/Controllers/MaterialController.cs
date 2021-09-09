using Microsoft.AspNetCore.Mvc;
using QiriPeru.Ecommerce.Core.Entities;
using QiriPeru.Ecommerce.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QiriPeru.Ecommerce.API.Controllers
{
    
    public class MaterialController : BaseApiController
    {
        private readonly IGenericRepository<Material> _materialRepository;

        public MaterialController(IGenericRepository<Material> materialRepository)
        {
            _materialRepository  = materialRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Material>>> GetMateriales()
        {
            var materiales = await _materialRepository.GetAllAsync();
            return Ok(materiales);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Material>> GetMaterial(int id)
        {
            return await _materialRepository.GetByIdAsync(id);
        }
    }
}
