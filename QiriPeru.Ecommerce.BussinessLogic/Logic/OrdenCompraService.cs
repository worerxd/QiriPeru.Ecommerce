using Microsoft.AspNetCore.Identity;
using QiriPeru.Ecommerce.Core.Entities;
using QiriPeru.Ecommerce.Core.Entities.OrdenCompra;
using QiriPeru.Ecommerce.Core.Interfaces;
using QiriPeru.Ecommerce.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QiriPeru.Ecommerce.BussinessLogic.Logic
{
    public class OrdenCompraService : IOrdenCompraService
    {              
        private readonly ICarritoCompraRepository _carritoCompraRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<Usuario> _userManager;

        public OrdenCompraService(ICarritoCompraRepository carritoCompraRepository, IUnitOfWork unitOfWork, UserManager<Usuario> userManager)
        {
            _carritoCompraRepository = carritoCompraRepository;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<OrdenCompras> AddOrdenCompraAsync(string compradorEmail, int tipoEnvio, string carritoId, Core.Entities.OrdenCompra.Direccion direccion)
        {
            var carritoCompra = await _carritoCompraRepository.GetCarritoCompraAsync(carritoId);

            if(carritoCompra == null)
            {
                return null;
            }

            var items = new List<OrdenItem>();

            foreach (var item in carritoCompra.Items)
            {
                var productoItem = await _unitOfWork.Repository<Producto>().GetByIdAsync(item.Id);
                var itemOrdenado = new ProductoItemOrdenado(productoItem.Id, productoItem.Nombre, productoItem.Imagen);
                var ordenItem = new OrdenItem(itemOrdenado, productoItem.Precio, item.Cantidad);

                items.Add(ordenItem);
            }            

            var tipoEnvioEntity = await _unitOfWork.Repository<TipoEnvio>().GetByIdAsync(tipoEnvio);

            var subTotal = items.Sum(item => item.Precio * item.Cantidad);

            var ordenCompra = new OrdenCompras(compradorEmail, direccion, tipoEnvioEntity, items, subTotal);

            _unitOfWork.Repository<OrdenCompras>().AddEntity(ordenCompra);

            var resultado = await _unitOfWork.Complete();

            if(resultado<=0)
            {
                return null;
            }

            await _carritoCompraRepository.DeleteCarritoCompraAsync(carritoId);

            return ordenCompra;

        }

        public async Task<OrdenCompras> UpdateOrdenCompraByIdAsync(int id)
        {
            var spec = new OrdenCompraWithItemsSpecification(id);

            var ordenCompra = await _unitOfWork.Repository<OrdenCompras>().GetByIdWithSpecAsync(spec);

            if (ordenCompra.Status == OrdenStatus.Pendiente)
                ordenCompra.Status = OrdenStatus.Entregado;
            else if (ordenCompra.Status == OrdenStatus.Entregado)
                ordenCompra.Status = OrdenStatus.Cancelado;

            await _unitOfWork.Repository<OrdenCompras>().Update(ordenCompra);

            return ordenCompra;
        }

        public async Task<IReadOnlyList<OrdenCompras>> GetOrdenComprasAll()
        {
            var spec = new OrdenCompraWithItemsSpecification();

            return await _unitOfWork.Repository<OrdenCompras>().GetAllWithSpecAsync(spec);
        }

        public async Task<OrdenCompras> GetOrdenComprasByIdAsync(int id)
        {
            var spec = new OrdenCompraWithItemsSpecification(id);

            return await _unitOfWork.Repository<OrdenCompras>().GetByIdWithSpecAsync(spec);
        }

        public async Task<IReadOnlyList<OrdenCompras>> GetOrdenComprasByUserEmailAsync(string email)
        {
            var spec = new OrdenCompraWithItemsSpecification(email);

            return await _unitOfWork.Repository<OrdenCompras>().GetAllWithSpecAsync(spec);
        }

        public async Task<IReadOnlyList<TipoEnvio>> GetTipoEnvios()
        {
            return await _unitOfWork.Repository<TipoEnvio>().GetAllAsync();
        }

        
    }
}
