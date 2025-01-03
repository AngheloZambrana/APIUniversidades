using AutoMapper;
using Backend.DTOs.WithID;
using Backend.DTOs.WithoutID;
using Backend.Entities;

namespace Backend.Mappers;

public class FacultadesMapper : Profile
{
    public FacultadesMapper()
    {
        CreateMap<FacultadesDTO, Facultades>().ReverseMap();
        CreateMap<(FacultadesPostDTO, int), Facultades>()
            .ForMember(dst => dst.UniversidadID, opt => opt.MapFrom(src => src.Item2))
            .ForMember(dst => dst.Nombre, opt => opt.MapFrom(src => src.Item1.Nombre))
            .ForMember(dst => dst.Descripcion, opt => opt.MapFrom(src => src.Item1.Descripcion))
            .ForMember(dst => dst.FacultadID, opt => opt.MapFrom(src => src.Item2))
            .ReverseMap();
        CreateMap<FacultadesPostDTO, Facultades>()
            .ForMember(dst => dst.Nombre, opt => opt.MapFrom(src => src.Nombre))
            .ForMember(dst => dst.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
            .ForMember(dst => dst.UniversidadID, opt => opt.MapFrom(src => src.UniversidadID));
    }
}