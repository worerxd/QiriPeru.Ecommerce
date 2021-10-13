using QiriPeru.Ecommerce.Core.Entities;
using QiriPeru.Ecommerce.Core.Entities.OrdenCompra;
using QiriPeru.Ecommerce.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QiriPeru.Ecommerce.BussinessLogic.Logic
{
    public class OrdenCompraService : IOrdenCompraService
    {
        private readonly IGenericRepository<OrdenCompras> _ordenComprasRepository;
        private readonly IGenericRepository<Producto> _productoRepsitory;
        private readonly ICarritoCompraRepository _carritoCompraRepository;
        private readonly IGenericRepository<TipoEnvio> _tipoEnvioRepository;

        public OrdenCompraService(IGenericRepository<OrdenCompras> ordenComprasRepository, IGenericRepository<Producto> productoRepsitory, ICarritoCompraRepository carritoCompraRepository, IGenericRepository<TipoEnvio> tipoEnvioRepository)
        {
            _ordenComprasRepository = ordenComprasRepository;
            _productoRepsitory = productoRepsitory;
            _carritoCompraRepository = carritoCompraRepository;
            _tipoEnvioRepository = tipoEnvioRepository;
        }
        public async Task<OrdenCompras> AddOrdenCompraAsync(string compradorEmail, int tipoEnvio, string carritoId, Core.Entities.OrdenCompra.Direccion direccion)
        {
            var carritoCompra = await _carritoCompraRepository.GetCarritoCompraAsync(carritoId);

            var items = new List<OrdenItem>();

            foreach (var item in carritoCompra.Items)
            {
                var productoItem = await _productoRepsitory.GetByIdAsync(item.Id);
                var itemOrdenado = new ProductoItemOrdenado(productoItem.Id, productoItem.Nombre, productoItem.Imagen);
                var ordenItem = new OrdenItem(itemOrdenado, productoItem.Precio, item.Cantidad);

                items.Add(ordenItem);
            }

            var tipoEnvioEntity = await _tipoEnvioRepository.GetByIdAsync(tipoEnvio);

            var subTotal = items.Sum(item => item.Precio * item.Cantidad);

            var ordenCompra = new OrdenCompras(compradorEmail, direccion, tipoEnvioEntity, items, subTotal);

            return ordenCompra;

        }

        public Task<OrdenCompras> GetOrdenComprasByIdAsync(int id, string email)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<OrdenCompras>> GetOrdenComprasByUserEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<TipoEnvio>> GetTipoEnvios()
        {
            throw new NotImplementedException();
        }
    }
}
