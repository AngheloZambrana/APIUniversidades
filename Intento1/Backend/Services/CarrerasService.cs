using AutoMapper;
using Backend.DTOs.WithID;
using Backend.DTOs.WithoutID;
using Backend.Entities;
using Backend.Services.Interfaces;
using DB.SingleDAO;

namespace Backend.Services;

public class CarrerasService : ICarrerasServices
{
    private readonly ICarreraDAO _carreraDAO;
    private readonly IMapper _mapper;

    public CarrerasService(IMapper mapper, ICarreraDAO carreraDAO)
    {
        _mapper = mapper;
        _carreraDAO = carreraDAO;
    }
    
    public List<CarrerasDTO> GetAllCarreras()
    {
        return _carreraDAO.ReadAll().Select(_mapper.Map<CarrerasDTO>).ToList();
    }

    public CarrerasDTO GetCarreraById(int carreraId)
    {
        return _mapper.Map<CarrerasDTO>(_carreraDAO.Read(carreraId));
    }
    
    public CarrerasDTO? CreateCarrera(CarrerasPostDTO carreras)
    {
        var entity = _mapper.Map<Carreras>(carreras);
        entity.CarrerasID = ObtenerUltimoId() + 1;
        _carreraDAO.Create(entity);
        return _mapper.Map<CarrerasDTO>(_carreraDAO.Read(entity.CarrerasID));
    }

    public CarrerasDTO? UpdateCarrera(CarrerasDTO carreras)
    {
        _carreraDAO.Update(_mapper.Map<Carreras>(carreras));
        return _mapper.Map<CarrerasDTO>(_carreraDAO.Read(carreras.CarrerasID));
    }

    public int ObtenerUltimoId()
    {
        var all = _carreraDAO.ReadAll();
        return all.Any() ? all.Max(x => x.CarrerasID) : 0;
    }
}