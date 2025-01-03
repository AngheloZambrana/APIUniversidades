using Backend.DTOs.WithID;
using Backend.DTOs.WithoutID;

namespace Backend.Services.Interfaces;

public interface IBecaServices
{
    public List<BecaDTO> GetAllBecas();
    public BecaDTO? GetBecaById(int becaId);
    public BecaDTO? CreateBeca(BecaPostDTO beca);
    public BecaDTO? UpdateBeca(BecaDTO beca);
}