using Backend.DTOs.WithID;
using Backend.DTOs.WithoutID;
using Backend.Services.Interfaces;
using DB.SingleDAO;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class UniversidadesController : ControllerBase
{
    private readonly IUniversidadesServices _universidadesServices;

    public UniversidadesController(IUniversidadesServices universidadesServices)
    {
        _universidadesServices = universidadesServices;
    }

    [HttpGet]
    public ActionResult<List<UniversidadesDTO>> GetAllUniversidades()
    {
        var result =  _universidadesServices.GetAllUniversidades();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public ActionResult<UniversidadesDTO> GetUniversidadeById(int id)
    {
        var result = _universidadesServices.GetUniversidadeById(id);
        return Ok(result);
    }
    
    [HttpPost]
    public ActionResult<UniversidadesDTO> CreateUniversidades(UniversidadesPostDTO universidades)
    {
        var result = _universidadesServices.CreateUniversidades(universidades);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public ActionResult<UniversidadesDTO> UpdateUniversidades(int id, [FromBody] UniversidadesDTO universidades)
    {
        if (id != universidades.UniversidadID)
        {
            return BadRequest("El ID de la URL no coincide con el ID del cuerpo.");
        }
        var result = _universidadesServices.UpdateUniversidades(universidades);
        return Ok(result);
    }
}


