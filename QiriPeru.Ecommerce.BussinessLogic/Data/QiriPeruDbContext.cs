using Microsoft.EntityFrameworkCore;
using QiriPeru.Ecommerce.Core.Entities;
using QiriPeru.Ecommerce.Core.Entities.OrdenCompra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QiriPeru.Ecommerce.BussinessLogic.Data
{
    public class QiriPeruDbContext : DbContext
    {
        public QiriPeruDbContext(DbContextOptions<QiriPeruDbContext> options) : base(options)
        {

        }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Material> Materiales { get; set; }
        public DbSet<OrdenCompras> OrdenCompras { get; set; }
        public DbSet<OrdenItem> OrdenItems { get; set; }
        public DbSet<TipoEnvio> TipoEnvios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }

    }
}
