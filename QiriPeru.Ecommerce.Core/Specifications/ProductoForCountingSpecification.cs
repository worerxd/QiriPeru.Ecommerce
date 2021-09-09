using QiriPeru.Ecommerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QiriPeru.Ecommerce.Core.Specifications
{
    public class ProductoForCountingSpecification : BaseSpecification<Producto>
    {
        public ProductoForCountingSpecification(ProductoSpecificationParams productoParams)
            : base(x =>
            (string.IsNullOrEmpty(productoParams.Search) || x.Nombre.Contains(productoParams.Search)) &&
            (!productoParams.Material.HasValue || x.MaterialId == productoParams.Material) &&
            (!productoParams.Categoria.HasValue || x.CategoriaId == productoParams.Categoria)
            )
        {
        }
    }
}
