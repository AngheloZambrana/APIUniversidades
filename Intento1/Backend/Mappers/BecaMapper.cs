using AutoMapper;
using Backend.DTOs.WithID;
using Backend.DTOs.WithoutID;
using Backend.Entities;

namespace Backend.Mappers;

public class BecaMapper : Profile
{
    public BecaMapper()
    {
        CreateMap<BecaDTO, Becas>().ReverseMap();
        CreateMap<(BecaPostDTO, int), Becas>()
            .ForMember(dst => dst.BecaID, opt => opt.MapFrom(src => src.Item2))
            .ForMember(dst => dst.Nombre, opt => opt.MapFrom(src => src.Item1.Nombre))
            .ForMember(dst => dst.Descripcion, opt => opt.MapFrom(src => src.Item1.Descripcion))
            .ForMember(dst => dst.Criterios, opt => opt.MapFrom(src => src.Item1.Criterios))
            .ForMember(dst => dst.UniversidadID, opt => opt.MapFrom(src => src.Item1.UniversidadID))
            .ReverseMap();
        CreateMap<BecaPostDTO, Becas>()
            .ForMember(dst => dst.Nombre, opt => opt.MapFrom(src => src.Nombre))
            .ForMember(dst => dst.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
            .ForMember(dst => dst.Criterios, opt => opt.MapFrom(src => src.Criterios))
            .ForMember(dst => dst.UniversidadID, opt => opt.MapFrom(src => src.UniversidadID));

    }
}