using AutoMapper;
using QiriPeru.Ecommerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QiriPeru.Ecommerce.API.Dtos
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Producto, ProductoDto>()
                .ForMember(p => p.CategoriaNombre, x => x.MapFrom(a => a.Categoria.Nombre))
                .ForMember(p => p.MaterialNombre, x => x.MapFrom(a => a.Material.Nombre));

            CreateMap<Direccion, DireccionDto>().ReverseMap();

            CreateMap<Usuario, UsuarioDto>().ReverseMap();

            CreateMap<DireccionDto, Core.Entities.OrdenCompra.Direccion>();
        }
    }
}
