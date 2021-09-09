using Microsoft.Extensions.Logging;
using QiriPeru.Ecommerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QiriPeru.Ecommerce.BussinessLogic.Data
{
    public class QiriPeruDbContextData
    {
        public static async Task CargarDataAsync(QiriPeruDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if(!context.Materiales.Any())
                {
                    var materialesData = File.ReadAllText("../QiriPeru.Ecommerce.BussinessLogic/CargarData/material.json");
                    var materiales = JsonSerializer.Deserialize<List<Material>>(materialesData);

                    foreach (var material in materiales)
                    {
                        context.Materiales.Add(material);
                    }

                    await context.SaveChangesAsync();

                }

                if (!context.Categorias.Any())
                {
                    var categoriasData = File.ReadAllText("../QiriPeru.Ecommerce.BussinessLogic/CargarData/categoria.json");
                    var categorias = JsonSerializer.Deserialize<List<Categoria>>(categoriasData);

                    foreach (var categoria in categorias)
                    {
                        context.Categorias.Add(categoria);
                    }

                    await context.SaveChangesAsync();

                }

                if (!context.Productos.Any())
                {
                    var productosData = File.ReadAllText("../QiriPeru.Ecommerce.BussinessLogic/CargarData/producto.json");
                    var productos = JsonSerializer.Deserialize<List<Producto>>(productosData);

                    foreach (var producto in productos)
                    {
                        context.Productos.Add(producto);
                    }

                    await context.SaveChangesAsync();

                }

            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<QiriPeruDbContextData>();
                logger.LogError(e.Message);
            }
        }
    }
}
