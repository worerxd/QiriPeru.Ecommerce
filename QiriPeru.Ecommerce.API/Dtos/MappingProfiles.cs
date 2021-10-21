using AutoMapper;
using QiriPeru.Ecommerce.Core.Entities;
using QiriPeru.Ecommerce.Core.Entities.OrdenCompra;
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

            CreateMap<Core.Entities.Direccion, DireccionDto>().ReverseMap();

            CreateMap<Usuario, UsuarioDto>().ReverseMap();

            CreateMap<DireccionDto, Core.Entities.OrdenCompra.Direccion>();

            CreateMap<OrdenCompras, OrdenCompraResponseDto>()
                .ForMember(o => o.TipoEnvio, x => x.MapFrom(y => y.TipoEnvio.Nombre))
                .ForMember(o => o.TipoEnvioPrecio, x => x.MapFrom(y => y.TipoEnvio.Precio));

            CreateMap<OrdenItem, OrdenItemResponseDto>()
                .ForMember(o => o.ProductoId, x => x.MapFrom(y => y.ItemOrdenado.ProductoItemId))
                .ForMember(o => o.ProductoNombre, x => x.MapFrom(y => y.ItemOrdenado.ProductoNombre))
                .ForMember(o => o.ProductoImagen, x => x.MapFrom(y => y.ItemOrdenado.ImagenUrl));
        }
    }
}
