using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QiriPeru.Ecommerce.Core.Entities
{
    public class CarritoItem
    {
        public int Id { get; set; }
        public string Producto { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public string Imagen { get; set; }
        public string Material { get; set; }
        public string Categoria { get; set; }
    }
}
