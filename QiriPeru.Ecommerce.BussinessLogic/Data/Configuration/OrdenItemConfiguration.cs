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
    public class OrdenItemConfiguration : IEntityTypeConfiguration<OrdenItem>
    {
        public void Configure(EntityTypeBuilder<OrdenItem> builder)
        {
            builder.OwnsOne(i => i.ItemOrdenado, x => 
            { 
                x.WithOwner(); 
            });

            builder.Property(i => i.Precio)
                .HasColumnType("decimal(18,2)");

        }
    }
}
