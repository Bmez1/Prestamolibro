using AutoMapper;
using PruebaIngresoblibliotecario.Core.DTOs;
using PruebaIngresoblibliotecario.Core.DTOs.Inputs;
using PruebaIngresoblibliotecario.Core.Entities;

namespace PruebaIngresoblibliotecario.Core.Mappers
{
    public class PrestamoMappingProfile : Profile
    {
        public PrestamoMappingProfile()
        {
            CreateMap<CrearPrestamoDto, Prestamo>()
                .ForMember(dest => dest.IdentificacionUsuario, opt => opt.MapFrom(src => src.IdentificacionUsuario.Trim()));
            CreateMap<Prestamo, PrestamoCreadoDto>()
                .ForMember(dest => dest.FechaMaximaDevolucion, opt => opt.MapFrom(src => src.FechaMaximaDevolucion.ToShortDateString()));
            CreateMap<Prestamo, DataPrestamoDto>();
        }
    }
}