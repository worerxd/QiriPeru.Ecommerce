using Microsoft.EntityFrameworkCore;
using QiriPeru.Ecommerce.BussinessLogic.Data;
using QiriPeru.Ecommerce.Core.Entities;
using QiriPeru.Ecommerce.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QiriPeru.Ecommerce.BussinessLogic.Logic
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly QiriPeruDbContext _context;

        public ProductoRepository(QiriPeruDbContext context)
        {
            _context = context;
        }
        public async Task<Producto> GetProductoByIdAsync(int id)
        {
            return await _context.Productos
                            .Include(p => p.Categoria)
                            .Include(p => p.Material)
                            .SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IReadOnlyList<Producto>> GetProductosAsync()
        {
            return await _context.Productos
                            .Include(p => p.Categoria)
                            .Include(p => p.Material)
                            .ToListAsync();
        }
    }
}
