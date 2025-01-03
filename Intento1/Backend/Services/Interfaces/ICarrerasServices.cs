using Backend.DTOs.WithID;
using Backend.DTOs.WithoutID;

namespace Backend.Services.Interfaces;

public interface ICarrerasServices
{
    public List<CarrerasDTO> GetAllCarreras();
    public CarrerasDTO? GetCarreraById(int carreraId);
    public CarrerasDTO? CreateCarrera(CarrerasPostDTO carreras);
    public CarrerasDTO? UpdateCarrera(CarrerasDTO carreras);
}