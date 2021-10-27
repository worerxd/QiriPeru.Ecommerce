﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QiriPeru.Ecommerce.Core.Entities.OrdenCompra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QiriPeru.Ecommerce.BussinessLogic.Data.Configuration
{
    public class OrdenCompraConfiguration : IEntityTypeConfiguration<OrdenCompras>
    {
        public void Configure(EntityTypeBuilder<OrdenCompras> builder)
        {
            builder.OwnsOne(o => o.DireccionEnvio, x =>
           {
               x.WithOwner();
           });

            
                

            builder.Property(s => s.Status)
                .HasConversion(
                o => o.ToString(), 
                o => (OrdenStatus)Enum.Parse(typeof(OrdenStatus), o)
                );

            builder.HasMany(o => o.OrdenItems)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(o => o.SubTotal)
                .HasColumnType("decimal(18,2)");
        }
    }
}
