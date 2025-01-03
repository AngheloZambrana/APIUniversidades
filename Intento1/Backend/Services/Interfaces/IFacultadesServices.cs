using Backend.DTOs.WithID;
using Backend.DTOs.WithoutID;

namespace Backend.Services.Interfaces;

public interface IFacultadesServices
{
    public List<FacultadesDTO> GetAllFacultades();
    public FacultadesDTO? GetFacultadeById(int facultadeId);
    public FacultadesDTO? CreateFacultades(FacultadesPostDTO facultades);
    public FacultadesDTO? UpdateFacultades(FacultadesDTO facultades);
}