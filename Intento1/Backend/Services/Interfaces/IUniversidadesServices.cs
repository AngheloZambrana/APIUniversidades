using Backend.DTOs.WithID;
using Backend.DTOs.WithoutID;

namespace Backend.Services.Interfaces;

public interface IUniversidadesServices
{
    public List<UniversidadesDTO> GetAllUniversidades();
    public UniversidadesDTO? GetUniversidadeById(int universidadeId);
    public UniversidadesDTO? CreateUniversidades(UniversidadesPostDTO universidade);
    public UniversidadesDTO? UpdateUniversidades(UniversidadesDTO universidades);

}