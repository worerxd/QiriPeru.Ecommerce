using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QiriPeru.Ecommerce.Core.Entities
{
    public class Producto : ClaseBase
    {
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public int Stock { get; set; }

        public int MaterialId { get; set; }

        public Material Material { get; set; }

        public int CategoriaId { get; set; }

        public Categoria Categoria { get; set; }

        public decimal Precio { get; set; }

        public string Imagen { get; set; }
    }
}
