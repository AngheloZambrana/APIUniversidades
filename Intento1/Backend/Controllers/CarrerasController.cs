using Backend.DTOs.WithID;
using Backend.DTOs.WithoutID;
using Backend.Services;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class CarrerasController : ControllerBase
{
    private readonly ICarrerasServices _carrerasService;

    public CarrerasController(ICarrerasServices carrerasService)
    {
        _carrerasService = carrerasService;
    }

    [HttpGet]
    public ActionResult<List<CarrerasDTO>> GetAllCarreras()
    {
        var result = _carrerasService.GetAllCarreras();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public ActionResult<CarrerasDTO> GetCarreras(int id)
    {
        var result = _carrerasService.GetCarreraById(id);
        return Ok(result);
    }
    
    [HttpPost]
    public ActionResult<CarrerasDTO> CreateCarrera(CarrerasPostDTO carreras)
    {
        var result = _carrerasService.CreateCarrera(carreras);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public ActionResult<CarrerasDTO> UpdateCarrera(int id, [FromBody] CarrerasDTO carreras)
    {
        if (id != carreras.CarrerasID)
        {
            return BadRequest("El ID de la URL no coincide con el ID del cuerpo.");
        }
        var result = _carrerasService.UpdateCarrera(carreras);
        return Ok(result);
    }
}