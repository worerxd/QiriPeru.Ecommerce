using QiriPeru.Ecommerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QiriPeru.Ecommerce.Core.Specifications
{
    public class ProductoWithCategoriasAndMaterialSpecification : BaseSpecification<Producto>
    {
        public ProductoWithCategoriasAndMaterialSpecification(ProductoSpecificationParams productoParams) 
            : base( x => 
            (string.IsNullOrEmpty(productoParams.Search) || x.Nombre.Contains(productoParams.Search)) &&
            (!productoParams.Material.HasValue || x.MaterialId == productoParams.Material) &&
            (!productoParams.Categoria.HasValue || x.CategoriaId == productoParams.Categoria)
            )
        {
            AddInclude(p => p.Categoria);
            AddInclude(p => p.Material);

            //ApplyPaging(Skip, Take);
            ApplyPaging(productoParams.PageSize * (productoParams.PageIndex - 1),productoParams.PageSize );

            if (!string.IsNullOrEmpty(productoParams.Sort))
            {
                switch (productoParams.Sort)
                {
                    case "nombreAsc":
                        AddOrderBy(p => p.Nombre);
                        break;
                    case "nombreDesc":
                        AddOrderByDescending(p => p.Nombre);
                        break;
                    case "precioAsc":
                        AddOrderBy(p => p.Precio);
                        break;
                    case "precioDesc":
                        AddOrderByDescending(p => p.Precio);
                        break;
                    case "descripcionAsc":
                        AddOrderBy(p => p.Descripcion);
                        break;
                    case "descripcionDesc":
                        AddOrderByDescending(p => p.Descripcion);
                        break;
                    default:
                        AddOrderBy(p => p.Nombre);
                        break;

                }
            }
        }

        public ProductoWithCategoriasAndMaterialSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(p => p.Categoria);
            AddInclude(p => p.Material);
        }
    }
}
