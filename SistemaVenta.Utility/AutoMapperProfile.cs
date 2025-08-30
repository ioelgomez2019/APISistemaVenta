using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using SistemaVenta.DTO; /*Data Transfer Object */
using SistemaVenta.Model; /*clases del dominio o entidades */

namespace SistemaVenta.Utility
{
    public class AutoMapperProfile: Profile
    {
        //Parallel crear un clase se escribe CTO
        public AutoMapperProfile()
        {
            #region Rol
            CreateMap<Rol, RolDTO>().ReverseMap();
            #endregion Rol

            #region Menu
            CreateMap<Menu, MenuDTO>().ReverseMap();
            #endregion  Menu

            #region Usuario
            CreateMap<Usuario, UsuarioDTO>()
                .ForMember(destino =>
                destino.RolDescripcion, //destino es el DTO
                opt => opt.MapFrom(origen => origen.IdRolNavigation.Nombre)
                )
                .ForMember(destino =>
                destino.EsActivo, //destino es el DTO
                opt => opt.MapFrom(origen => origen.EsActivo == true ? 0 : 1)
                );

            CreateMap<Usuario, SesionDTO>()
                .ForMember(destino =>
                destino.RolDescripcion, //destino es el DTO
                opt => opt.MapFrom(origen => origen.IdRolNavigation.Nombre)
                );
            CreateMap<UsuarioDTO, Usuario>()
                .ForMember(destino =>
                destino.IdRolNavigation, //destino es el DTO
                opt => opt.Ignore()
                )
                .ForMember(destino =>
                destino.EsActivo, //destino es el DTO
                opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)
                );

            #endregion  Usuario

            #region Categoria
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
            #endregion Categoria

            #region Producto
            CreateMap<Producto, ProductoDTO>()
                .ForMember(destino =>
                destino.DescripcionCategoria, //destino es el DTO
                opt => opt.MapFrom(origen => origen.IdCategoriaNavigation.Nombre)
                )
                .ForMember(destino =>
                destino.Precio, //destino es el DTO
                opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-PE")))
                ).ForMember(destino =>
                destino.EsActivo, //destino es el DTO
                opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
                );
            CreateMap<ProductoDTO, Producto>()
                .ForMember(destino =>
                destino.IdCategoriaNavigation, //destino es el DTO
                opt => opt.Ignore()
                )
                .ForMember(destino =>
                destino.Precio, //destino es el DTO
                opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Precio, new CultureInfo("es-PE")))
                )
                .ForMember(destino =>
                destino.EsActivo, //destino es el DTO
                opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)
                );

            #endregion Producto

            #region Venta
            CreateMap<Venta, VentaDTO>()
                .ForMember(destino =>
                destino.TotalTexto, //destino es el DTO
                opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-PE")))
                )
                .ForMember(destino =>
                destino.FechaRegistro, //destino es el DTO
                opt => opt.MapFrom(origen => origen.FechaRegistro.Value.ToString("dd/MM/yyyy"))
                );
            CreateMap<VentaDTO, Venta>()
                .ForMember(destino =>
                destino.Total, //destino es el DTO
                opt => opt.MapFrom(origen => Convert.ToDecimal(origen.TotalTexto, new CultureInfo("es-PE")))
                );


            #endregion Venta

            #region DetalleVenta
            CreateMap<DetalleVenta, DetalleVentaDTO>()
                .ForMember(destino =>
                destino.DescripcionProducto, //destino es el DTO
                opt => opt.MapFrom(origen => Convert.ToString(origen.IdProductoNavigation.Nombre))
                )
                .ForMember(destino =>
                destino.PrecioTexto, //destino es el DTO
                opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-PE")))
                )
                .ForMember(destino =>
                destino.TotalTexto, //destino es el DTO
                opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-PE")))
                );
            CreateMap<DetalleVentaDTO, DetalleVenta>()
                .ForMember(destino =>
                destino.Precio, //destino es el DTO  
                opt => opt.MapFrom(origen => Convert.ToDecimal(origen.PrecioTexto, new CultureInfo("es-PE")))
                ).ForMember(destino =>
                destino.Total, //destino es el DTO
                opt => opt.MapFrom(origen => Convert.ToDecimal(origen.TotalTexto, new CultureInfo("es-PE")))
                );
            #endregion DetalleVenta

            #region Reporte
            CreateMap<DetalleVenta, ReporteDTO>()
                .ForMember(destino =>
                destino.FechaRegistro, //destino es el DTO
                opt => opt.MapFrom(origen => origen.IdVentaNavigation.FechaRegistro.Value.ToString("dd/MM/yyyy"))
                )
                .ForMember(destino =>
                destino.NumeroDocumento, //destino es el DTO
                memberOptions => memberOptions.MapFrom(origen => origen.IdVentaNavigation.NumeroDocumento)
                )
                .ForMember(destino =>
                destino.TipoPago, //destino es el DTO
                memberOptions => memberOptions.MapFrom(origen => origen.IdVentaNavigation.TipoPago)
                )
                .ForMember(destino =>
                destino.TotalVenta, //destino es el DTO
                memberOptions => memberOptions.MapFrom(origen => Convert.ToString(origen.IdVentaNavigation.Total.Value, new CultureInfo("es-PE")))
                )
                .ForMember(destino =>
                destino.Producto, //destino es el DTO
                memberOptions => memberOptions.MapFrom(origen => origen.IdProductoNavigation.Nombre)
                )
                .ForMember(destino =>
                destino.Precio, //destino es el DTO
                memberOptions => memberOptions.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-PE")))
                )
                .ForMember(destino =>
                destino.Total, //destino es el DTO
                memberOptions => memberOptions.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-PE")))
                );
            #endregion Reporte
        }
    }
}
