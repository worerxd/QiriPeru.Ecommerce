using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QiriPeru.Ecommerce.Core.Entities.OrdenCompra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QiriPeru.Ecommerce.BussinessLogic.Data.Configuration
{
    public class TipoEnvioConfiguration : IEntityTypeConfiguration<TipoEnvio>
    {
        public void Configure(EntityTypeBuilder<TipoEnvio> builder)
        {
            builder.Property(t => t.Precio)
                .HasColumnType("decimal(18,2)");
        }
    }
}
