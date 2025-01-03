using AutoMapper;
using Backend.DTOs.WithID;
using Backend.DTOs.WithoutID;
using Backend.Entities;
using Backend.Services.Interfaces;
using DB.SingleDAO;

namespace Backend.Services;

public class UniversidadesService : IUniversidadesServices
{
    private readonly IUniversidadesDAO _universidadesDAO;
    private readonly IMapper _mapper;

    public UniversidadesService(IMapper mapper, IUniversidadesDAO universidadeDAO)
    {
        _universidadesDAO = universidadeDAO;
        _mapper = mapper;
    }
    
    public List<UniversidadesDTO> GetAllUniversidades()
    {
        return _universidadesDAO.ReadAll().Select(_mapper.Map<UniversidadesDTO>).ToList();
    }

    public UniversidadesDTO GetUniversidadeById(int universidadeId)
    {
        return _mapper.Map<UniversidadesDTO>(_universidadesDAO.Read(universidadeId));
    }
    
    public UniversidadesDTO? CreateUniversidades(UniversidadesPostDTO universidade)
    {
        var entity = _mapper.Map<Universidades>(universidade);
        entity.UniversidadID = ObtenerUltimoId() + 1;
        _universidadesDAO.Create(entity);
        return _mapper.Map<UniversidadesDTO>(_universidadesDAO.Read(entity.UniversidadID));
    }

    public UniversidadesDTO? UpdateUniversidades(UniversidadesDTO universidades)
    {
        _universidadesDAO.Update(_mapper.Map<Universidades>(universidades));
        return _mapper.Map<UniversidadesDTO>(_universidadesDAO.Read(universidades.UniversidadID));
    }

    public int ObtenerUltimoId()
    {
        var allUniversidades = _universidadesDAO.ReadAll();
        return allUniversidades.Any() ? allUniversidades.Max(x => x.UniversidadID) : 0;
    }
}