using AutoMapper;
using Backend.DTOs.WithID;
using Backend.DTOs.WithoutID;
using Backend.Entities;
using Backend.Services.Interfaces;
using DB.SingleDAO;

namespace Backend.Services;

public class FacultadesService : IFacultadesServices
{
    private readonly IFacultadDAO _facultadDao;
    private readonly IMapper _mapper;

    public FacultadesService(IMapper mapper, IFacultadDAO facultadDAO)
    {
        _mapper = mapper;
        _facultadDao = facultadDAO;
    }

    public List<FacultadesDTO> GetAllFacultades()
    {
        return _facultadDao.ReadAll().Select(_mapper.Map<FacultadesDTO>).ToList();
    }

    public FacultadesDTO GetFacultadeById(int facultadeId)
    {
        return _mapper.Map<FacultadesDTO>(_facultadDao.Read(facultadeId));
    }
    
    public FacultadesDTO? CreateFacultades(FacultadesPostDTO facultades)
    {
        var entity = _mapper.Map<Facultades>(facultades);
        entity.FacultadID = ObtenerUltimoId() + 1;
        _facultadDao.Create(entity);
        return _mapper.Map<FacultadesDTO>(_facultadDao.Read(entity.FacultadID));
    }

    public FacultadesDTO? UpdateFacultades(FacultadesDTO facultades)
    {
        _facultadDao.Update(_mapper.Map<Facultades>(facultades));
        return _mapper.Map<FacultadesDTO>(_facultadDao.Read(facultades.FacultadID));
    }

    public int ObtenerUltimoId()
    {
        var all = _facultadDao.ReadAll();
        return all.Any() ? all.Max(x => x.FacultadID) : 0;
    }
}