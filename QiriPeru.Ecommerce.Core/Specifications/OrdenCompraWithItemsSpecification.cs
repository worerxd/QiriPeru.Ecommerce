using QiriPeru.Ecommerce.Core.Entities.OrdenCompra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QiriPeru.Ecommerce.Core.Specifications
{
    public class OrdenCompraWithItemsSpecification : BaseSpecification<OrdenCompras>
    {

        public OrdenCompraWithItemsSpecification(string email) : base(o => o.CompradorEmail == email)
        {
            AddInclude(o => o.OrdenItems);
            AddInclude(o => o.TipoEnvio);            
            AddOrderByDescending(o => o.OrdenCompraFecha);
        }

        public OrdenCompraWithItemsSpecification(int id, string email) : base(o => o.Id == id && o.CompradorEmail == email)
        {
            AddInclude(o => o.OrdenItems);
            AddInclude(o => o.TipoEnvio);            
            AddOrderByDescending(o => o.OrdenCompraFecha);
        }

        public OrdenCompraWithItemsSpecification(int id) : base(o => o.Id == id)
        {
            AddInclude(o => o.OrdenItems);
            AddInclude(o => o.TipoEnvio);
            AddOrderByDescending(o => o.OrdenCompraFecha);
        }

        public OrdenCompraWithItemsSpecification() : base()
        {
            AddInclude(o => o.OrdenItems);
            AddInclude(o => o.TipoEnvio);           
            AddOrderByDescending(o => o.OrdenCompraFecha);
        }


    }
}
