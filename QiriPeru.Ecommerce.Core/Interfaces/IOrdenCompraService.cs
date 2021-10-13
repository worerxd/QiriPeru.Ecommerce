﻿using QiriPeru.Ecommerce.Core.Entities.OrdenCompra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QiriPeru.Ecommerce.Core.Interfaces
{
    public interface IOrdenCompraService
    {
        Task<OrdenCompras> AddOrdenCompraAsync(string compradorEmail, int tipoEnvio, string carritoId, Direccion direccion);

        Task<IReadOnlyList<OrdenCompras>> GetOrdenComprasByUserEmailAsync(string email);

        Task<OrdenCompras> GetOrdenComprasByIdAsync(int id, string email);

        Task<IReadOnlyList<TipoEnvio>> GetTipoEnvios();
    }
}
