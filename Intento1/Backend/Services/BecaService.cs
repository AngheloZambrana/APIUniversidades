using AutoMapper;
using Backend.DTOs.WithID;
using Backend.DTOs.WithoutID;
using Backend.Entities;
using Backend.Services.Interfaces;
using DB.SingleDAO;

namespace Backend.Services;

public class BecaService : IBecaServices
{
    private readonly IBecaDAO _becaDao;
    private readonly IMapper _mapper;

    public BecaService(IBecaDAO becaDao, IMapper mapper)
    {
        _becaDao = becaDao;
        _mapper = mapper;
    }
    
    public List<BecaDTO> GetAllBecas()
    {
        return _becaDao.ReadAll().Select(_mapper.Map<BecaDTO>).ToList();
    }

    public BecaDTO GetBecaById(int becaId)
    {
        return _mapper.Map<BecaDTO>(_becaDao.Read(becaId));
    }
    
    public BecaDTO? CreateBeca(BecaPostDTO becas)
    {
        var entity = _mapper.Map<Becas>(becas);
        entity.BecaID = ObtenerUltimoId() + 1;
        _becaDao.Create(entity);
        return _mapper.Map<BecaDTO>(_becaDao.Read(entity.BecaID));
    }

    public BecaDTO? UpdateBeca(BecaDTO becas)
    {
        _becaDao.Update(_mapper.Map<Becas>(becas));
        return _mapper.Map<BecaDTO>(_becaDao.Read(becas.BecaID));
    }

    public int ObtenerUltimoId()
    {
        var all = _becaDao.ReadAll();
        return all.Any() ? all.Max(x => x.BecaID) : 0;
    }
}