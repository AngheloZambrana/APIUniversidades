using AutoMapper;
using Backend.DTOs.WithID;
using Backend.DTOs.WithoutID;
using Backend.Entities;

namespace Backend.Mappers;

public class CarrerasMapper : Profile
{
    public CarrerasMapper()
    {
        CreateMap<Carreras, CarrerasDTO>().ReverseMap();
        CreateMap<(CarrerasPostDTO, int), Carreras>()
            .ForMember(dst => dst.CarrerasID, opt => opt.MapFrom(src => src.Item2))
            .ForMember(dst => dst.Nombre, opt => opt.MapFrom(src => src.Item1.Nombre))
            .ForMember(dst => dst.Duracion, opt => opt.MapFrom(src => src.Item1.Duracion))
            .ForMember(dst => dst.FacultadID, opt => opt.MapFrom(src => src.Item1.FacultadID))
            .ReverseMap();
        CreateMap<CarrerasPostDTO, Carreras>()
            .ForMember(dst => dst.Nombre, opt => opt.MapFrom(src => src.Nombre))
            .ForMember(dst => dst.Duracion, opt => opt.MapFrom(src => src.Duracion))
            .ForMember(dst => dst.FacultadID, opt => opt.MapFrom(src => src.FacultadID));
    }
    
}