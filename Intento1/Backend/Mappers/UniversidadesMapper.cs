using AutoMapper;
using Backend.DTOs.WithID;
using Backend.DTOs.WithoutID;
using Backend.Entities;

public class UniversidadesMapper : Profile
{
    public UniversidadesMapper()
    {
        CreateMap<Universidades, UniversidadesDTO>().ReverseMap();
        CreateMap<(UniversidadesPostDTO, int), Universidades>().
            ForMember(dst => dst.UniversidadID, opt => opt.MapFrom(src => src.Item2))
            .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Item1.Name))
            .ForMember(dst => dst.Descripcion, opt => opt.MapFrom(src => src.Item1.Descripcion))
            .ForMember(dst => dst.Tipo, opt => opt.MapFrom(src => src.Item1.Tipo))
            .ReverseMap();
        CreateMap<UniversidadesPostDTO, Universidades>()
            .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dst => dst.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
            .ForMember(dst => dst.Tipo, opt => opt.MapFrom(src => src.Tipo));
    }
}